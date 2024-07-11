#if UNITY_EDITOR
using UnityEditor;
using VMFramework.Core.Editor;

namespace VMFramework.Editor
{
    public static class InitializerTemplateCreator
    {
        private static readonly GameInitializerScriptPostProcessor gameInitializerPostProcessor = new();
        
        private static readonly EditorInitializerScriptPostProcessor editorInitializerPostProcessor = new();

        [MenuItem(UnityMenuItemNames.SCRIPT_TEMPLATE + "Game Initializer")]
        private static void CreateGameInitializerScript()
        {
            ScriptTemplatesCreatorTools.CreateScript<GameInitializerScriptCreationViewer>((info, selectedAssetFolderPath) =>
            {
                ScriptCreator.CreateScriptAssets(ScriptTemplatesNames.GAME_INITIALIZER, info.className,
                    selectedAssetFolderPath, extraInfo: new GameInitializerScriptExtraInfo()
                    {
                        namespaceName = info.namespaceName,
                        initializationOrderName = info.initializationOrder.ToString(),
                        loadingType = info.loadingType
                    }, postProcessor: gameInitializerPostProcessor);
            });
        }
        
        [MenuItem(UnityMenuItemNames.SCRIPT_TEMPLATE + "Editor Initializer")]
        private static void CreateEditorInitializerScript()
        {
            ScriptTemplatesCreatorTools.CreateScript<EditorInitializerScriptCreationViewer>((info, selectedAssetFolderPath) =>
            {
                ScriptCreator.CreateScriptAssets(ScriptTemplatesNames.EDITOR_INITIALIZER, info.className,
                    selectedAssetFolderPath, extraInfo: new InitializerScriptExtraInfo()
                    {
                        namespaceName = info.namespaceName,
                        initializationOrderName = info.initializationOrder.ToString()
                    }, postProcessor: editorInitializerPostProcessor);
            });
        }
    }
}
#endif