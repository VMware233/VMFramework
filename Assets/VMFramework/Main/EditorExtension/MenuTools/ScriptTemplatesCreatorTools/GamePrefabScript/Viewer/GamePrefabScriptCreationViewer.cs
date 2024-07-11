#if UNITY_EDITOR
using Sirenix.OdinInspector;

namespace VMFramework.Editor
{
    public sealed class GamePrefabScriptCreationViewer : ScriptCreationViewer
    {
        public bool createSubFolders = true;
        
        [EnumToggleButtons]
        public GamePrefabBaseType gamePrefabBaseType;

        public bool withGamePrefabGeneralSetting = true;

        public bool withGameItem;
        
        [ShowIf(nameof(withGameItem))]
        [EnumToggleButtons]
        public GameItemBaseType gameItemBaseType;

        protected override string nameSuffix => withGameItem ? "Config" : string.Empty;
    }
}
#endif