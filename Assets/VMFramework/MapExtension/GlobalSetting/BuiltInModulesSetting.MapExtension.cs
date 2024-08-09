using VMFramework.Maps;

namespace VMFramework.GameLogicArchitecture
{
    public partial class BuiltInModulesSetting
    {
        public static MapCoreGeneralSetting mapCoreGeneralSetting =>
            GlobalSettingFile == null ? null : GlobalSettingFile.mapCoreGeneralSetting;
        
        public static GridMapGeneralSetting GridMapGeneralSetting =>
            GlobalSettingFile == null ? null : GlobalSettingFile.gridMapGeneralSetting;
        
        public static ExtendedRuleTileGeneralSetting ExtendedRuleTileGeneralSetting =>
            GlobalSettingFile == null ? null : GlobalSettingFile.extendedRuleTileGeneralSetting;
    }
}