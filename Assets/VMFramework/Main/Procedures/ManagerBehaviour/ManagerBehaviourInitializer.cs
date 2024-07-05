using System.Collections.Generic;
using System.Linq;
using UnityEngine.Scripting;

namespace VMFramework.Procedure
{
    [GameInitializerRegister(GameInitializationDoneProcedure.ID, ProcedureLoadingType.OnEnter)]
    [Preserve]
    public sealed class ManagerBehaviourInitializer : IGameInitializer
    {
        IEnumerable<InitializationAction> IInitializer.GetInitializationActions()
        {
            var managerBehaviours = ManagerBehaviourCollector.Collect().ToList();

            return managerBehaviours.SelectMany(managerBehaviour =>
                managerBehaviour.GetInitializationActions());
        }
    }
}