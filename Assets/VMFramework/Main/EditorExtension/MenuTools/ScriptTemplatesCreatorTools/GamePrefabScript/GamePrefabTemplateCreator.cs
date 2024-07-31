#if UNITY_EDITOR
using System;
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

        [MenuItem(UnityMenuItemNames.SCRIPT_TEMPLATE + "Derived Game Prefab", false, 0)]
        private static void CreateDerivedGamePrefabScript()
        {
            ScriptTemplatesCreatorTools.CreateScript<DerivedGamePrefabScriptCreationViewer>(info =>
            {
                var parentGamePrefabName = info.parentGamePrefabType.Name;
                var parentGamePrefabInterfaceName = "I" + parentGamePrefabName;
                var parentGameItemType = info.parentGamePrefabType.GetGameItemType();
                var parentGameItemName = parentGameItemType.Name;
                var parentGameItemInterfaceName = "I" + parentGameItemName;

                var gamePrefabName = info.ClassName;
                var gamePrefabInterfaceName = "I" + gamePrefabName;
                var gameItemName = info.name;
                var gameItemInterfaceName = "I" + gameItemName;

                var gamePrefabFolder = info.assetFolderPath;
                var gameItemFolder = info.assetFolderPath;

                var createGameItem = info.withGameItem && info.parentGamePrefabType.HasGameItem();

                if (info.createSubFolders)
                {
                    gamePrefabFolder = gamePrefabFolder.PathCombine(gamePrefabName);
                    gameItemFolder = gameItemFolder.PathCombine(gameItemName);
                }

                ScriptCreator.CreateScriptAssets(ScriptTemplatesNames.GAME_PREFAB, gamePrefabName, gamePrefabFolder,
                    extraInfo: new GamePrefabScriptExtraInfo()
                    {
                        namespaceName = info.namespaceName,
                        enableParentInterfaceRegion = info.withGamePrefabInterface,
                        parentInterfaceName = gamePrefabInterfaceName,
                        parentClassName = parentGamePrefabName,
                        enableIDSuffixOverrideRegion = false,
                        enableGameItemTypeOverrideRegion = createGameItem,
                        gameItemType = createGameItem ? $"typeof({gameItemName})" : "null"
                    }, postProcessor: gamePrefabPostProcessor);

                if (info.withGamePrefabInterface)
                {
                    ScriptCreator.CreateScriptAssets(ScriptTemplatesNames.GAME_PREFAB_INTERFACE,
                        gamePrefabInterfaceName, gamePrefabFolder, extraInfo: new GamePrefabInterfaceScriptExtraInfo()
                        {
                            namespaceName = info.namespaceName,
                            parentInterfaceName = parentGamePrefabInterfaceName
                        }, postProcessor: gamePrefabInterfacePostProcessor);
                }

                if (createGameItem)
                {
                    ScriptCreator.CreateScriptAssets(ScriptTemplatesNames.GAME_ITEM, gameItemName, gameItemFolder,
                        extraInfo: new GameItemScriptExtraInfo()
                        {
                            namespaceName = info.namespaceName,
                            parentClassName = parentGameItemName,
                            enableParentInterfaceRegion = info.withGameItemInterface,
                            parentInterfaceName = gameItemInterfaceName,
                            gamePrefabInterfaceName = info.withGamePrefabInterface
                                ? gamePrefabInterfaceName
                                : gamePrefabName,
                            gamePrefabFieldName = gamePrefabName.ToPascalCase()
                        }, postProcessor: gameItemPostProcessor);

                    if (info.withGameItemInterface)
                    {
                        ScriptCreator.CreateScriptAssets(ScriptTemplatesNames.GAME_ITEM_INTERFACE,
                            gameItemInterfaceName, gameItemFolder, extraInfo: new GameItemInterfaceScriptExtraInfo()
                            {
                                namespaceName = info.namespaceName,
                                parentInterfaceName = parentGameItemInterfaceName
                            }, postProcessor: gameItemInterfacePostProcessor);
                    }
                }
            });
        }

        [MenuItem(UnityMenuItemNames.SCRIPT_TEMPLATE + "Game Prefab")]
        private static void CreateGamePrefabScript()
        {
            ScriptTemplatesCreatorTools.CreateScript<GamePrefabScriptCreationViewer>(info =>
            {
                if (info.withGameItem)
                {
                    if (info.gamePrefabBaseType is GamePrefabBaseType.GamePrefab
                        or GamePrefabBaseType.LocalizedGamePrefab)
                    {
                        throw new ArgumentException($"Cannot create a {nameof(GameItem)} with {nameof(GamePrefab)} :" +
                                                    $"{info.gamePrefabBaseType}");
                    }
                }

                var gamePrefabName = info.ClassName;
                var gamePrefabInterfaceName = "I" + gamePrefabName;

                var gameItemName = info.name;
                var gameItemInterfaceName = "I" + gameItemName;

                var generalSettingName = gameItemName + "GeneralSetting";

                var gameItemType = info.withGameItem ? $"typeof({gameItemName})" : "null";

                var gamePrefabFolder = info.assetFolderPath;
                var gameItemFolder = info.assetFolderPath;
                var gamePrefabGeneralSettingFolder = info.assetFolderPath;

                if (info.createSubFolders)
                {
                    gamePrefabFolder = gamePrefabFolder.PathCombine(gamePrefabName);
                    gameItemFolder = gameItemFolder.PathCombine(gameItemName);
                    gamePrefabGeneralSettingFolder = gamePrefabGeneralSettingFolder.PathCombine(generalSettingName);
                }

                ScriptCreator.CreateScriptAssets(ScriptTemplatesNames.GAME_PREFAB, gamePrefabName, gamePrefabFolder,
                    extraInfo: new GamePrefabScriptExtraInfo()
                    {
                        namespaceName = info.namespaceName,
                        parentInterfaceName = gamePrefabInterfaceName,
                        parentClassName = info.gamePrefabBaseType.GetName(),
                        enableIDSuffixOverrideRegion = true,
                        idSuffix = info.name.ToSnakeCase(),
                        enableGameItemTypeOverrideRegion = info.withGameItem,
                        gameItemType = gameItemType,
                    }, postProcessor: gamePrefabPostProcessor);

                ScriptCreator.CreateScriptAssets(ScriptTemplatesNames.GAME_PREFAB_INTERFACE, gamePrefabInterfaceName,
                    gamePrefabFolder, extraInfo: new GamePrefabInterfaceScriptExtraInfo()
                    {
                        namespaceName = info.namespaceName,
                        parentInterfaceName = info.gamePrefabBaseType.GetInterfaceName()
                    }, postProcessor: gamePrefabInterfacePostProcessor);

                if (info.withGameItem)
                {
                    ScriptCreator.CreateScriptAssets(ScriptTemplatesNames.GAME_ITEM, gameItemName, gameItemFolder,
                        extraInfo: new GameItemScriptExtraInfo()
                        {
                            namespaceName = info.namespaceName,
                            parentClassName = info.gameItemBaseType.GetName(),
                            enableParentInterfaceRegion = true,
                            parentInterfaceName = gameItemInterfaceName,
                            gamePrefabInterfaceName = gamePrefabInterfaceName,
                            gamePrefabFieldName = info.ClassName.ToPascalCase()
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
                            baseGamePrefabType = gamePrefabInterfaceName,
                            nameInGameEditor = gameItemName.ToPascalCase(" "),
                        }, postProcessor: gamePrefabGeneralSettingPostProcessor);
                }
            });
        }
    }
}
#endif