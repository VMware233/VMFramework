using System;
using System.Collections.Generic;
using UnityEngine.Scripting;

namespace VMFramework.Procedure
{
    /// <summary>
    /// Create the ManagerBehaviours in the very beginning of the game.
    /// </summary>
    [GameInitializerRegister(VMFrameworkInitializationDoneProcedure.ID, ProcedureLoadingType.OnEnter)]
    [Preserve]
    public sealed class ManagerBehaviourCreationInitializer : IGameInitializer
    {
        IEnumerable<InitializationAction> IInitializer.GetInitializationActions()
        {
            yield return new(InitializationOrder.BeforeInitStart, OnBeforeInitStart, this);
        }

        private static void OnBeforeInitStart(Action onDone)
        {
            ManagerCreator.CreateManagers();

            foreach (var manager in ManagerCreator.managers)
            {
                manager.SetInstance();
            }
            
            onDone();
        }
    }
}
