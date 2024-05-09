#if UNITY_EDITOR
using System.Linq;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;
using VMFramework.GameLogicArchitecture;

namespace VMFramework.OdinExtensions
{
    public class GamePrefabIDContextMenuDrawer : OdinValueDrawer<string>, IDefinesGenericMenuItems
    {
        protected override void DrawPropertyLayout(GUIContent label)
        {
            CallNextDrawer(label);
        }

        public void PopulateGenericMenu(InspectorProperty property, GenericMenu genericMenu)
        {
            var value = Property.ValueEntry.WeakSmartValue;

            if (value is not string id)
            {
                return;
            }

            if (GamePrefabManager.TryGetGamePrefab(id, out var gamePrefab) == false)
            {
                return;
            }

            var wrappers = GamePrefabWrapperQuery.GetGamePrefabWrapper(gamePrefab).ToList();

            if (wrappers.Count == 0)
            {
                return;
            }
            
            genericMenu.AddSeparator("");
            
            genericMenu.AddItem(new GUIContent("选中GamePrefabWrapper"), false, () =>
            {
                Selection.activeObject = wrappers[0];
            });
            
            genericMenu.AddItem(new GUIContent("打开GamePrefabWrapper"), false, () =>
            {
                GUIHelper.OpenInspectorWindow(wrappers[0]);
            });
        }
    }
}
#endif