using VMFramework.ExtendedTilemap;

namespace VMFramework.GameLogicArchitecture
{
    public partial class GameCoreSettingBase
    {
        public static ExtendedRuleTileGeneralSetting extendedRuleTileGeneralSetting =>
            gameCoreSettingsFileBase == null ? null : gameCoreSettingsFileBase.extendedRuleTileGeneralSetting;
    }
}