using UnityEditor;
using VMFramework.Core;
using VMFramework.Core.Editor;

namespace VMFramework.Editor
{
    public static class GeneralSettingTemplateCreator
    {
        private static readonly GeneralSettingScriptCreationPostProcessor postProcessor = new();

        [MenuItem(UnityMenuItemNames.ASSETS_CREATION_VMFRAMEWORK + nameof(CreateGeneralSettingScript))]
        private static void CreateGeneralSettingScript()
        {
            ScriptTemplatesCreatorTools.CreateScript<GeneralSettingScriptCreationViewer>(
                (info, selectedAssetFolderPath) =>
                {
                    ScriptCreator.CreateScriptAssets(ScriptTemplatesNames.GENERAL_SETTING, info.className,
                        selectedAssetFolderPath, extraInfo: new GeneralSettingScriptCreationExtraInfo()
                        {
                            namespaceName = info.namespaceName,
                            nameInGameEditor = info.name.ToPascalCase(" ")
                        }, postProcessor: postProcessor);
                });
        }
    }
}