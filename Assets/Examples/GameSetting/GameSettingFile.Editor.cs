#if UNITY_EDITOR
using VMFramework.GameLogicArchitecture;
using VMFramework.GameLogicArchitecture.Editor;

namespace VMFramework.Examples 
{
    [GlobalSettingFileEditorConfig(FolderPath = ConfigurationPath.DEFAULT_GLOBAL_SETTINGS_PATH)]
    public partial class GameSettingFile
    {
        
    }
}
#endif