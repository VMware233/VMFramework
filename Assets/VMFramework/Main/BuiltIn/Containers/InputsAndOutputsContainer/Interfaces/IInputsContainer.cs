using VMFramework.Core;

namespace VMFramework.Containers
{
    public interface IInputsContainer : IContainer
    {
        public IKCubeInteger<int> inputsRange { get; }
    }
}