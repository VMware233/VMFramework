using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine.Scripting;
using VMFramework.Configuration;
using VMFramework.Procedure;

namespace VMFramework.GameLogicArchitecture
{
    [GameInitializerRegister(VMFrameworkInitializationDoneProcedure.ID, ProcedureLoadingType.OnEnter)]
    [Preserve]
    public sealed class GlobalSettingsFileLoadingInitializer : IGameInitializer
    {
        private static readonly List<IGlobalSetting> globalSettings = new();

        IEnumerable<InitializationAction> IInitializer.GetInitializationActions()
        {
            yield return new(InitializationOrder.InitStart, OnInitStart, this);
            yield return new(InitializationOrder.Init, OnInit, this);
        }

        private static async void OnInitStart(Action onDone)
        {
            var tasks = new List<UniTask>();
            
            globalSettings.Clear();
            
            foreach (var globalSetting in GlobalSettingCollector.Collect())
            {
                tasks.Add(globalSetting.LoadGlobalSettingFile());
                globalSettings.Add(globalSetting);
            }
            
            await UniTask.WhenAll(tasks);
            
            onDone();
        }

        private static void OnInit(Action onDone)
        {
            globalSettings.CheckSettings();
            
            onDone();
        }
    }
}
