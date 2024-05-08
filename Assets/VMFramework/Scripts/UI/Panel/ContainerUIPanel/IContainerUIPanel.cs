using VMFramework.Containers;

namespace VMFramework.UI
{
    public interface IContainerUIPanel : IUIPanelController
    {
        public int containerUIPriority { get; }

        public IContainer GetBindContainer();

        public void SetBindContainer(IContainer newBindContainer);
    }

}