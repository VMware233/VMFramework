using VMFramework.Maps;

namespace VMFramework.GameLogicArchitecture
{
    public partial class BuiltInModulesSetting
    {
        public static MapCoreGeneralSetting mapCoreGeneralSetting =>
            globalSettingFile == null ? null : globalSettingFile.mapCoreGeneralSetting;
        
        public static ExtendedRuleTileGeneralSetting extendedRuleTileGeneralSetting =>
            globalSettingFile == null ? null : globalSettingFile.extendedRuleTileGeneralSetting;
    }
}