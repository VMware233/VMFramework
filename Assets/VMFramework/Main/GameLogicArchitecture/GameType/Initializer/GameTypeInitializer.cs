using System;
using System.Collections.Generic;
using UnityEngine.Scripting;
using VMFramework.Procedure;

namespace VMFramework.GameLogicArchitecture
{
    [GameInitializerRegister(VMFrameworkInitializationDoneProcedure.ID, ProcedureLoadingType.OnEnter)]
    [Preserve]
    public sealed class GameTypeInitializer : IGameInitializer
    {
        IEnumerable<InitializationAction> IInitializer.GetInitializationActions()
        {
            yield return new(InitializationOrder.PreInit, OnPreInit, this);
        }

        private static void OnPreInit(Action onDone)
        {
            CoreSetting.gameTypeGeneralSetting.CheckGameTypeInfo();
            CoreSetting.gameTypeGeneralSetting.InitGameTypeInfo();
            
            onDone();
        }
    }
}