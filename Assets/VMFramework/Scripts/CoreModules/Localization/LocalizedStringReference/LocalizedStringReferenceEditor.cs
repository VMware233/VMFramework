#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEditor.Localization;
using UnityEditor.Localization.UI;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;
using VMFramework.Core;
using VMFramework.Core.Editor;

namespace VMFramework.Localization
{
    public partial class LocalizedStringReference
    {
        #region Init

        protected override void OnInspectorInit()
        {
            base.OnInspectorInit();
            
            OnTableNameChanged();
        }

        #endregion

        #region Default Value

        private bool ExistsDefaultValue() => defaultValue.IsNullOrEmptyAfterTrim() == false;

        #endregion
        
        #region Table

        [HideLabel, HorizontalGroup(TABLE_HORIZONTAL_GROUP)]
        [ShowInInspector]
        private StringTableCollection stringTableCollection;

        private void OnTableNameChanged()
        {
            if (tableName.IsNullOrEmpty())
            {
                stringTableCollection = null;
                return;
            }

            stringTableCollection = LocalizationEditorSettings.GetStringTableCollection(tableName);
        }

        private bool ExistsTable()
        {
            return stringTableCollection!= null;
        }

        [Button("打开表"), HorizontalGroup(TABLE_HORIZONTAL_GROUP, width: 100)]
        [ShowIf(nameof(ExistsTable))]
        private void OpenTableEditor()
        {
            if (stringTableCollection == null)
            {
                OnTableNameChanged();

                if (stringTableCollection == null)
                {
                    return;
                }
            }
            
            LocalizationTablesWindow.ShowWindow(stringTableCollection);
        }

        #endregion

        #region Key

        private bool ExistsKey()
        {
            if (stringTableCollection == null)
            {
                return false;
            }

            if (key.IsNullOrEmptyAfterTrim())
            {
                return false;
            }

            var table = stringTableCollection.StringTables.FirstOrDefault();

            if (table == null)
            {
                return false;
            }

            return table.GetEntry(key) != null;
        }

        #region Select Key

        [Button, HorizontalGroup(KEY_TOOL_HORIZONTAL_GROUP)]
        [ShowIf(nameof(ExistsTable))]
        private void SelectKey()
        {
            if (stringTableCollection == null)
            {
                return;
            }
            
            var keys = new List<string>();

            foreach (var row in stringTableCollection.GetRowEnumerator())
            {
                var tableEntries = row.TableEntries;

                if (tableEntries.Length == 0)
                {
                    continue;
                }
                
                var entry = tableEntries[0];
                
                keys.Add(entry.Key);
            }

            new StringSelector(keys, selectedKey => key = selectedKey).Show();
        }

        #endregion

        #region Create New Key

        [Button, HorizontalGroup(KEY_TOOL_HORIZONTAL_GROUP)]
        [HideIf(nameof(ExistsKey))]
        public void CreateNewKey()
        {
            if (key.IsNullOrEmptyAfterTrim())
            {
                Debug.LogWarning("Key cannot be empty.");
                return;
            }
            
            OnTableNameChanged();

            if (stringTableCollection == null)
            {
                Debug.LogWarning("No table selected.");
                return;
            }

            if (ExistsKey())
            {
                Debug.LogWarning("Key already exists in table.");
                return;
            }

            foreach (var stringTable in stringTableCollection.StringTables)
            {
                stringTable.AddEntry(key, "");
                
                stringTable.EnforceSave();
            }
        }

        #endregion

        #region Set Key Value By Default

        [Button, HorizontalGroup(KEY_TOOL_HORIZONTAL_GROUP)]
        [ShowIf(nameof(ExistsDefaultValue))]
        public void SetKeyValueByDefault()
        {
            SetKeyValueInEditor(defaultValue, false);
        }

        #endregion
        
        #region Set Key Value In Editor

        public void SetKeyValueInEditor(string value, bool replace)
        {
            if (stringTableCollection == null)
            {
                Debug.LogWarning("No table selected.");
                return;
            }

            if (key.IsNullOrEmptyAfterTrim())
            {
                Debug.LogWarning("Key cannot be empty.");
                return;
            }

            foreach (var stringTable in stringTableCollection.StringTables)
            {
                var entry = stringTable.GetEntry(key);

                if (entry != null)
                {
                    if (entry.GetLocalizedString().IsNullOrEmptyAfterTrim() == false && replace == false)
                    {
                        continue;
                    }
                }

                stringTable.AddEntry(key, value);
                
                stringTable.SetEditorDirty();
            }
            
            stringTableCollection.SetEditorDirty();
            stringTableCollection.SharedData.SetEditorDirty();
            
            AssetDatabase.SaveAssets();
        }

        #endregion

        #endregion

        #region To String

        private string ToStringEditor()
        {
            StringTable table;

            if (LocalizationSettings.ProjectLocale == null)
            {
                var collection = LocalizationEditorSettings.GetStringTableCollection(tableName);

                if (collection == null)
                {
                    table = null;
                }
                else
                {
                    table = collection.StringTables.First();
                }
            }
            else
            {
                table = LocalizationSettings.StringDatabase.GetTable(tableName,
                    LocalizationSettings.ProjectLocale);
            }

            if (table == null)
            {
                return $"(Table not found: {tableName})" + defaultValue;
            }
                
            if (key.IsNullOrEmptyAfterTrim())
            {
                return $"Key cannot be empty." + defaultValue;
            }
                
            var entryEditor = table.GetEntry(key);

            if (entryEditor == null)
            {
                return $"(Key not found: {key})" + defaultValue;
            }
                
            return entryEditor.GetLocalizedString();
        }

        #endregion
    }
}
#endif