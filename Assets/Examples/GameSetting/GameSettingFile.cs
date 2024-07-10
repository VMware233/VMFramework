using VMFramework.GameLogicArchitecture;

namespace VMFramework.Examples 
{
    [GlobalSettingFileConfig(FileName = nameof(GameSettingFile))]
    public sealed partial class GameSettingFile : GlobalSettingFile
    {
        public PlayerGeneralSetting playerGeneralSetting;
    }
}