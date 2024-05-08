#if UNITY_EDITOR
using VMFramework.Core;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace VMFramework.OdinExtensions
{
    [DrawerPriority(DrawerPriorityLevel.SuperPriority)]
    public class StringContextMenuDrawer : OdinValueDrawer<string>, IDefinesGenericMenuItems
    {
        protected override void DrawPropertyLayout(GUIContent label)
        {
            CallNextDrawer(label);
        }

        void IDefinesGenericMenuItems.PopulateGenericMenu(InspectorProperty property,
            GenericMenu genericMenu)
        {
            if (property.ValueEntry.WeakSmartValue is not string str)
            {
                return;
            }

            if (property.GetAttribute<ValueDropdownAttribute>() != null)
            {
                return;
            }

            if (str.IsEmptyAfterTrim())
            {
                return;
            }

            genericMenu.AddSeparator("");

            genericMenu.AddItem(new GUIContent("帕斯卡命名"), false, () =>
            {
                property.ValueEntry.WeakSmartValue = str.ToPascalCase(" ");
            });

            genericMenu.AddItem(new GUIContent("下划线命名"), false, () =>
            {
                property.ValueEntry.WeakSmartValue = str.ToSnakeCase();
            });

            if (str.Contains(' '))
            {
                genericMenu.AddItem(new GUIContent("清除空格"), false, () =>
                {
                    property.ValueEntry.WeakSmartValue = str.Replace(" ", "");
                });
            }
        }
    }
}

#endif