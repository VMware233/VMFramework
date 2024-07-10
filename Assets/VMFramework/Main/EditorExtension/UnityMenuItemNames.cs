#if UNITY_EDITOR
namespace VMFramework.Editor
{
    public static class UnityMenuItemNames
    {
        public const string ASSETS_CREATION_VMFRAMEWORK = "Assets/Create/" + VMFRAMEWORK;
        
        public const string VMFRAMEWORK = "VMFramework/";
        
        public const string EDITOR_INITIALIZATION = VMFRAMEWORK + "Editor Initialization/";
        
        public const string FOLDERS_CREATION_TOOLS = VMFRAMEWORK + "Folders Creation Tools/";
        
        public const string GLOBAL_SETTINGS = VMFRAMEWORK + "Global Settings/";
        
        public const string GAME_PREFABS_TOOLS = VMFRAMEWORK + "Game Prefabs Tools/";
    }
}
#endif