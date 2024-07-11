#if UNITY_EDITOR
using UnityEditor;
using VMFramework.Core;
using VMFramework.Core.Editor;
using VMFramework.GameLogicArchitecture;

namespace VMFramework.Editor
{
    public static class GamePrefabTemplateCreator
    {
        private static readonly GamePrefabScriptPostProcessor gamePrefabPostProcessor = new();

        private static readonly GamePrefabInterfaceScriptPostProcessor gamePrefabInterfacePostProcessor = new();

        private static readonly GameItemScriptPostProcessor gameItemPostProcessor = new();

        private static readonly GameItemInterfaceScriptPostProcessor gameItemInterfacePostProcessor = new();

        private static readonly GamePrefabGeneralSettingScriptPostProcessor gamePrefabGeneralSettingPostProcessor =
            new();

        [MenuItem(UnityMenuItemNames.SCRIPT_TEMPLATE + "Game Prefab")]
        private static void CreateGamePrefabScript()
        {
            ScriptTemplatesCreatorTools.CreateScript<GamePrefabScriptCreationViewer>((info, selectedAssetFolderPath) =>
            {
                var interfaceName = "I" + info.className;

                string gameItemName = info.name;

                string gameItemType = info.withGameItem ? $"typeof({gameItemName})" : "null";
                
                var generalSettingName = gameItemName + "GeneralSetting";
                string gameItemInterfaceName = "I" + gameItemName;

                string gameItemFolder = selectedAssetFolderPath;
                string gamePrefabFolder = selectedAssetFolderPath;
                string gamePrefabGeneralSettingFolder = selectedAssetFolderPath;

                if (info.createSubFolders)
                {
                    gameItemFolder = gameItemFolder.PathCombine(gameItemName);
                    gamePrefabFolder = gamePrefabFolder.PathCombine(info.className);
                    gamePrefabGeneralSettingFolder = gamePrefabGeneralSettingFolder.PathCombine(generalSettingName);
                }

                ScriptCreator.CreateScriptAssets(ScriptTemplatesNames.GAME_PREFAB, info.className, gamePrefabFolder,
                    extraInfo: new GamePrefabScriptExtraInfo()
                    {
                        namespaceName = info.namespaceName,
                        idSuffix = info.name.ToSnakeCase(),
                        parentInterfaceName = interfaceName,
                        parentClassName = info.gamePrefabBaseType.GetName(),
                        gameItemType = gameItemType,
                    }, postProcessor: gamePrefabPostProcessor);

                ScriptCreator.CreateScriptAssets(ScriptTemplatesNames.GAME_PREFAB_INTERFACE, interfaceName,
                    gamePrefabFolder, extraInfo: new GamePrefabInterfaceScriptExtraInfo()
                    {
                        namespaceName = info.namespaceName,
                        parentInterfaceName = info.gamePrefabBaseType.GetInterfaceName()
                    }, postProcessor: gamePrefabInterfacePostProcessor);

                if (info.withGameItem)
                {
                    ScriptCreator.CreateScriptAssets(ScriptTemplatesNames.GAME_ITEM, gameItemName,
                        gameItemFolder, extraInfo: new GameItemScriptExtraInfo()
                        {
                            namespaceName = info.namespaceName,
                            parentClassName = info.gameItemBaseType.GetName(),
                            parentInterfaceName = gameItemInterfaceName
                        }, postProcessor: gameItemPostProcessor);

                    ScriptCreator.CreateScriptAssets(ScriptTemplatesNames.GAME_ITEM_INTERFACE, gameItemInterfaceName,
                        gameItemFolder, extraInfo: new GameItemInterfaceScriptExtraInfo()
                        {
                            namespaceName = info.namespaceName,
                            parentInterfaceName = info.gameItemBaseType.GetInterfaceName()
                        }, postProcessor: gameItemInterfacePostProcessor);
                }

                if (info.withGamePrefabGeneralSetting)
                {
                    ScriptCreator.CreateScriptAssets(ScriptTemplatesNames.GAME_PREFAB_GENERAL_SETTING,
                        generalSettingName, gamePrefabGeneralSettingFolder,
                        extraInfo: new GamePrefabGeneralSettingScriptExtraInfo()
                        {
                            namespaceName = info.namespaceName,
                            baseGamePrefabType = interfaceName,
                            nameInGameEditor = gameItemName.ToPascalCase(" "),
                        }, postProcessor: gamePrefabGeneralSettingPostProcessor);
                }
            });
        }
    }
}
#endif