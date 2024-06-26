﻿using System;
using VMFramework.GameLogicArchitecture;
using VMFramework.Procedure;

namespace VMFramework.GameEvents
{
    [GameInitializerRegister(VMFrameworkInitializationDoneProcedure.ID, ProcedureLoadingType.OnEnter)]
    public sealed class GameEventInitializer : IGameInitializer
    {
        void IInitializer.OnPostInit(Action onDone)
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