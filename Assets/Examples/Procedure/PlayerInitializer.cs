using System;
using System.Collections.Generic;
using UnityEngine.Scripting;
using VMFramework.Procedure;

namespace VMFramework.Examples
{
    [GameInitializerRegister(ClientRunningProcedure.ID, ProcedureLoadingType.OnEnter)]
    [Preserve]
    public sealed class PlayerInitializer : IGameInitializer
    {
        IEnumerable<InitializationAction> IInitializer.GetInitializationActions()
        {
            yield return new(InitializationOrder.PostInit, OnPostInit, this);
        }

        private void OnPostInit(Action onDone)
        {
            // Do something with the player here.
            
            // Call the onDone callback to signal that the initialization is done.
            onDone();
        }
    }
}