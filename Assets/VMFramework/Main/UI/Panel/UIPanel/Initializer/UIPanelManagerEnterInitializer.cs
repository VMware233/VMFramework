using System;
using UnityEngine;
using UnityEngine.Scripting;
using VMFramework.GameLogicArchitecture;
using VMFramework.Procedure;

namespace VMFramework.UI
{
    [GameInitializerRegister(GameInitializationDoneProcedure.ID, ProcedureLoadingType.OnEnter)]
    [Preserve]
    internal sealed class UIPanelManagerEnterInitializer : IGameInitializer
    {
        void IInitializer.OnInitComplete(Action onDone)
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