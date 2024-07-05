using VMFramework.Procedure;

namespace VMFramework.Examples
{
    [ManagerCreationProvider("Demo")]
    public sealed class BasicManagerBehaviour : ManagerBehaviour<BasicManagerBehaviour>
    {
        protected override void OnBeforeInitStart()
        {
            base.OnBeforeInitStart();
            
            // Add any code here that needs to be executed to initialize the manager.
        }
    }
}