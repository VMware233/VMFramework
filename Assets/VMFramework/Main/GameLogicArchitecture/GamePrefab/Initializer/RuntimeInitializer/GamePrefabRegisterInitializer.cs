using System;
using System.Collections.Generic;
using UnityEngine.Scripting;
using VMFramework.Procedure;

namespace VMFramework.GameLogicArchitecture
{
    [GameInitializerRegister(VMFrameworkInitializationDoneProcedure.ID, ProcedureLoadingType.OnEnter)]
    [Preserve]
    public sealed class GamePrefabRegisterInitializer : IGameInitializer
    {
        IEnumerable<InitializationAction> IInitializer.GetInitializationActions()
        {
            yield return new(InitializationOrder.PostInit, OnPostInit, this);
            yield return new(InitializationOrder.InitComplete, OnInitComplete, this);
        }

        private static async void OnPostInit(Action onDone)
        {
            GamePrefabManager.Clear();

            var gamePrefabs = await GamePrefabCollectorManager.Collect();

            foreach (var gamePrefab in gamePrefabs)
            {
                GamePrefabManager.RegisterGamePrefab(gamePrefab);
            }

            onDone();
        }
        
        private static void OnInitComplete(Action onDone)
        {
            foreach (var gamePrefab in GamePrefabManager.GetAllGamePrefabs())
            {
                gamePrefab.CheckSettings();
            }
            
            onDone();
        }
    }
}