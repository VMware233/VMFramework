#if UNITY_EDITOR
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace VMFramework.OdinExtensions
{
    [DrawerPriority(DrawerPriorityLevel.SuperPriority)]
    public class FloatContextMenuDrawer : OdinValueDrawer<float>, IDefinesGenericMenuItems
    {
        protected override void DrawPropertyLayout(GUIContent label)
        {
            CallNextDrawer(label);
        }

        void IDefinesGenericMenuItems.PopulateGenericMenu(InspectorProperty property, GenericMenu genericMenu)
        {
            genericMenu.AddSeparator("");
            
            genericMenu.AddItem(new GUIContent($"设为{float.MaxValue}"), false, () =>
            {
                ValueEntry.SmartValue = float.MaxValue;
            });
            
            genericMenu.AddItem(new GUIContent($"设为{float.MinValue}"), false, () =>
            {
                ValueEntry.SmartValue = float.MinValue;
            });
        }
    }
}

#endif