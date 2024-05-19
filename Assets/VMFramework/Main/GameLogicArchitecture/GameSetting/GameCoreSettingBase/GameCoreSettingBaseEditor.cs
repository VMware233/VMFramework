#if UNITY_EDITOR
using VMFramework.Editor;
using VMFramework.Editor.GameEditor;
using VMFramework.GameLogicArchitecture.Editor;

namespace VMFramework.GameLogicArchitecture
{
    public partial class GameCoreSettingBase
    {
        internal static GameEditorGeneralSetting gameEditorGeneralSetting =>
            gameCoreSettingsFileBase == null ? null : gameCoreSettingsFileBase.gameEditorGeneralSetting;

        public static ColorfulHierarchyGeneralSetting colorfulHierarchyGeneralSetting =>
            gameCoreSettingsFileBase == null
                ? null
                : gameCoreSettingsFileBase.colorfulHierarchyGeneralSetting;

        public static HierarchyComponentIconGeneralSetting hierarchyComponentIconGeneralSetting =>
            gameCoreSettingsFileBase == null
                ? null
                : gameCoreSettingsFileBase.hierarchyComponentIconGeneralSetting;

        public static GamePrefabWrapperGeneralSetting gamePrefabWrapperGeneralSetting =>
            gameCoreSettingsFileBase == null
                ? null
                : gameCoreSettingsFileBase.gamePrefabWrapperGeneralSetting;
        
        public static TextureImporterGeneralSetting textureImporterGeneralSetting =>
            gameCoreSettingsFileBase == null
                ? null
                : gameCoreSettingsFileBase.textureImporterGeneralSetting;
    }
}
#endif