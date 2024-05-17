using VMFramework.GameEvents;

namespace VMFramework.Containers
{
    public class ContainerEvent<TContainerEvent> : GameEvent<TContainerEvent>
        where TContainerEvent : ContainerEvent<TContainerEvent>
    {
        public IContainer container { get; private set; }
        
        public void SetContainer(IContainer container)
        {
            this.container = container;
        }

        protected override void OnPropagationStopped()
        {
            base.OnPropagationStopped();
            
            container = null;
        }
    }
}