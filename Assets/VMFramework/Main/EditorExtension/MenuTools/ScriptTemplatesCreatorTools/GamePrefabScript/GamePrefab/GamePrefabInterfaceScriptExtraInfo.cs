#if UNITY_EDITOR
using VMFramework.Core.Editor;

namespace VMFramework.Editor
{
    public sealed class GamePrefabInterfaceScriptExtraInfo : ScriptCreationExtraInfo
    {
        public string parentInterfaceName { get; init; }
    }
}
#endif