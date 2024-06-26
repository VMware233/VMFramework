using System;
using Cysharp.Threading.Tasks;
using UnityEngine.Localization.Settings;
using UnityEngine.Scripting;
using VMFramework.Procedure;

namespace VMFramework.Localization
{
    [GameInitializerRegister(VMFrameworkInitializationDoneProcedure.ID, ProcedureLoadingType.OnEnter)]
    [Preserve]
    public class LocalizationInitializer : IGameInitializer
    {
        public async void OnInit(Action onDone)
        {
            await UniTask.WaitUntil(() => LocalizationSettings.InitializationOperation.IsDone);
            onDone();
        }
    }
}
