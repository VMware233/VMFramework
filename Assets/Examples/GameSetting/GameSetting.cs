using VMFramework.GameLogicArchitecture;

namespace VMFramework.Examples
{
    public class GameSetting : GameCoreSetting
    {
        public static GameSettingFile gameSettingFile => (GameSettingFile)gameCoreSettingsFile;
        
        // Write Your Custom Game Settings Here
        public static PlayerGeneralSetting playerGeneralSetting => gameSettingFile.playerGeneralSetting;
    }
}