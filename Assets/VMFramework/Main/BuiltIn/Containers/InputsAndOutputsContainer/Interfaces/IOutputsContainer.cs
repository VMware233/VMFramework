using VMFramework.Core;

namespace VMFramework.Containers
{
    public interface IOutputsContainer : IContainer
    {
        public IKCubeInteger<int> outputsRange { get; }
    }
}