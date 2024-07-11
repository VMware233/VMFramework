#if UNITY_EDITOR
using VMFramework.Core.Editor;

namespace VMFramework.Editor
{
    public sealed class GamePrefabGeneralSettingScriptPostProcessor
        : ScriptCreationPostProcessor<GamePrefabGeneralSettingScriptExtraInfo>
    {
        protected override void PostProcess(string scriptAbsolutePath, ref string scriptContent,
            GamePrefabGeneralSettingScriptExtraInfo extraInfo)
        {
            Replace(ref scriptContent, "BASE_GAME_PREFAB_TYPE", extraInfo.baseGamePrefabType);
            Replace(ref scriptContent, "NAME_IN_GAME_EDITOR", extraInfo.nameInGameEditor);
        }
    }
}
#endif