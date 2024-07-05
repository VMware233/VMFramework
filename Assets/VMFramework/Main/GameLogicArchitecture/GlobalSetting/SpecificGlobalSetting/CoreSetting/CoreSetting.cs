using VMFramework.GameEvents;
using VMFramework.Procedure;

namespace VMFramework.GameLogicArchitecture
{
    [ManagerCreationProvider(ManagerType.SettingCore)]
    public sealed partial class CoreSetting : GlobalSetting<CoreSetting, CoreSettingFile>
    {
        public static GameTypeGeneralSetting gameTypeGeneralSetting =>
            globalSettingFile == null ? null : globalSettingFile.gameTypeGeneralSetting;
        
        public static GameEventGeneralSetting gameEventGeneralSetting =>
            globalSettingFile == null ? null : globalSettingFile.gameEventGeneralSetting;

        public static ColliderMouseEventGeneralSetting colliderMouseEventGeneralSetting =>
            globalSettingFile == null ? null : globalSettingFile.colliderMouseEventGeneralSetting;
    }
}