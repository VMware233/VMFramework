#if UNITY_EDITOR
using VMFramework.Editor;
using VMFramework.Editor.GameEditor;
using VMFramework.Procedure;

namespace VMFramework.GameLogicArchitecture.Editor
{
    [ManagerCreationProvider(ManagerType.SettingCore)]
    public sealed class EditorSetting : GlobalSetting<EditorSetting, EditorSettingFile>
    {
        public static GameEditorGeneralSetting gameEditorGeneralSetting => 
            globalSettingFile == null? null : globalSettingFile.gameEditorGeneralSetting;

        public static HierarchyGeneralSetting hierarchyGeneralSetting => 
            globalSettingFile == null? null : globalSettingFile.hierarchyGeneralSetting;

        public static TextureImporterGeneralSetting textureImporterGeneralSetting =>
            globalSettingFile == null? null : globalSettingFile.textureImporterGeneralSetting;
        
        public static GamePrefabWrapperEditorGeneralSetting gamePrefabWrapperEditorGeneralSetting =>
            globalSettingFile == null? null : globalSettingFile.gamePrefabWrapperEditorGeneralSetting;

        public static string generalSettingsAssetFolderPath =>
            globalSettingFile == null
                ? ConfigurationPath.DEFAULT_GENERAL_SETTINGS_PATH
                : globalSettingFile.generalSettingsAssetFolderPath;
        
        public static string gamePrefabsAssetFolderPath =>
            globalSettingFile == null
                ? ConfigurationPath.DEFAULT_GAME_PREFABS_PATH
                : globalSettingFile.gamePrefabsAssetFolderPath;
    }
}
#endif