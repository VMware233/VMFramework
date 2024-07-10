using VMFramework.GameLogicArchitecture;

namespace VMFramework.Editor
{
    public interface IScriptCreationViewer : INameOwner
    {
        public string className { get; }
        
        public string namespaceName { get; }
    }
}