using VMFramework.GameEvents;

namespace VMFramework.Containers
{
    public class ContainerEvent<TContainerEvent> : SingletonGameEvent<TContainerEvent>
        where TContainerEvent : ContainerEvent<TContainerEvent>, new()
    {
        public IContainer container { get; private set; }
        
        public static void SetContainer(IContainer newContainer)
        {
            instance.container = newContainer;
        }

        protected override void OnPropagationStopped()
        {
            base.OnPropagationStopped();
            
            container = null;
        }
    }
}