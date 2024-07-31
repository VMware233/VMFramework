#if UNITY_EDITOR
using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.Configuration;
using VMFramework.GameLogicArchitecture;
using VMFramework.OdinExtensions;

namespace VMFramework.Editor
{
    public abstract class ScriptCreationViewer : BaseConfig, IScriptCreationViewer
    {
        [FolderPath]
        public string assetFolderPath;
        
        [IsClassName]
        [SuffixLabel("@" + nameof(nameSuffix))]
        public string name;
        
        [Namespace]
        public string namespaceName;

        protected virtual string nameSuffix => string.Empty;

        public string ClassName => name + nameSuffix;

        #region Interface Implementation

        string IScriptCreationViewer.AssetFolderPath
        {
            get => assetFolderPath;
            set => assetFolderPath = value;
        }

        string INameOwner.name => name;

        string IScriptCreationViewer.NamespaceName => namespaceName;

        #endregion
    }
}
#endif