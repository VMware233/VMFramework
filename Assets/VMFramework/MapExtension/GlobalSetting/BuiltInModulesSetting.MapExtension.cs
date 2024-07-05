using VMFramework.Map;

namespace VMFramework.GameLogicArchitecture
{
    public partial class BuiltInModulesSetting
    {
        public static MapCoreGeneralSetting mapCoreGeneralSetting =>
            globalSettingFile == null ? null : globalSettingFile.mapCoreGeneralSetting;
    }
}