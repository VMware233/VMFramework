#if UNITY_EDITOR
using VMFramework.Core.Editor;

namespace VMFramework.Editor
{
    public sealed class GlobalSettingScriptCreationExtraInfo : ScriptCreationExtraInfo
    {
        public string globalSettingFileName { get; init; }
    }
}
#endif