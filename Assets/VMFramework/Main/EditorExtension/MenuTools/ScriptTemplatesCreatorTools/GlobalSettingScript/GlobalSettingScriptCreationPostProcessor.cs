#if UNITY_EDITOR
using VMFramework.Core.Editor;

namespace VMFramework.Editor
{
    public sealed class GlobalSettingScriptCreationPostProcessor
        : ScriptCreationPostProcessor<GlobalSettingScriptCreationExtraInfo>
    {
        protected override void PostProcess(string scriptAbsolutePath, ref string scriptContent,
            GlobalSettingScriptCreationExtraInfo extraInfo)
        {
            Replace(ref scriptContent, "GLOBAL_SETTING_FILE_NAME", extraInfo.globalSettingFileName);
        }
    }
}
#endif