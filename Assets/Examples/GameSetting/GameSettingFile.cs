using VMFramework.GameLogicArchitecture;
using VMFramework.GameLogicArchitecture.Editor;

namespace VMFramework.Examples
{
    [GlobalSettingFileConfig(FileName = nameof(GameSettingFile))]
    [GlobalSettingFileEditorConfig(FolderPath = ConfigurationPath.DEFAULT_GLOBAL_SETTINGS_PATH)]
    public sealed partial class GameSettingFile : GlobalSettingFile
    {
        // Write Your Custom General Settings Here
        
        public PlayerGeneralSetting playerGeneralSetting;
    }
}