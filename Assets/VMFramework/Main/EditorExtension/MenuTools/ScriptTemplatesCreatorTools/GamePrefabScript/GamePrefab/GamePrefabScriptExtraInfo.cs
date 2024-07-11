#if UNITY_EDITOR
using VMFramework.Core.Editor;

namespace VMFramework.Editor
{
    public sealed class GamePrefabScriptExtraInfo : ScriptCreationExtraInfo
    {
        public string parentClassName { get; init; }
        
        public string parentInterfaceName { get; init; }
        
        public string idSuffix { get; init; }
        
        public string gameItemType { get; init; }
    }
}
#endif