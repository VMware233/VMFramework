#if UNITY_EDITOR
using VMFramework.Core.Editor;

namespace VMFramework.Editor
{
    public sealed class GamePrefabScriptPostProcessor
        : ScriptCreationPostProcessor<GamePrefabScriptExtraInfo>
    {
        protected override void PostProcess(string scriptAbsolutePath, ref string scriptContent,
            GamePrefabScriptExtraInfo extraInfo)
        {
            Replace(ref scriptContent, "PARENT_CLASS_NAME", extraInfo.parentClassName);
            Replace(ref scriptContent, "PARENT_INTERFACE_NAME", extraInfo.parentInterfaceName);
            Replace(ref scriptContent, "ID_SUFFIX", extraInfo.idSuffix);
            Replace(ref scriptContent, "GAME_ITEM_TYPE", extraInfo.gameItemType);
        }
    }
}
#endif