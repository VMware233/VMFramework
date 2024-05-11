using VMFramework.Map;

namespace VMFramework.GameLogicArchitecture
{
    public partial class GameCoreSettingBase
    {
        public static MapCoreGeneralSetting mapCoreGeneralSetting =>
            gameCoreSettingsFileBase == null ? null : gameCoreSettingsFileBase.mapCoreGeneralSetting;
    }
}