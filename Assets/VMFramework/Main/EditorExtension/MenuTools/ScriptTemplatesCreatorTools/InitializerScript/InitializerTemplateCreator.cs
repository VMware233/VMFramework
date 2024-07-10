#if UNITY_EDITOR
using UnityEditor;
using VMFramework.Core.Editor;

namespace VMFramework.Editor
{
    public static class InitializerTemplateCreator
    {
        private static readonly GameInitializerScriptCreationPostProcessor gameInitializerPostProcessor = new();
        
        private static readonly EditorInitializerScriptCreationPostProcessor editorInitializerPostProcessor = new();

        [MenuItem(UnityMenuItemNames.ASSETS_CREATION_VMFRAMEWORK + nameof(CreateGameInitializerScript))]
        private static void CreateGameInitializerScript()
        {
            ScriptTemplatesCreatorTools.CreateScript<GameInitializerScriptCreationViewer>((info, selectedAssetFolderPath) =>
            {
                ScriptCreator.CreateScriptAssets(ScriptTemplatesNames.GAME_INITIALIZER, info.className,
                    selectedAssetFolderPath, extraInfo: new GameInitializerScriptCreationExtraInfo()
                    {
                        namespaceName = info.namespaceName,
                        initializationOrderName = info.initializationOrder.ToString(),
                        loadingType = info.loadingType
                    }, postProcessor: gameInitializerPostProcessor);
            });
        }
        
        [MenuItem(UnityMenuItemNames.ASSETS_CREATION_VMFRAMEWORK + nameof(CreateEditorInitializerScript))]
        private static void CreateEditorInitializerScript()
        {
            ScriptTemplatesCreatorTools.CreateScript<EditorInitializerScriptCreationViewer>((info, selectedAssetFolderPath) =>
            {
                ScriptCreator.CreateScriptAssets(ScriptTemplatesNames.EDITOR_INITIALIZER, info.className,
                    selectedAssetFolderPath, extraInfo: new InitializerScriptCreationExtraInfo()
                    {
                        namespaceName = info.namespaceName,
                        initializationOrderName = info.initializationOrder.ToString()
                    }, postProcessor: editorInitializerPostProcessor);
            });
        }
    }
}
#endif