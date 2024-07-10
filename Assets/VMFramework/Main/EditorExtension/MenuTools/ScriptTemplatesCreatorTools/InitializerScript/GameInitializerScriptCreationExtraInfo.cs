#if UNITY_EDITOR
using VMFramework.Procedure;

namespace VMFramework.Editor
{
    public sealed class GameInitializerScriptCreationExtraInfo : InitializerScriptCreationExtraInfo
    {
        public ProcedureLoadingType loadingType { get; init; }
    }
}
#endif