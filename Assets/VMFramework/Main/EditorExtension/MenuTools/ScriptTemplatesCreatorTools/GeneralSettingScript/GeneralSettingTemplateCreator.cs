#if UNITY_EDITOR
using UnityEditor;
using VMFramework.Core;
using VMFramework.Core.Editor;
using VMFramework.GameLogicArchitecture;

namespace VMFramework.Editor
{
    public static class GeneralSettingTemplateCreator
    {
        private static readonly GeneralSettingScriptPostProcessor postProcessor = new();

        [MenuItem(UnityMenuItemNames.SCRIPT_TEMPLATE + "General Setting")]
        private static void CreateGeneralSettingScript()
        {
            ScriptTemplatesCreatorTools.CreateScript<GeneralSettingScriptCreationViewer>(
                (info, selectedAssetFolderPath) =>
                {
                    ScriptCreator.CreateScriptAssets(ScriptTemplatesNames.GENERAL_SETTING, info.className,
                        selectedAssetFolderPath, extraInfo: new GeneralSettingScriptExtraInfo()
                        {
                            namespaceName = info.namespaceName,
                            nameInGameEditor = info.name.ToPascalCase(" ")
                        }, postProcessor: postProcessor);
                });
        }
    }
}
#endif