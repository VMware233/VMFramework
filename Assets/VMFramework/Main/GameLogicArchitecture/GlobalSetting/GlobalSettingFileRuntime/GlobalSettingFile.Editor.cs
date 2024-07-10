#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using VMFramework.Core;
using VMFramework.Core.Editor;
using VMFramework.GameLogicArchitecture.Editor;

namespace VMFramework.GameLogicArchitecture
{
    public partial class GlobalSettingFile
    {
        [Button(ButtonSizes.Medium), TabGroup(TAB_GROUP_NAME, DEBUGGING_CATEGORY)]
        public void AutoFindSettings()
        {
            foreach (var fieldInfo in GetAllGeneralSettingsFields())
            {
                if (fieldInfo.GetValue(this).IsUnityNull() == false)
                {
                    continue;
                }

                var result = fieldInfo.FieldType.FindScriptableObject();

                if (result != null)
                {
                    fieldInfo.SetValue(this, result);
                }
            }

            this.EnforceSave();
        }

        [Button(ButtonSizes.Medium), TabGroup(TAB_GROUP_NAME, DEBUGGING_CATEGORY)]
        public void AutoFindAndCreateSettings()
        {
            EditorSetting.AssertGlobalSettingFileIsLoaded();

            var folderPath = EditorSetting.generalSettingsAssetFolderPath;
            
            foreach (var fieldInfo in GetAllGeneralSettingsFields())
            {
                if (fieldInfo.GetValue(this).IsUnityNull() == false)
                {
                    continue;
                }
                
                var fileName = fieldInfo.FieldType.Name;

                var path = folderPath.PathCombine(fileName).MakeAssetPath(".asset");

                var result = fieldInfo.FieldType.FindOrCreateScriptableObjectAtPath(path);

                if (result != null)
                {
                    fieldInfo.SetValue(this, result);
                }
            }

            this.EnforceSave();
        }

        [Button(ButtonSizes.Medium), TabGroup(TAB_GROUP_NAME, DEBUGGING_CATEGORY)]
        public List<IGeneralSetting> GetAllGeneralSettingsDebug()
        {
            return GetAllGeneralSettings().ToList();
        }
    }
}
#endif