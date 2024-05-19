#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using VMFramework.Core;
using VMFramework.Core.Editor;
using VMFramework.Editor;
using VMFramework.Editor.GameEditor;
using VMFramework.GameLogicArchitecture.Editor;
using EditorUtility = UnityEditor.EditorUtility;

namespace VMFramework.GameLogicArchitecture
{
    public partial class GameCoreSettingBaseFile
    {
        [LabelText("游戏编辑器"), TabGroup(TAB_GROUP_NAME, EDITOR_EXTENSION_CATEGORY)]
        [Required]
        internal GameEditorGeneralSetting gameEditorGeneralSetting;

        [LabelText("带颜色的层级"), TabGroup(TAB_GROUP_NAME, EDITOR_EXTENSION_CATEGORY)]
        [Required]
        public ColorfulHierarchyGeneralSetting colorfulHierarchyGeneralSetting;

        [LabelText("层级组件图标"), TabGroup(TAB_GROUP_NAME, EDITOR_EXTENSION_CATEGORY)]
        [Required]
        public HierarchyComponentIconGeneralSetting hierarchyComponentIconGeneralSetting;
        
        [LabelText("图片导入设置"), TabGroup(TAB_GROUP_NAME, EDITOR_EXTENSION_CATEGORY)]
        [Required]
        public TextureImporterGeneralSetting textureImporterGeneralSetting;
        
        [LabelText("游戏预制体包装器设置"), TabGroup(TAB_GROUP_NAME, CORE_CATEGORY)]
        public GamePrefabWrapperGeneralSetting gamePrefabWrapperGeneralSetting;
        
        public static void CheckGlobal()
        {
            var derivedClasses = typeof(GameCoreSettingBaseFile)
                .GetDerivedClasses(false, false).ToList();

            switch (derivedClasses.Count)
            {
                case 0:
                    typeof(GameCoreSettingBaseFile).FindOrCreateScriptableObject(
                        ConfigurationPath.GAME_CORE_SETTING_FILE_PATH, defaultName);
                    break;
                case > 2:
                    Debug.LogWarning($"不允许有多个类继承{typeof(GameCoreSettingBaseFile)}，" +
                                     $"继承的类如下：{derivedClasses.ToString(",")}");
                    derivedClasses[0]
                        .FindOrCreateScriptableObject(ConfigurationPath.GAME_CORE_SETTING_FILE_PATH,
                            defaultName);
                    break;
                case 1:
                    derivedClasses[0]
                        .FindOrCreateScriptableObject(ConfigurationPath.GAME_CORE_SETTING_FILE_PATH,
                            defaultName);
                    break;
            }
        }
        
        [Button("自动寻找并创建通用配置文件", ButtonSizes.Medium), 
         TabGroup(TAB_GROUP_NAME, DEBUGGING_CATEGORY)]
        private void AutoFindSettingAndCreate()
        {
            foreach (var fieldInfo in GetType().GetFields())
            {
                if (fieldInfo.IsPublic && fieldInfo.FieldType.IsDerivedFrom<ScriptableObject>(false))
                {
                    var result =
                        fieldInfo.FieldType.FindOrCreateScriptableObject(
                            ConfigurationPath.GENERAL_SETTING_DIRECTORY_PATH, fieldInfo.FieldType.Name);

                    fieldInfo.SetValue(this, result);
                }
            }

            EditorUtility.SetDirty(this);

            if (EditorApplication.isUpdating == false)
            {
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
        }

        [Button("自动寻找通用配置文件", ButtonSizes.Medium),
         TabGroup(TAB_GROUP_NAME, DEBUGGING_CATEGORY)]
        public void AutoFindSetting()
        {
            foreach (var fieldInfo in GetType().GetFields())
            {
                if (fieldInfo.IsPublic &&
                    fieldInfo.FieldType.IsDerivedFrom<ScriptableObject>(false))
                {
                    var result =
                        fieldInfo.FieldType.FindScriptableObject();

                    if (result != null)
                    {
                        fieldInfo.SetValue(this, result);
                    }
                }
            }

            EditorUtility.SetDirty(this);

            if (EditorApplication.isUpdating == false)
            {
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
        }

        [Button("打开配置文件单例索引ScriptableObject的基类"),
         TabGroup(TAB_GROUP_NAME, DEBUGGING_CATEGORY)]
        private void OpenGameCoreSettingBaseFileScript()
        {
            typeof(GameCoreSettingBaseFile).OpenScriptOfType();
        }

        [Button("打开配置文件单例索引基类"),
         TabGroup(TAB_GROUP_NAME, DEBUGGING_CATEGORY)]
        private void OpenGameCoreSettingBaseScript()
        {
            typeof(GameCoreSettingBase).OpenScriptOfType();
        }

        [Button, TabGroup(TAB_GROUP_NAME, DEBUGGING_CATEGORY)]
        private IReadOnlyList<Object> FindMissingScriptSetting()
        {
            var result = ConfigurationPath.GENERAL_SETTING_DIRECTORY_PATH.FindAssetsOfType<Object>().ToList();

            result.RemoveAll(obj => obj != null);

            return result;
        }

        [Button, TabGroup(TAB_GROUP_NAME, DEBUGGING_CATEGORY)]
        private void RemoveMissingScriptSetting()
        {
            string[] guids = AssetDatabase.FindAssets("",
                new[] { ConfigurationPath.GENERAL_SETTING_DIRECTORY_PATH });

            foreach (var guid in guids)
            {
                string assetPath = AssetDatabase.GUIDToAssetPath(guid);
                var obj = AssetDatabase.LoadAssetAtPath<Object>(assetPath);

                if (obj == null)
                {
                    AssetDatabase.DeleteAsset(assetPath);
                }
            }

            AssetDatabase.Refresh();
        }

        public override void CheckSettingsGUI()
        {
            AutoFindSetting();

            base.CheckSettingsGUI();
        }
    }
}
#endif