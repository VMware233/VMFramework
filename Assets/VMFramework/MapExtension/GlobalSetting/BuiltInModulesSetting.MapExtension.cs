using VMFramework.Maps;

namespace VMFramework.GameLogicArchitecture
{
    public partial class BuiltInModulesSetting
    {
        public static MapCoreGeneralSetting mapCoreGeneralSetting =>
            GlobalSettingFile == null ? null : GlobalSettingFile.mapCoreGeneralSetting;
        
        public static ExtendedRuleTileGeneralSetting extendedRuleTileGeneralSetting =>
            GlobalSettingFile == null ? null : GlobalSettingFile.extendedRuleTileGeneralSetting;
    }
}