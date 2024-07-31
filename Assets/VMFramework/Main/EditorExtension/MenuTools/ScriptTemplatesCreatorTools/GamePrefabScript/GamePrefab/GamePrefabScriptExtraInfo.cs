#if UNITY_EDITOR
using VMFramework.Core.Editor;

namespace VMFramework.Editor
{
    public sealed class GamePrefabScriptExtraInfo : ScriptCreationExtraInfo
    {
        public string parentClassName { get; init; }
        
        public bool enableParentInterfaceRegion { get; init; }
        
        public string parentInterfaceName { get; init; }
        
        public bool enableIDSuffixOverrideRegion { get; init; }
        
        public string idSuffix { get; init; }
        
        public bool enableGameItemTypeOverrideRegion { get; init; }
        
        public string gameItemType { get; init; }
    }
}
#endif