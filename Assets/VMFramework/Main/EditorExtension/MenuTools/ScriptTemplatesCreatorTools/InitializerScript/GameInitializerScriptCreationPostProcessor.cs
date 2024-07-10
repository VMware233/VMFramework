#if UNITY_EDITOR
using VMFramework.Procedure;

namespace VMFramework.Editor
{
    public sealed class GameInitializerScriptCreationPostProcessor
        : InitializerScriptCreationPostProcessor<GameInitializerScriptCreationExtraInfo>
    {
        protected override void PostProcess(string scriptAbsolutePath, ref string scriptContent, GameInitializerScriptCreationExtraInfo extraInfo)
        {
            base.PostProcess(scriptAbsolutePath, ref scriptContent, extraInfo);

            string loadingTypeParameter = nameof(ProcedureLoadingType) + "." + extraInfo.loadingType;
            
            Replace(ref scriptContent, "LOADING_TYPE_PARAMETER", loadingTypeParameter);
        }
    }
}
#endif