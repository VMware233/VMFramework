#if UNITY_EDITOR
namespace VMFramework.Editor
{
    public static partial class EditorNames
    {
        #region Open Script

        public const string OPEN_SCRIPT_BUTTON = "Open Script";

        public const string OPEN_THIS_SCRIPT_BUTTON = "Open This Script";

        public const string OPEN_THIS_SCRIPT_BUTTON_PATH = OPEN_SCRIPT_BUTTON + "/" + OPEN_THIS_SCRIPT_BUTTON;

        public const string OPEN_GAME_PREFAB_SCRIPT_BUTTON = "Open Game Prefab Script";

        public const string OPEN_GAME_PREFAB_SCRIPT_BUTTON_PATH =
            OPEN_SCRIPT_BUTTON + "/" + OPEN_GAME_PREFAB_SCRIPT_BUTTON;

        public const string OPEN_GAME_ITEM_SCRIPT_BUTTON = "Open Game Item Script";

        public const string OPEN_GAME_ITEM_SCRIPT_BUTTON_PATH =
            OPEN_SCRIPT_BUTTON + "/" + OPEN_GAME_ITEM_SCRIPT_BUTTON;

        public const string OPEN_CONTROLLER_SCRIPT_BUTTON = "Open Controller Script";

        public const string OPEN_CONTROLLER_SCRIPT_BUTTON_PATH =
            OPEN_SCRIPT_BUTTON + "/" + OPEN_CONTROLLER_SCRIPT_BUTTON;

        public const string OPEN_GLOBAL_SETTING_SCRIPT_BUTTON = "Open Global Setting Script";

        public const string OPEN_GLOBAL_SETTING_SCRIPT_BUTTON_PATH =
            OPEN_SCRIPT_BUTTON + "/" + OPEN_GLOBAL_SETTING_SCRIPT_BUTTON;

        #endregion

        #region Save

        public const string SAVE_BUTTON = "Save";

        public const string SAVE_ALL_BUTTON = "Save All";

        #endregion

        #region Asset

        public const string ASSET_BUTTON = "Asset";

        public const string SELECT_ASSET_BUTTON = "Select Asset";

        public const string SELECT_ASSET_BUTTON_PATH = ASSET_BUTTON + "/" + SELECT_ASSET_BUTTON;

        public const string OPEN_ASSET_IN_NEW_INSPECTOR_BUTTON = "Open Asset in New Inspector";

        public const string OPEN_ASSET_IN_NEW_INSPECTOR_BUTTON_PATH =
            ASSET_BUTTON + "/" + OPEN_ASSET_IN_NEW_INSPECTOR_BUTTON;

        #endregion

        #region General Settings

        public const string GENERAL_SETTINGS_BUTTON = "General Settings";

        public const string AUTO_FIND_SETTINGS_BUTTON = "Auto Find Settings";

        public const string AUTO_FIND_SETTINGS_BUTTON_PATH =
            GENERAL_SETTINGS_BUTTON + "/" + AUTO_FIND_SETTINGS_BUTTON;

        public const string AUTO_FIND_AND_CREATE_SETTINGS_BUTTON = "Auto Find and Create Settings";

        public const string AUTO_FIND_AND_CREATE_SETTINGS_BUTTON_PATH =
            GENERAL_SETTINGS_BUTTON + "/" + AUTO_FIND_AND_CREATE_SETTINGS_BUTTON;

        #endregion
    }
}
#endif