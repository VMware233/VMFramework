#if UNITY_EDITOR
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

            if (GamePrefabWrapperQuery.TryGetGamePrefabWrapper(id, out var wrapper) == false)
            {
                return;
            }
            
            genericMenu.AddSeparator("");
            
            genericMenu.AddItem(new GUIContent("选中GamePrefabWrapper"), false, () =>
            {
                Selection.activeObject = wrapper;
            });
            
            genericMenu.AddItem(new GUIContent("打开GamePrefabWrapper"), false, () =>
            {
                GUIHelper.OpenInspectorWindow(wrapper);
            });
        }
    }
}
#endif