#if UNITY_EDITOR
using VMFramework.Core.Editor;

namespace VMFramework.Editor
{
    public sealed class GameItemScriptExtraInfo : ScriptCreationExtraInfo
    {
        public string parentClassName { get; init; }
        
        public string parentInterfaceName { get; init; }
    }
}
#endif