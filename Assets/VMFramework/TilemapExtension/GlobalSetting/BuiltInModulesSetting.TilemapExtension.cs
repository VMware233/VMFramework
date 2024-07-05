using VMFramework.ExtendedTilemap;

namespace VMFramework.GameLogicArchitecture
{
    public partial class BuiltInModulesSetting
    {
        public static ExtendedRuleTileGeneralSetting extendedRuleTileGeneralSetting =>
            globalSettingFile == null ? null : globalSettingFile.extendedRuleTileGeneralSetting;
    }
}