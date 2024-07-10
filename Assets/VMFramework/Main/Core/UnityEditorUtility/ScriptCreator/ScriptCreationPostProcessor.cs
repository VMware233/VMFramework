#if UNITY_EDITOR
using System;
using UnityEngine;

namespace VMFramework.Core.Editor
{
    public abstract class ScriptCreationPostProcessor<TExtraInfo> : ScriptCreationPostProcessor
    {
        protected sealed override void PostProcess(string scriptAbsolutePath, ref string scriptContent, ScriptCreationExtraInfo extraInfo)
        {
            base.PostProcess(scriptAbsolutePath, ref scriptContent, extraInfo);

            if (extraInfo is not TExtraInfo typedExtraInfo)
            {
                Debug.LogError($"{nameof(extraInfo)} is not of type {typeof(TExtraInfo)}!");
                return;
            }
            
            PostProcess(scriptAbsolutePath, ref scriptContent, typedExtraInfo);
        }

        protected abstract void PostProcess(string scriptAbsolutePath, ref string scriptContent, TExtraInfo extraInfo);
    }
    
    public class ScriptCreationPostProcessor : IScriptCreationPostProcessor
    {
        protected virtual void PostProcess(string scriptAbsolutePath, ref string scriptContent,
            ScriptCreationExtraInfo extraInfo)
        {
            var fileName = scriptAbsolutePath.GetFileNameFromPath();

            fileName = fileName[..fileName.IndexOf('.')];

            var namespaceName = extraInfo.namespaceName;

            if (namespaceName.IsNullOrWhiteSpace())
            {
                namespaceName = "#NAMESPACE_NAME#";
            }

            Replace(ref scriptContent, "SCRIPT_NAME", fileName);

            Replace(ref scriptContent, "NAMESPACE_BEGIN", $"namespace {namespaceName} \n{{");
            Replace(ref scriptContent, "NAMESPACE_END", "}");
        }

        protected static void Replace(ref string scriptContent, string tag, string value)
        {
            scriptContent = scriptContent.Replace($"#{tag}#", value);
        }
        
        void IScriptCreationPostProcessor.PostProcess(string scriptAbsolutePath, ref string scriptContent,
            ScriptCreationExtraInfo extraInfo)
        {
            PostProcess(scriptAbsolutePath, ref scriptContent, extraInfo);
        }
    }
}
#endif