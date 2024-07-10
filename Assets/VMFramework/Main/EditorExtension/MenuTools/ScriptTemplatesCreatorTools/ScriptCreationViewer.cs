#if UNITY_EDITOR
using Sirenix.OdinInspector;
using VMFramework.Configuration;
using VMFramework.GameLogicArchitecture;
using VMFramework.OdinExtensions;

namespace VMFramework.Editor
{
    public class ScriptCreationViewer : BaseConfig, IScriptCreationViewer
    {
        [IsClassName]
        [SuffixLabel("@" + nameof(nameSuffix))]
        public string name;
        [Namespace]
        public string namespaceName;

        protected virtual string nameSuffix => string.Empty;

        public string className => name + nameSuffix;

        string INameOwner.name => name;

        string IScriptCreationViewer.namespaceName => namespaceName;
    }
}
#endif