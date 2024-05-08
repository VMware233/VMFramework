#if UNITY_EDITOR
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace VMFramework.OdinExtensions
{
    [DrawerPriority(DrawerPriorityLevel.SuperPriority)]
    public class IntContextMenuDrawer : OdinValueDrawer<int>, IDefinesGenericMenuItems
    {
        protected override void DrawPropertyLayout(GUIContent label)
        {
            CallNextDrawer(label);
        }

        public void PopulateGenericMenu(InspectorProperty property, GenericMenu genericMenu)
        {
            genericMenu.AddSeparator("");
            
            genericMenu.AddItem(new GUIContent($"设为{int.MaxValue}"), false, () =>
            {
                ValueEntry.SmartValue = int.MaxValue;
            });
            
            genericMenu.AddItem(new GUIContent($"设为{int.MinValue}"), false, () =>
            {
                ValueEntry.SmartValue = int.MinValue;
            });
        }
    }
}

#endif