using VMFramework.GameEvents;
using VMFramework.Procedure;

namespace VMFramework.GameLogicArchitecture
{
    [ManagerCreationProvider(ManagerType.SettingCore)]
    public sealed partial class CoreSetting : GlobalSetting<CoreSetting, CoreSettingFile>
    {
        public static GameTypeGeneralSetting GameTypeGeneralSetting =>
            GlobalSettingFile == null ? null : GlobalSettingFile.gameTypeGeneralSetting;
        
        public static GameEventGeneralSetting GameEventGeneralSetting =>
            GlobalSettingFile == null ? null : GlobalSettingFile.gameEventGeneralSetting;

        public static ColliderMouseEventGeneralSetting ColliderMouseEventGeneralSetting =>
            GlobalSettingFile == null ? null : GlobalSettingFile.colliderMouseEventGeneralSetting;
    }
}