#if UNITY_EDITOR
namespace VMFramework.Editor.GameEditor
{
    public static class GameEditorNames
    {
        public const string GAME_EDITOR_NAME = "Game Editor";
        
        public const string OPEN_GAME_EDITOR_SCRIPT_BUTTON = "Open Game Editor Script";

        public const string OPEN_GAME_EDITOR_SCRIPT_BUTTON_PATH =
            EditorNames.OPEN_SCRIPT_BUTTON + "/" + OPEN_GAME_EDITOR_SCRIPT_BUTTON;

        #region Category Names
        
        public const string GENERAL_SETTINGS_CATEGORY = "General Settings";
        
        public const string CORE_CATEGORY = "Core";
        
        public const string BUILT_IN_CATEGORY = "Built-In";
        
        public const string RESOURCES_MANAGEMENT_CATEGORY = "Res. Management";

        #endregion
    }
}
#endif