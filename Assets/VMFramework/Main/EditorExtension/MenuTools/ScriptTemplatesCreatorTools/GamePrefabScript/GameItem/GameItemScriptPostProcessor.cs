#if UNITY_EDITOR
using VMFramework.Core.Editor;

namespace VMFramework.Editor
{
    public sealed class GameItemScriptPostProcessor : ScriptCreationPostProcessor<GameItemScriptExtraInfo>
    {
        protected override void PostProcess(string scriptAbsolutePath, ref string scriptContent,
            GameItemScriptExtraInfo extraInfo)
        {
            Replace(ref scriptContent, "PARENT_CLASS_NAME", extraInfo.parentClassName);
            Replace(ref scriptContent, "PARENT_INTERFACE_NAME", extraInfo.parentInterfaceName);
        }
    }
}
#endif