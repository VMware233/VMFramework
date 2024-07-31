#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VMFramework.Procedure;
using VMFramework.Procedure.Editor;

namespace VMFramework.GameLogicArchitecture.Editor
{
    internal sealed class GamePrefabWrapperEditorInitializer : IEditorInitializer
    {
        private static readonly List<IGamePrefab> gamePrefabs = new();
        private static readonly HashSet<GamePrefabGeneralSetting> gamePrefabGeneralSettings = new();

        IEnumerable<InitializationAction> IInitializer.GetInitializationActions()
        {
            yield return new(InitializationOrder.Init, OnInit, this);
        }

        private static void OnInit(Action onDone)
        {
            GamePrefabWrapperCreator.OnGamePrefabWrapperCreated += gamePrefabWrapper =>
            {
                gamePrefabs.Clear();
                gamePrefabs.AddRange(gamePrefabWrapper.GetGamePrefabs());

                if (gamePrefabs.Count == 0 || gamePrefabs.All(gamePrefab => gamePrefab == null))
                {
                    Debug.LogWarning($"No Valid Game Prefabs found in {gamePrefabWrapper.name}");
                    return;
                }

                gamePrefabGeneralSettings.Clear();
                foreach (var gamePrefab in gamePrefabs)
                {
                    if (gamePrefab.TryGetGamePrefabGeneralSetting(out var gamePrefabGeneralSetting))
                    {
                        gamePrefabGeneralSettings.Add(gamePrefabGeneralSetting);
                    }
                }

                if (gamePrefabGeneralSettings.Count == 0)
                {
                    Debug.LogWarning($"No {nameof(GamePrefabGeneralSetting)} Found for " +
                                     $"any of the Game Prefabs in {gamePrefabWrapper.name}");
                    return;
                }

                if (gamePrefabGeneralSettings.Count > 1)
                {
                    Debug.LogWarning($"Multiple {nameof(GamePrefabGeneralSetting)} Found for " +
                                     $"the Game Prefabs in {gamePrefabWrapper.name}. " +
                                     $"This is not recommended.");
                }

                foreach (var gamePrefabGeneralSetting in gamePrefabGeneralSettings)
                {
                    gamePrefabGeneralSetting.AddToInitialGamePrefabWrappers(gamePrefabWrapper);
                }
            };
            
            GamePrefabWrapperInitializeUtility.Refresh();
            GamePrefabWrapperInitializeUtility.CreateAutoRegisterGamePrefabs();
            
            onDone();
        }
    }
}
#endif