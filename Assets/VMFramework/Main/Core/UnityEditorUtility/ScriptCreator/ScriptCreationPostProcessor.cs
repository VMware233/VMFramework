#if UNITY_EDITOR
using System;
using System.Linq;
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
            string namespaceBegin = "";
            string namespaceEnd = "";

            if (namespaceName.IsNullOrWhiteSpace())
            {
                namespaceName = ScriptTemplateTags.NAMESPACE_NAME;
            }
            else
            {
                namespaceBegin = $"namespace {namespaceName} \n{{";
                namespaceEnd = "}";
            }

            string usingNamespacesContent = "";

            if (extraInfo.usingNamespaces != null)
            {
                usingNamespacesContent = extraInfo.usingNamespaces.Where(name => name.IsNullOrWhiteSpace() == false)
                    .Select(name => $"using {name};").Join("\n");
            }

            Replace(ref scriptContent, ScriptTemplateTags.CLASS_NAME, fileName);

            Replace(ref scriptContent, ScriptTemplateTags.NAMESPACE_BEGIN, namespaceBegin);
            Replace(ref scriptContent, ScriptTemplateTags.NAMESPACE_END, namespaceEnd);
            Replace(ref scriptContent, ScriptTemplateTags.NAMESPACE_NAME, namespaceName);
            Replace(ref scriptContent, ScriptTemplateTags.USING_NAMESPACES, usingNamespacesContent);
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