#if UNITY_EDITOR
namespace VMFramework.Core.Editor
{
    public interface IScriptCreationPostProcessor
    {
        public void PostProcess(string scriptAbsolutePath, ref string scriptContent, ScriptCreationExtraInfo extraInfo);
    }
}
#endif