#if UNITY_EDITOR
using VMFramework.Core.Editor;

namespace VMFramework.Editor
{
    public sealed class GamePrefabGeneralSettingScriptExtraInfo : ScriptCreationExtraInfo
    {
        public string baseGamePrefabType { get; init; }
        
        public string nameInGameEditor { get; init; }
    }
}
#endif