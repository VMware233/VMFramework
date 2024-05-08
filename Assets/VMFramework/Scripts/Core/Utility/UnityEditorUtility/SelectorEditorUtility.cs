#if UNITY_EDITOR
using Sirenix.OdinInspector.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace VMFramework.Core.Editor
{
    public static class SelectorEditorUtility
    {
        public const float DEFAULT_WIDE_POPUP_WIDTH = 350;
        public const float DEFAULT_NARROW_POPUP_WIDTH = 200;

        #region Show Odin Selector

        public static void Show<T>(this OdinSelector<T> selector, bool autoConfirmSelection = false)
        {
            selector.AssertIsNotNull(nameof(selector));
            
            if (autoConfirmSelection)
            {
                if (selector.SelectionTree.EnumerateTree().Count() == 1)
                {
                    selector.SelectionTree.EnumerateTree().First().Select();
                    selector.SelectionTree.Selection.ConfirmSelection();
                    return;
                }
            }

            selector.ShowInPopup();
        }

        #endregion
    }

    #region Scriptable Object Create Selector

    public class ScriptableObjectCreationSelector : ScriptableObjectCreationSelector<ScriptableObject>
    {
        public ScriptableObjectCreationSelector(
            Type scriptableObjectBaseType,
            string defaultDestinationPath,
            Action<ScriptableObject> onScriptableObjectCreated = null
        ) : base(
            scriptableObjectBaseType
                .GetDerivedClasses(true, false)
                .Where(type => type.IsAbstract == false),
            defaultDestinationPath,
            onScriptableObjectCreated
        )
        {

        }
    }
    
    public class ScriptableObjectCreationSelector<T> : TypeSelector
        where T : ScriptableObject
    {
        private readonly Action<T> onCreated;
        private readonly string defaultDestinationPath;

        #region Constructor
        
        public ScriptableObjectCreationSelector(
            IEnumerable<Type> scriptableObjectTypes,
            string defaultDestinationPath,
            Action<T> onCreated = null) : base(scriptableObjectTypes)
        {
            this.onCreated = onCreated;
            this.defaultDestinationPath = defaultDestinationPath;
            SelectionConfirmed += ShowSaveFileDialog;
        }
        
        public ScriptableObjectCreationSelector(
            string defaultDestinationPath,
            Action<T> onCreated = null
        ) : this(
            typeof(T).GetDerivedClasses(true, false).Where(type => type.IsAbstract == false),
            defaultDestinationPath,
            onCreated
        )
        {

        }

        #endregion

        #region Show Save File Dialog

        private void ShowSaveFileDialog(IEnumerable<Type> selection)
        {
            var selectedType = selection.FirstOrDefault();

            var obj =
                ScriptableObject.CreateInstance(selectedType);

            string destination = defaultDestinationPath.TrimEnd('/');

            if (destination.CreateDirectory())
            {
                AssetDatabase.Refresh();
            }

            if (IOUtility.projectFolderPath.TryMakeRelative(destination, out var relativeDestination))
            {
                destination = IOUtility.projectFolderPath.PathCombine(relativeDestination);
            }
            else
            {
                destination = IOUtility.projectFolderPath.PathCombine(destination);
            }

            destination = UnityEditor.EditorUtility.SaveFilePanel("Save object as", destination,
                "New " + obj.GetType().GetNiceName(), "asset");

            if (destination.IsNullOrEmpty())
            {
                Debug.LogWarning($"未选择保存路径，创建{selectedType}失败");

                Object.DestroyImmediate(obj);
                return;
            }

            if (obj.TryCreateAsset(destination) == false)
            {
                Object.DestroyImmediate(obj);
            }

            onCreated?.Invoke(obj as T);
        }

        #endregion
    }

    #endregion

    #region Scriptable Object Asset Selector

    public class ScriptableObjectAssetSelector<T> : OdinSelector<T>
        where T : ScriptableObject
    {
        private IEnumerable<T> assets;
        private Dictionary<T, string> paths = new();
        private bool supportMultiSelect;
        private Action<T> onSelectionConfirmed;

        #region Constructor

        public ScriptableObjectAssetSelector(IEnumerable<T> assets,
            Action<T> onSelectionConfirmed = null, bool supportMultiSelect = false)
        {
            this.assets = assets;
            this.supportMultiSelect = supportMultiSelect;
            this.onSelectionConfirmed = onSelectionConfirmed;

            SelectionConfirmed += OnSelectionConfirmed;
        }

        public ScriptableObjectAssetSelector(string folderPath,
            Action<T> onSelectionConfirmed = null, bool supportMultiSelect = false)
        {
            assets = folderPath.FindAssetsOfType<T>();
            foreach (var asset in assets)
            {
                var assetPath = asset.GetAssetPath();
                folderPath.TryMakeRelative(assetPath, out assetPath);
                paths.Add(asset, assetPath);
            }

            this.supportMultiSelect = supportMultiSelect;
            this.onSelectionConfirmed = onSelectionConfirmed;

            SelectionConfirmed += OnSelectionConfirmed;
        }

        #endregion

        #region Style

        protected override float DefaultWindowWidth() =>
            SelectorEditorUtility.DEFAULT_WIDE_POPUP_WIDTH;

        #endregion

        #region Selection Event

        private void OnSelectionConfirmed(IEnumerable<T> selection)
        {
            foreach (var scriptableObject in selection)
            {
                onSelectionConfirmed?.Invoke(scriptableObject);
            }
        }
        
        public override bool IsValidSelection(IEnumerable<T> collection)
        {
            return collection.Any();
        }

        #endregion

        #region Build Tree

        protected override void BuildSelectionTree(OdinMenuTree tree)
        {
            tree.Selection.SupportsMultiSelect = supportMultiSelect;
            tree.Config.DrawSearchToolbar = true;
            tree.Config.SelectMenuItemsOnMouseDown = true;
            tree.AddRange(assets, x => paths.TryGetValue(x, out var path) ? path : x.name)
                .AddThumbnailIcons();
        }

        #endregion
    }

    #endregion

    #region Type Selector

    public class TypeSelector : OdinSelector<Type>
    {
        private IEnumerable<Type> types;
        private Action<Type> onSelected;
        private bool supportsMultiSelect;
        private IReadOnlyDictionary<Type, string> typePaths = new Dictionary<Type, string>();

        private Type lastType;

        #region Extern Configuration

        public bool hideNamespaces { get; init; }

        public bool flattenTree { get; init; }
        
        public bool autoOrder { get; init; }

        public int autoExpandPathWhenChildCountLessThan { get; init; } = 1;

        #endregion

        #region Style

        public override string Title => null;
        
        protected override float DefaultWindowWidth() => SelectorEditorUtility.DEFAULT_WIDE_POPUP_WIDTH;

        #endregion

        #region Constructor

        public TypeSelector(IEnumerable<Type> types, Action<Type> onSelected = null, 
            bool supportsMultiSelect = false)
        {
            this.types = types ?? Type.EmptyTypes;
            this.onSelected = onSelected;
            this.supportsMultiSelect = supportsMultiSelect;
            SelectionConfirmed += OnSelectionConfirmed;
        }
        
        public TypeSelector(Type parentType, bool includingAbstract = false, bool includingGenericDefinition = false,
            Action<Type> onSelected = null, bool supportsMultiSelect = false)
        {
            types = parentType.GetDerivedClasses(true, includingGenericDefinition);
            if (includingAbstract == false)
            {
                types = types.Where(x => x.IsAbstract == false);
            }
            this.onSelected = onSelected;
            this.supportsMultiSelect = supportsMultiSelect;
            SelectionConfirmed += OnSelectionConfirmed;
        }

        public TypeSelector(IReadOnlyDictionary<Type, string> typePaths,
            Action<Type> onSelected = null, bool supportsMultiSelect = false)
        {
            types = typePaths.Keys;
            this.typePaths = typePaths;
            this.onSelected = onSelected;
            this.supportsMultiSelect = supportsMultiSelect;
            SelectionConfirmed += OnSelectionConfirmed;
        }

        #endregion

        #region Selection Event

        private void OnSelectionConfirmed(IEnumerable<Type> selection)
        {
            if (onSelected != null)
            {
                foreach (var type in selection)
                {
                    onSelected(type);
                }
            }
        }
        
        public override bool IsValidSelection(IEnumerable<Type> collection)
        {
            return collection.Any();
        }

        public override void SetSelection(Type selected)
        {
            base.SetSelection(selected);
            
            SelectionTree.Selection.SelectMany(x => x.GetParentMenuItemsRecursive(false))
                .Examine(x => x.Toggled = true);
        }

        #endregion

        #region Build Tree

        protected sealed override void BuildSelectionTree(OdinMenuTree tree)
        {
            if (autoOrder)
            {
                types = OrderTypes(types);
            }

            tree.Config.UseCachedExpandedStates = true;
            tree.DefaultMenuStyle.NotSelectedIconAlpha = 1f;
            tree.Config.SelectMenuItemsOnMouseDown = true;

            foreach (Type type in types)
            {
                string niceName = type.GetNiceName();
                string typeNamePath = GetTypeNamePath(type);
                
                OdinMenuItem odinMenuItem = tree.AddObjectAtPath(typeNamePath, type)
                    .Last();
                
                if (niceName == typeNamePath)
                {
                    odinMenuItem.SearchString = typeNamePath;
                }
                else
                {
                    odinMenuItem.SearchString = niceName + "|" + typeNamePath;
                }
                
                if (flattenTree && type.Namespace != null && !hideNamespaces)
                {
                    odinMenuItem.OnDrawItem += x =>
                        GUI.Label(x.Rect.Padding(10f, 0.0f).AlignCenterY(16f),
                            type.Namespace, SirenixGUIStyles.RightAlignedGreyMiniLabel);
                }
            }

            tree.EnumerateTree(item => item.Value != null, false)
                .AddThumbnailIcons();

            if (autoExpandPathWhenChildCountLessThan >= 0)
            {
                tree.EnumerateTree(item =>
                {
                    if (item.ChildMenuItems.Count <= autoExpandPathWhenChildCountLessThan)
                    {
                        item.Toggled = true;
                    }
                });
            }

            tree.Selection.SupportsMultiSelect = supportsMultiSelect;
            tree.Selection.SelectionChanged += t =>
            {
                Type type = SelectionTree.Selection
                    .Select(x => x.Value)
                    .OfType<Type>().LastOrDefault();
                if ((object)type == null)
                    type = lastType;
                lastType = type;
            };
        }

        #endregion

        #region Type Utility

        private static IEnumerable<Type> OrderTypes(IEnumerable<Type> types)
        {
            return types.OrderByDescending(x => x.Namespace.IsNullOrEmptyAfterTrim())
                .ThenBy(x => x.Namespace)
                .ThenBy(x => x.Name);
        }

        private string GetTypeNamePath(Type type)
        {
            if (typePaths.TryGetValue(type, out var typeNamePath))
            {
                return typeNamePath;
            }
            
            typeNamePath = type.GetNiceName();
            
            if (!flattenTree && !string.IsNullOrEmpty(type.Namespace) && !hideNamespaces)
            {
                char separator = flattenTree ? '.' : '/';
                typeNamePath = type.Namespace + separator + typeNamePath;
            }

            return typeNamePath;
        }

        #endregion

        #region Type Info

        [OnInspectorGUI]
        private void ShowTypeInfo()
        {
            string typeName = "";
            string assemblyName = "";
            string baseTypeName = "";
            int height = 16;
            Rect rect = GUILayoutUtility.GetRect(0.0f, height * 3 + 8).Padding(10f, 4f)
                .AlignTop(height);
            int width = 75;
            if (lastType != null)
            {
                typeName = lastType.GetNiceFullName();
                assemblyName = lastType.Assembly.GetName().Name;
                baseTypeName = lastType.BaseType == null
                    ? ""
                    : lastType.BaseType.GetNiceFullName();
            }

            GUIStyle alignedGreyMiniLabel = SirenixGUIStyles.LeftAlignedGreyMiniLabel;
            GUI.Label(rect.AlignLeft(width), "Type Name", alignedGreyMiniLabel);
            GUI.Label(rect.AlignRight(rect.width - width), typeName, alignedGreyMiniLabel);
            rect.y += height;
            GUI.Label(rect.AlignLeft(width), "Base Type", alignedGreyMiniLabel);
            GUI.Label(rect.AlignRight(rect.width - width), baseTypeName, alignedGreyMiniLabel);
            rect.y += height;
            GUI.Label(rect.AlignLeft(width), "Assembly", alignedGreyMiniLabel);
            GUI.Label(rect.AlignRight(rect.width - width), assemblyName, alignedGreyMiniLabel);
        }

        #endregion
    }

    #endregion

    #region Command Selector

    public class CommandSelector : OdinSelector<string>, IEnumerable
    {
        private struct CommandInfo
        {
            public string name;
            public string category;
            public SdfIconType icon;
            public Action command;
        }
        
        private Dictionary<string, CommandInfo> commandInfos = new();

        #region Constructor

        public CommandSelector() : base()
        {
            SelectionConfirmed += OnSelectionConfirmed;
        }

        #endregion

        #region Style

        protected override float DefaultWindowWidth() =>
            SelectorEditorUtility.DEFAULT_NARROW_POPUP_WIDTH;

        #endregion

        #region Command Info Add

        public void Add(string name, Action command)
        {
            commandInfos.Add(name, new CommandInfo
            {
                name = name,
                command = command
            });
        }
        
        public void Add(string name, SdfIconType icon, Action command)
        {
            commandInfos.Add(name, new CommandInfo
            {
                name = name,
                icon = icon,
                command = command
            });
        }
        
        public void Add(string name, string category, Action command)
        {
            commandInfos.Add(name, new CommandInfo
            {
                name = name,
                category = category,
                command = command
            });
        }
        
        public void Add(string name, string category, SdfIconType icon, Action command)
        {
            commandInfos.Add(name, new CommandInfo
            {
                name = name,
                category = category,
                icon = icon,
                command = command
            });
        }

        #endregion

        #region Selection Event
        
        public override bool IsValidSelection(IEnumerable<string> collection)
        {
            return collection.Any();
        }
        
        private void OnSelectionConfirmed(IEnumerable<string> selection)
        {
            foreach (var commandName in selection)
            {
                if (commandInfos.TryGetValue(commandName, out var commandInfo))
                {
                    commandInfo.command?.Invoke();
                }
            } 
        }

        #endregion

        #region Build Tree

        protected override void BuildSelectionTree(OdinMenuTree tree)
        {
            tree.Config.UseCachedExpandedStates = true;
            tree.Config.SelectMenuItemsOnMouseDown = true;

            foreach (var commandInfo in commandInfos.Values)
            {
                var path = commandInfo.name;

                if (commandInfo.category.IsNullOrEmpty() == false)
                {
                    path = commandInfo.category + "/" + path;
                }
                
                tree.Add(path, commandInfo.name, commandInfo.icon);
            }
        }

        #endregion

        #region Enumerable

        public IEnumerator GetEnumerator()
        {
            return commandInfos.Values.GetEnumerator();
        }

        #endregion
    }

    #endregion

    #region String Selector

    public class StringSelector : OdinSelector<string>
    {
        private IEnumerable<string> strings;
        private Action<string> onSelected;
        private bool supportsMultiSelect;
        private Dictionary<string, string> stringPaths = new();

        #region Constructor

        public StringSelector(IEnumerable<string> strings, Action<string> onSelected = null, 
            bool supportsMultiSelect = false) : base()
        {
            this.strings = strings;
            this.onSelected = onSelected;
            this.supportsMultiSelect = supportsMultiSelect;
            
            SelectionConfirmed += OnSelectionConfirmed;
        }

        #endregion

        #region Style

        protected override float DefaultWindowWidth() =>
            SelectorEditorUtility.DEFAULT_NARROW_POPUP_WIDTH;

        #endregion

        #region Selection Event
        
        public override bool IsValidSelection(IEnumerable<string> collection)
        {
            return collection.Any();
        }
        
        private void OnSelectionConfirmed(IEnumerable<string> selection)
        {
            if (onSelected != null)
            {
                foreach (var type in selection)
                {
                    onSelected(type);
                }
            }
        }

        #endregion

        #region Build Tree

        protected override void BuildSelectionTree(OdinMenuTree tree)
        {
            tree.Config.UseCachedExpandedStates = true;
            tree.Config.SelectMenuItemsOnMouseDown = true;
            tree.Selection.SupportsMultiSelect = supportsMultiSelect;

            foreach (var str in strings)
            {
                var path = CollectionExtensions.GetValueOrDefault(stringPaths, str, str);

                tree.Add(path, str);
            }
        }

        #endregion
    }

    #endregion
}

#endif