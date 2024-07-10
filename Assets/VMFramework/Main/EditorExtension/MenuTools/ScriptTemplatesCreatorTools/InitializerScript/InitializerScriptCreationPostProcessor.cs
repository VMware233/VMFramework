#if UNITY_EDITOR
using VMFramework.Core.Editor;

namespace VMFramework.Editor
{
    public abstract class InitializerScriptCreationPostProcessor<TExtraInfo>
        : ScriptCreationPostProcessor<TExtraInfo> where TExtraInfo : InitializerScriptCreationExtraInfo
    {
        protected override void PostProcess(string scriptAbsolutePath, ref string scriptContent,
            TExtraInfo extraInfo)
        {
            Replace(ref scriptContent, "INITIALIZATION_ORDER_NAME", extraInfo.initializationOrderName);
        }
    }
}
#endif