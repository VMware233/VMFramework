#if UNITY_EDITOR && ODIN_INSPECTOR
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;
using VMFramework.Core.Editor;
using VMFramework.GameLogicArchitecture.Editor;

namespace VMFramework.OdinExtensions
{
    internal sealed class GamePrefabIDContextMenuDrawer : OdinValueDrawer<string>, IDefinesGenericMenuItems
    {
        protected override void DrawPropertyLayout(GUIContent label)
        {
            CallNextDrawer(label);
        }

        void IDefinesGenericMenuItems.PopulateGenericMenu(InspectorProperty property, GenericMenu genericMenu)
        {
            var value = Property.ValueEntry.WeakSmartValue;

            if (value is not string id)
            {
                return;
            }

            if (GamePrefabWrapperQueryTools.TryGetGamePrefabWrapper(id, out var wrapper) == false)
            {
                return;
            }
            
            genericMenu.AddSeparator();
            
            genericMenu.AddItem("Select GamePrefabWrapper", () =>
            {
                Selection.activeObject = wrapper;
            });
            
            genericMenu.AddItem("Open GamePrefabWrapper", () =>
            {
                GUIHelper.OpenInspectorWindow(wrapper);
            });
        }
    }
}
#endif