using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.Scripting;
using VMFramework.Procedure;

namespace VMFramework.Localization
{
    [GameInitializerRegister(VMFrameworkInitializationDoneProcedure.ID, ProcedureLoadingType.OnEnter)]
    [Preserve]
    public sealed class LocalizationInitializer : IGameInitializer
    {
        IEnumerable<InitializationAction> IInitializer.GetInitializationActions()
        {
            yield return new(InitializationOrder.Init, OnInit, this);
        }

        private static async void OnInit(Action onDone)
        {
            await UniTask.WaitUntil(() => LocalizationSettings.InitializationOperation.IsDone);
            
            Debug.Log("Localization initialization complete.");
            
            onDone();
        }
    }
}
