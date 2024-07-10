#if UNITY_EDITOR
using VMFramework.Core.Editor;

namespace VMFramework.Editor
{
    public sealed class GeneralSettingScriptCreationExtraInfo : ScriptCreationExtraInfo
    {
        public string nameInGameEditor { get; init; }
    }
}
#endif