#if UNITY_EDITOR
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEditor.Localization;
using UnityEditor.Localization.UI;
using UnityEngine;
using VMFramework.Core;
using VMFramework.Localization;

namespace VMFramework.OdinExtensions
{
    public class TableNameAttributeDrawer : GeneralValueDropdownAttributeDrawer<TableNameAttribute>, 
        IDefinesGenericMenuItems
    {
        protected override IEnumerable<ValueDropdownItem> GetValues()
        {
            return LocalizationEditorManager.GetTableNameList();
        }

        void IDefinesGenericMenuItems.PopulateGenericMenu(InspectorProperty property, GenericMenu genericMenu)
        {
            var value = Property.ValueEntry.WeakSmartValue;

            if (value is not string tableName)
            {
                return;
            }

            if (tableName.IsNullOrWhiteSpace())
            {
                ShowWindow(null);
                return;
            }
            
            var stringTableCollection = LocalizationEditorSettings.GetStringTableCollection(tableName);

            ShowWindow(stringTableCollection);
            
            return;

            void ShowWindow(LocalizationTableCollection collection)
            {
                if (collection == null)
                {
                    genericMenu.AddItem(new GUIContent("打开表格编辑器"), false,
                        LocalizationTablesWindow.ShowWindow);

                    genericMenu.AddItem(new GUIContent("创建新表"), false,
                        LocalizationTablesWindow.ShowTableCreator);
                }
                else
                {
                    genericMenu.AddItem(new GUIContent("打开表"), false, () =>
                    {
                        LocalizationTablesWindow.ShowWindow(collection);
                    });
                }
            }
        }
    }
}
#endif