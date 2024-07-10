#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;
using VMFramework.Core;
using VMFramework.Core.Linq;
using VMFramework.GameLogicArchitecture;
using VMFramework.GameLogicArchitecture.Editor;
using VMFramework.Procedure.Editor;

namespace VMFramework.Editor.GameEditor
{
    internal sealed class GameEditor : OdinMenuEditorWindow
    {
        private static readonly List<IGameEditorContextMenuModifier> contextMenuModifiers = new();
        
        static GameEditor()
        {
            contextMenuModifiers.AddInstancesOfDerivedClasses(false);
        }

        [MenuItem(UnityMenuItemNames.VMFRAMEWORK + GameEditorNames.GAME_EDITOR + " #G", false, 100)]
        private static void OpenWindow()
        {
            var window = CreateWindow<GameEditor>(GameEditorNames.GAME_EDITOR);
            window.position = GUIHelper.GetEditorWindowRect().AlignCenter(800, 600);
        }

        protected override OdinMenuTree BuildMenuTree()
        {
            if (EditorInitializer.isInitialized == false && Application.isPlaying == false)
            {
                var loadingPreviewTree = new OdinMenuTree(true)
                {
                    { "Loading...", new GameEditorLoadingPreview() }
                };
                
                loadingPreviewTree.Selection.Add(loadingPreviewTree.RootMenuItem.ChildMenuItems[0]);
                
                return loadingPreviewTree;
            }

            OdinMenuTree tree = new(true);

            var nodesInfo = new Dictionary<IGameEditorMenuTreeNode, TreeNodeInfo>();
            
            var nodes = new List<IGameEditorMenuTreeNode>();
            
            var leftNodes = new Queue<IGameEditorMenuTreeNode>();
            
            foreach (var globalSettingFile in GlobalSettingFileEditorManager.GetGlobalSettings())
            {
                if (globalSettingFile is IGameEditorMenuTreeNode node)
                {
                    leftNodes.Enqueue(node);
                    nodes.Add(node);
                    nodesInfo.Add(node, new TreeNodeInfo()
                    {
                        parent = null,
                        provider = null
                    });
                }
            }

            while (leftNodes.Count > 0)
            {
                var leftNode = leftNodes.Dequeue();

                if (leftNode.IsUnityNull())
                {
                    continue;
                }

                if (leftNode is not IGameEditorMenuTreeNodesProvider provider)
                {
                    continue;
                }

                foreach (var child in provider.GetAllMenuTreeNodes())
                {
                    if (nodesInfo.TryGetValue(child, out var existingInfo))
                    {
                        Debug.LogWarning(
                            $"{child.name} has already provided by {existingInfo.provider.name}." +
                            $"Cannot be provided by {provider.name} again!");
                        continue;
                    }

                    leftNodes.Enqueue(child);
                    nodes.Add(child);
                    
                    var parent = child.parentNode;
                    if (parent == null && provider is IGameEditorMenuTreeNode providerNode)
                    {
                        parent = providerNode;
                    }
                    
                    nodesInfo.Add(child, new()
                    {
                        parent = parent,
                        provider = provider
                    });
                }
            }

            foreach (var (node, info) in nodesInfo)
            {
                if (info.parent == null)
                {
                    continue;
                }
                
                if (nodesInfo.ContainsKey(info.parent) == false)
                {
                    Debug.LogWarning($"The parent of {node.name} is not found!" +
                                     $"It doesn't belong to any provider.");
                    info.parent = null;
                }
            }

            foreach (var node in nodes)
            {
                var path = node.name;
                
                foreach (var parent in node.TraverseToRoot(false, node => nodesInfo[node].parent))
                {
                    path = parent.name + "/" + path;
                }

                if (node.isVisible == false)
                {
                    continue;
                }
                
                tree.Add(path, node, node.icon);
            }

            tree.DefaultMenuStyle.IconSize = 24.00f;
            tree.Config.DrawSearchToolbar = true;

            tree.EnumerateTree().Examine(AddRightClickContextMenu);

            return tree;
        }

        private void AddRightClickContextMenu(OdinMenuItem menuItem)
        {
            menuItem.OnDrawItem += OnDrawRightClickContextMenu;
        }

        private void OnDrawRightClickContextMenu(OdinMenuItem menuItem)
        {
            if (menuItem.Value is not IGameEditorContextMenuProvider contextMenuProvider)
            {
                return;
            }

            if (Event.current.type != EventType.MouseDown || Event.current.button != 1 ||
                menuItem.Rect.Contains(Event.current.mousePosition) == false)
            {
                return;
            }

            GenericMenu menu = new GenericMenu();

            foreach (var config in contextMenuProvider.GetMenuItems())
            {
                menu.AddItem(new GUIContent(config.name, config.tooltip), false,
                    () => config.onClick?.Invoke());
            }

            var selectedNodes = MenuTree.Selection.SelectedValues.Cast<IGameEditorMenuTreeNode>()
                .WhereNotNull().ToList();

            foreach (var modifier in contextMenuModifiers)
            {
                modifier.ModifyContextMenu(contextMenuProvider, selectedNodes, menu);
            }

            menu.ShowAsContext();

            Event.current.Use();
        }

        protected override void OnBeginDrawEditors()
        {
            if (MenuTree?.Selection == null)
            {
                return;
            }

            var selected = MenuTree.Selection.FirstOrDefault();
            var toolbarHeight = MenuTree.Config.SearchToolbarHeight;

            SirenixEditorGUI.BeginHorizontalToolbar(toolbarHeight);
            
            if (selected is not { Value: not null })
            {
                SirenixEditorGUI.EndHorizontalToolbar();
                return;
            }
            
            GUILayout.Label(selected.Name);

            if (selected.Value is not IGameEditorToolbarProvider toolBarProvider)
            {
                SirenixEditorGUI.EndHorizontalToolbar();
                return;
            }
            
            var tree = new StringPathTree<ToolbarButtonConfig>();
                        
            foreach (var buttonConfig in toolBarProvider.GetToolbarButtons())
            {
                tree.Add(buttonConfig.path, buttonConfig);
            }

            foreach (var buttonNode in tree.root.children.Values)
            {
                if (SirenixEditorGUI.ToolbarButton(new GUIContent(buttonNode.pathPart,
                        buttonNode.data.tooltip)))
                {
                    if (buttonNode.children.Count <= 0)
                    {
                        buttonNode.data.onClick?.Invoke();
                    }
                    else
                    {
                        GenericMenu menu = new GenericMenu();

                        Action action = null;
                        foreach (var leaf in buttonNode.GetAllLeaves(true))
                        {
                            menu.AddItem(new GUIContent(leaf.pathPart, leaf.data.tooltip), false, () =>
                            {
                                leaf.data.onClick?.Invoke();
                            });
                            
                            action = leaf.data.onClick;
                        }

                        if (menu.GetItemCount() > 1)
                        {
                            menu.ShowAsContext();
                        }
                        else
                        {
                            action?.Invoke();
                        }
                    }
                }
            }
            
            SirenixEditorGUI.EndHorizontalToolbar();
        }

        private void OnProjectChange()
        {
            GamePrefabGeneralSettingUtility.RefreshAllInitialGamePrefabWrappers();
            
            ForceMenuTreeRebuild();
        }
    }
}
#endif