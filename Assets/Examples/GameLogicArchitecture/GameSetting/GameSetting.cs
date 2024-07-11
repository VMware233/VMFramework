using VMFramework.GameLogicArchitecture;
using VMFramework.Procedure;

namespace VMFramework.Examples 
{
    [ManagerCreationProvider(ManagerType.SettingCore)]
    public sealed partial class GameSetting : GlobalSetting<GameSetting, GameSettingFile>
    {
        public static PlayerGeneralSetting playerGeneralSetting => globalSettingFile.playerGeneralSetting;
        
        public static EntityGeneralSetting entityGeneralSetting => globalSettingFile.entityGeneralSetting;
    }
}