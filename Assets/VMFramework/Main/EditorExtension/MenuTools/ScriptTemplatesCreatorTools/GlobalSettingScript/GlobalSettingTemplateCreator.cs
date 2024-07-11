#if UNITY_EDITOR
using UnityEditor;
using VMFramework.Core;
using VMFramework.Core.Editor;
using VMFramework.GameLogicArchitecture;

namespace VMFramework.Editor
{
    public static class GlobalSettingTemplateCreator
    {
        private static readonly GlobalSettingScriptPostProcessor settingPostProcessor = new();
        private static readonly GlobalSettingFileScriptPostProcessor settingFilePostProcessor = new();

        [MenuItem(UnityMenuItemNames.SCRIPT_TEMPLATE + "Global Setting")]
        private static void CreateGlobalSettingScript()
        {
            ScriptTemplatesCreatorTools.CreateScript<GlobalSettingScriptCreationViewer>(
                (info, selectedAssetFolderPath) =>
                {
                    var globalSettingFileName = info.className + "File";

                    string folderPath = info.folderType.ToFolderString();

                    ScriptCreator.CreateScriptAssets(ScriptTemplatesNames.GLOBAL_SETTING, info.className,
                        selectedAssetFolderPath, extraInfo: new GlobalSettingScriptExtraInfo()
                        {
                            namespaceName = info.namespaceName,
                            globalSettingFileName = globalSettingFileName
                        }, postProcessor: settingPostProcessor);

                    ScriptCreator.CreateScriptAssets(ScriptTemplatesNames.GLOBAL_SETTING_FILE, globalSettingFileName,
                        selectedAssetFolderPath, extraInfo: new GlobalSettingFileScriptExtraInfo()
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