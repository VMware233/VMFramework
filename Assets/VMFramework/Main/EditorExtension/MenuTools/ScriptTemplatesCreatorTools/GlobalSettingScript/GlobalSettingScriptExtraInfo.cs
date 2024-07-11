#if UNITY_EDITOR
using VMFramework.Core.Editor;

namespace VMFramework.Editor
{
    public sealed class GlobalSettingScriptExtraInfo : ScriptCreationExtraInfo
    {
        public string globalSettingFileName { get; init; }
    }
}
#endif