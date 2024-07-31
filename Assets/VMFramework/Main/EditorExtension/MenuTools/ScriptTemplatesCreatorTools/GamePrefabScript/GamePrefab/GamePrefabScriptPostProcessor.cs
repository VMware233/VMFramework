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
            
            Region(ref scriptContent, "PARENT_INTERFACE_REGION", extraInfo.enableParentInterfaceRegion);
            Replace(ref scriptContent, "PARENT_INTERFACE_NAME", extraInfo.parentInterfaceName);
            
            Region(ref scriptContent, "ID_SUFFIX_OVERRIDE_REGION", extraInfo.enableIDSuffixOverrideRegion);
            Replace(ref scriptContent, "ID_SUFFIX", extraInfo.idSuffix);
            
            Region(ref scriptContent, "GAME_ITEM_TYPE_OVERRIDE_REGION", extraInfo.enableGameItemTypeOverrideRegion);
            Replace(ref scriptContent, "GAME_ITEM_TYPE", extraInfo.gameItemType);
        }
    }
}
#endif