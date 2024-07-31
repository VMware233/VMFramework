using System.Collections.Generic;
using UnityEngine.Scripting;
using VMFramework.Procedure;

namespace VMFramework.GameLogicArchitecture
{
    [GameInitializerRegister(GameInitializationDoneProcedure.ID, ProcedureLoadingType.OnEnter)]
    [Preserve]
    public sealed class GamePrefabInitializer : IGameInitializer
    {
        IEnumerable<InitializationAction> IInitializer.GetInitializationActions()
        {
            foreach (var gamePrefab in GamePrefabManager.GetAllGamePrefabs())
            {
                foreach (var action in gamePrefab.GetInitializationActions())
                {
                    yield return action;
                }
            }
        }
    }
}