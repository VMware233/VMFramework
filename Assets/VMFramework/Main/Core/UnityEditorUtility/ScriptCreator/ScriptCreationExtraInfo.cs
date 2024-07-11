#if UNITY_EDITOR
using System.Collections.Generic;

namespace VMFramework.Core.Editor
{
    public class ScriptCreationExtraInfo
    {
        public string namespaceName { get; init; }
        
        public IEnumerable<string> usingNamespaces { get; init; }
    }
}
#endif