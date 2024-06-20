using System;
using VMFramework.Procedure;

namespace VMFramework.Examples
{
    [GameInitializerRegister(GameInitializationDoneProcedure.ID, ProcedureLoadingType.OnEnter)]
    public sealed class PlayerInitializer : IGameInitializer
    {
        void IInitializer.OnPostInit(Action onDone)
        {
            // Do something with the player here.
            
            // Call the onDone callback to signal that the initialization is done.
            onDone();
        }
    }
}