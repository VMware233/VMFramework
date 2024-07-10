#if UNITY_EDITOR
using UnityEngine;
using VMFramework.Core.Editor;

namespace VMFramework.Editor
{
    public sealed class GlobalSettingFileScriptCreationPostProcessor
        : ScriptCreationPostProcessor<GlobalSettingFileScriptCreationExtraInfo>
    {
        protected override void PostProcess(string scriptAbsolutePath, ref string scriptContent,
            GlobalSettingFileScriptCreationExtraInfo extraInfo)
        {
            Replace(ref scriptContent, "FOLDER_PATH", extraInfo.folderPath);
            Replace(ref scriptContent, "NAME_IN_GAME_EDITOR", extraInfo.nameInGameEditor);
        }
    }
}
#endif