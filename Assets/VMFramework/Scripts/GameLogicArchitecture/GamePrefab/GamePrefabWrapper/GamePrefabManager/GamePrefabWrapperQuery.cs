using System;
using System.Collections.Generic;
using VMFramework.Core;
using VMFramework.Core.Editor;

namespace VMFramework.GameLogicArchitecture
{
    public static class GamePrefabWrapperQuery
    {
        public static IEnumerable<GamePrefabWrapper> GetAllGamePrefabWrappers()
        {
            return ConfigurationPath.RESOURCES_PATH.FindAssetsOfType<GamePrefabWrapper>();
        }

        public static IEnumerable<GamePrefabWrapper> GetGamePrefabWrapper(IGamePrefab gamePrefab)
        {
            if (gamePrefab == null)
            {
                yield break;
            }

            foreach (var gamePrefabWrapper in GetAllGamePrefabWrappers())
            {
                foreach (var existingGamePrefab in gamePrefabWrapper.GetGamePrefabs())
                {
                    if (existingGamePrefab == gamePrefab)
                    {
                        yield return gamePrefabWrapper;
                        break;
                    }
                }
            }
        }

        public static IEnumerable<GamePrefabWrapper> GetGamePrefabWrapper(Type gamePrefabType)
        {
            if (gamePrefabType == null)
            {
                yield break;
            }

            if (gamePrefabType.IsDerivedFrom<IGamePrefab>(true) == false)
            {
                yield break;
            }

            foreach (var gamePrefabWrapper in GetAllGamePrefabWrappers())
            {
                foreach (var existingGamePrefab in gamePrefabWrapper.GetGamePrefabs())
                {
                    if (existingGamePrefab.GetType().IsDerivedFrom(gamePrefabType, true))
                    {
                        yield return gamePrefabWrapper;
                        break;
                    }
                }
            }
        }
    }
}