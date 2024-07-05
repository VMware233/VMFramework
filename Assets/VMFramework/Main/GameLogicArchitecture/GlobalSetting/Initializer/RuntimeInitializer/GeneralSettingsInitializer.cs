using System.Collections.Generic;
using UnityEngine.Scripting;
using VMFramework.Procedure;

namespace VMFramework.GameLogicArchitecture
{
    [GameInitializerRegister(GameInitializationDoneProcedure.ID, ProcedureLoadingType.OnEnter)]
    [Preserve]
    public sealed class GeneralSettingsInitializer : IGameInitializer
    {
        IEnumerable<InitializationAction> IInitializer.GetInitializationActions()
        {
            foreach (var globalSetting in GlobalSettingCollector.Collect())
            {
                foreach (var action in globalSetting.globalSettingFile.GetInitializationActions())
                {
                    yield return action;
                }
            }
        }
    }
}