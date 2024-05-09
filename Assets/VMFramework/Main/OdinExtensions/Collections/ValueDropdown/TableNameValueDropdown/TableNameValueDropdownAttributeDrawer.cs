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
    public class TableNameValueDropdownAttributeDrawer :
        GeneralValueDropdownAttributeDrawer<TableNameValueDropdownAttribute>, IDefinesGenericMenuItems
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
                return;
            }
            
            var stringTableCollection = LocalizationEditorSettings.GetStringTableCollection(tableName);

            if (stringTableCollection == null)
            {
                return;
            }
            
            genericMenu.AddItem(new GUIContent("打开表"), false, () =>
            {
                LocalizationTablesWindow.ShowWindow(stringTableCollection);
            });
        }
    }
}
#endif