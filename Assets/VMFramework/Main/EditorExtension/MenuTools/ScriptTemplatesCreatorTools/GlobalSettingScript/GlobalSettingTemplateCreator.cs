#if UNITY_EDITOR
using UnityEditor;
using VMFramework.Core;
using VMFramework.Core.Editor;

namespace VMFramework.Editor
{
    public static class GlobalSettingTemplateCreator
    {
        private static readonly GlobalSettingScriptCreationPostProcessor settingPostProcessor = new();
        private static readonly GlobalSettingFileScriptCreationPostProcessor settingFilePostProcessor = new();

        [MenuItem(UnityMenuItemNames.ASSETS_CREATION_VMFRAMEWORK + nameof(CreateGlobalSettingScript))]
        private static void CreateGlobalSettingScript()
        {
            ScriptTemplatesCreatorTools.CreateScript<GlobalSettingScriptCreationViewer>(
                (info, selectedAssetFolderPath) =>
                {
                    var globalSettingFileName = info.className + "File";

                    string folderPath = info.folderType.ToFolderString();

                    ScriptCreator.CreateScriptAssets(ScriptTemplatesNames.GLOBAL_SETTING, info.className,
                        selectedAssetFolderPath, extraInfo: new GlobalSettingScriptCreationExtraInfo()
                        {
                            namespaceName = info.namespaceName,
                            globalSettingFileName = globalSettingFileName
                        }, postProcessor: settingPostProcessor);

                    ScriptCreator.CreateScriptAssets(ScriptTemplatesNames.GLOBAL_SETTING_FILE, globalSettingFileName,
                        selectedAssetFolderPath, extraInfo: new GlobalSettingFileScriptCreationExtraInfo()
                        {
                            namespaceName = info.namespaceName,
                            folderPath = folderPath,
                            nameInGameEditor = '\"' + info.className.ToPascalCase(" ") + '\"'
                        }, postProcessor: settingFilePostProcessor);
                });
        }
    }
}
#endif