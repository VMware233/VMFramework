using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;
using VMFramework.GameLogicArchitecture;
using VMFramework.Procedure;

namespace VMFramework.Properties
{
    [GameInitializerRegister(GameInitializationDoneProcedure.ID, ProcedureLoadingType.OnEnter)]
    [Preserve]
    public sealed class TooltipPropertyInitializer : IGameInitializer
    {
        IEnumerable<InitializationAction> IInitializer.GetInitializationActions()
        {
            yield return new(InitializationOrder.Init, OnInit, this);
        }

        private static void OnInit(Action onDone)
        {
            Debug.Log("Initializing Tooltip Property");
            
            foreach (var gamePrefab in GamePrefabManager.GetAllGamePrefabs())
            {
                if (gamePrefab.gameItemType == null)
                {
                    continue;
                }

                if (gamePrefab.gameItemType.IsAbstract)
                {
                    continue;
                }

                foreach (var configRuntime in TooltipPropertyManager.GetTooltipPropertyConfigsRuntime(
                             gamePrefab.gameItemType))
                {
                    TooltipPropertyManager.AddTooltipPropertyConfigRuntime(gamePrefab.id, configRuntime);
                }
            }

            onDone();
        }
    }
}