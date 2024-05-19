#if UNITY_EDITOR
using VMFramework.Localization;

namespace VMFramework.Editor.GameEditor
{
    public static class GameEditorNames
    {
        #region Game Editor Name

        public const string GAME_EDITOR_DEFAULT_NAME = "Game Editor";
        public const string GAME_EDITOR_NAME_KEY = "GameEditorName";

        public static string gameEditorName => LocalizationEditorManager.GetStringOfEditorTable(GAME_EDITOR_NAME_KEY,
            GAME_EDITOR_DEFAULT_NAME);

        #endregion

        #region Category Names

        public const string AUXILIARY_TOOLS_CATEGORY_DEFAULT_NAME = "Auxiliary Tools";
        public const string AUXILIARY_TOOLS_CATEGORY_NAME_KEY = "AuxiliaryToolsCategoryName";

        public static string auxiliaryToolsCategoryName =>
            LocalizationEditorManager.GetStringOfEditorTable(AUXILIARY_TOOLS_CATEGORY_NAME_KEY,
                AUXILIARY_TOOLS_CATEGORY_DEFAULT_NAME);
        
        public const string GENERAL_SETTINGS_CATEGORY_DEFAULT_NAME = "General Settings";
        public const string GENERAL_SETTINGS_CATEGORY_NAME_KEY = "GeneralSettingsCategoryName";

        public static string generalSettingsCategoryName =>
            LocalizationEditorManager.GetStringOfEditorTable(GENERAL_SETTINGS_CATEGORY_NAME_KEY,
                GENERAL_SETTINGS_CATEGORY_DEFAULT_NAME);
        
        public const string EDITOR_CATEGORY_DEFAULT_NAME = "Editor";
        public const string EDITOR_CATEGORY_NAME_KEY = "EditorCategoryName";

        public static string editorCategoryName =>
            LocalizationEditorManager.GetStringOfEditorTable(EDITOR_CATEGORY_NAME_KEY,
                EDITOR_CATEGORY_DEFAULT_NAME);
        
        public const string CORE_CATEGORY_DEFAULT_NAME = "Core";
        public const string CORE_CATEGORY_NAME_KEY = "CoreCategoryName";
        
        public static string coreCategoryName =>
            LocalizationEditorManager.GetStringOfEditorTable(CORE_CATEGORY_NAME_KEY,
                CORE_CATEGORY_DEFAULT_NAME);
        
        public const string BUILT_IN_CATEGORY_DEFAULT_NAME = "Built-In";
        public const string BUILT_IN_CATEGORY_NAME_KEY = "BuiltInCategoryName";
        
        public static string builtInCategoryName =>
            LocalizationEditorManager.GetStringOfEditorTable(BUILT_IN_CATEGORY_NAME_KEY,
                BUILT_IN_CATEGORY_DEFAULT_NAME);
        
        public const string RESOURCES_MANAGEMENT_CATEGORY_DEFAULT_NAME = "Resources Management";
        public const string RESOURCES_MANAGEMENT_CATEGORY_NAME_KEY = "ResourcesManagementCategoryName";
        
        public static string resourcesManagementCategoryName =>
            LocalizationEditorManager.GetStringOfEditorTable(RESOURCES_MANAGEMENT_CATEGORY_NAME_KEY,
                RESOURCES_MANAGEMENT_CATEGORY_DEFAULT_NAME);

        #endregion

        #region Tool Bar Names

        public const string OPEN_SCRIPT_BUTTON_DEFAULT_NAME = "Open Script";
        public const string OPEN_SCRIPT_BUTTON_NAME_KEY = "OpenScriptButtonName";
        
        public static string openScriptButtonName =>
            LocalizationEditorManager.GetStringOfEditorTable(OPEN_SCRIPT_BUTTON_NAME_KEY,
                OPEN_SCRIPT_BUTTON_DEFAULT_NAME);
        
        public const string OPEN_GAME_PREFAB_SCRIPT_BUTTON_DEFAULT_NAME = "Open Game Prefab Script";
        public const string OPEN_GAME_PREFAB_SCRIPT_BUTTON_NAME_KEY = "OpenGamePrefabScriptButtonName";
        
        public static string openGamePrefabScriptButtonName =>
            LocalizationEditorManager.GetStringOfEditorTable(OPEN_GAME_PREFAB_SCRIPT_BUTTON_NAME_KEY,
                OPEN_GAME_PREFAB_SCRIPT_BUTTON_DEFAULT_NAME);
        
        public const string OPEN_GAME_ITEM_SCRIPT_BUTTON_DEFAULT_NAME = "Open Game Item Script";
        public const string OPEN_GAME_ITEM_SCRIPT_BUTTON_NAME_KEY = "OpenGameItemScriptButtonName";
        
        public static string openGameItemScriptButtonName =>
            LocalizationEditorManager.GetStringOfEditorTable(OPEN_GAME_ITEM_SCRIPT_BUTTON_NAME_KEY,
                OPEN_GAME_ITEM_SCRIPT_BUTTON_DEFAULT_NAME);
        
        public const string OPEN_CONTROLLER_SCRIPT_BUTTON_DEFAULT_NAME = "Open Controller Script";
        public const string OPEN_CONTROLLER_SCRIPT_BUTTON_NAME_KEY = "OpenControllerScriptButtonName";
        
        public static string openControllerScriptButtonName =>
            LocalizationEditorManager.GetStringOfEditorTable(OPEN_CONTROLLER_SCRIPT_BUTTON_NAME_KEY,
                OPEN_CONTROLLER_SCRIPT_BUTTON_DEFAULT_NAME);
        
        public const string SAVE_BUTTON_DEFAULT_NAME = "Save";
        public const string SAVE_BUTTON_NAME_KEY = "SaveButtonName";
        
        public static string saveButtonName =>
            LocalizationEditorManager.GetStringOfEditorTable(SAVE_BUTTON_NAME_KEY,
                SAVE_BUTTON_DEFAULT_NAME);
        
        public const string SAVE_ALL_BUTTON_DEFAULT_NAME = "Save All";
        public const string SAVE_ALL_BUTTON_NAME_KEY = "SaveAllButtonName";
        
        public static string saveAllButtonName =>
            LocalizationEditorManager.GetStringOfEditorTable(SAVE_ALL_BUTTON_NAME_KEY,
                SAVE_ALL_BUTTON_DEFAULT_NAME);

        #endregion
    }
}
#endif