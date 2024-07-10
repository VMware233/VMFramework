#if UNITY_EDITOR
using VMFramework.Core.Editor;

namespace VMFramework.Editor
{
    public sealed class GlobalSettingFileScriptCreationExtraInfo : ScriptCreationExtraInfo
    {
        public string folderPath { get; init; }
        
        public string nameInGameEditor { get; init; }
    }
}
#endif