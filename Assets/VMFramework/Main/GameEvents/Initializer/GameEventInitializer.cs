using System;
using System.Collections.Generic;
using UnityEngine.Scripting;
using VMFramework.GameLogicArchitecture;
using VMFramework.Procedure;

namespace VMFramework.GameEvents
{
    [GameInitializerRegister(GameInitializationDoneProcedure.ID, ProcedureLoadingType.OnEnter)]
    [Preserve]
    public sealed class GameEventInitializer : IGameInitializer
    {
        IEnumerable<InitializationAction> IInitializer.GetInitializationActions()
        {
            yield return new(InitializationOrder.Init, OnInit, this);
        }

        private static void OnInit(Action onDone)
        {
            foreach (var gameEventConfig in GamePrefabManager.GetAllActiveGamePrefabs<IGameEventConfig>())
            {
                var gameEvent = IGameItem.Create<IGameEvent>(gameEventConfig.id);
                GameEventManager.Register(gameEvent);
            }
            
            onDone();
        }
    }
}