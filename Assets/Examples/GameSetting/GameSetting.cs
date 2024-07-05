using VMFramework.GameLogicArchitecture;
using VMFramework.Procedure;

namespace VMFramework.Examples
{
    [ManagerCreationProvider(ManagerType.SettingCore)]
    public sealed partial class GameSetting : GlobalSetting<GameSetting, GameSettingFile>
    {
        // Write Your Custom General Settings Here
        
        public static PlayerGeneralSetting playerGeneralSetting => globalSettingFile.playerGeneralSetting;
    }
}