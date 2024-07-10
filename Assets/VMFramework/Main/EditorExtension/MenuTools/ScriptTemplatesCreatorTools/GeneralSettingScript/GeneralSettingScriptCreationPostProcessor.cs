using VMFramework.Core.Editor;

namespace VMFramework.Editor
{
    public sealed class GeneralSettingScriptCreationPostProcessor
        : ScriptCreationPostProcessor<GeneralSettingScriptCreationExtraInfo>
    {
        protected override void PostProcess(string scriptAbsolutePath, ref string scriptContent,
            GeneralSettingScriptCreationExtraInfo extraInfo)
        {
            Replace(ref scriptContent, "NAME_IN_GAME_EDITOR", extraInfo.nameInGameEditor);
        }
    }
}