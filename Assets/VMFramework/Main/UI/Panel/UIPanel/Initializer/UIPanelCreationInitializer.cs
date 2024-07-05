using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;
using VMFramework.GameLogicArchitecture;
using VMFramework.Procedure;

namespace VMFramework.UI
{
    [GameInitializerRegister(GameInitializationDoneProcedure.ID, ProcedureLoadingType.OnEnter)]
    [Preserve]
    internal sealed class UIPanelCreationInitializer : IGameInitializer
    {
        IEnumerable<InitializationAction> IInitializer.GetInitializationActions()
        {
            yield return new(InitializationOrder.InitComplete, OnInitComplete, this);
        }

        private static void OnInitComplete(Action onDone)
        {
            Debug.Log("Creating initial UI Panels!");

            foreach (var uiPanelPreset in GamePrefabManager.GetAllActiveGamePrefabs<IUIPanelPreset>())
            {
                if (uiPanelPreset.isUnique)
                {
                    UIPanelManager.CreatePanel(uiPanelPreset.id);
                }
                else if (uiPanelPreset.prewarmCount > 0)
                {
                    for (int i = 0; i < uiPanelPreset.prewarmCount; i++)
                    {
                        UIPanelManager.CreatePanel(uiPanelPreset.id);
                    }
                }
            }
            
            onDone();
        }
    }
}