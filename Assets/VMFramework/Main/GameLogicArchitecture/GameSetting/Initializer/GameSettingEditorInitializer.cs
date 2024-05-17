#if UNITY_EDITOR
using System;
using VMFramework.Procedure.Editor;

namespace VMFramework.GameLogicArchitecture.Editor
{
    internal class GameSettingEditorInitializer : IEditorInitializer
    {
        public void OnBeforeInit(Action onDone)
        {
            GameCoreSettingBaseFile.CheckGlobal();
            
            GameCoreSettingBase.gameCoreSettingsFileBase.AutoFindSetting();
            
            foreach (var generalSetting in GameCoreSettingBase.GetAllGeneralSettings())
            {
                if (generalSetting == null)
                {
                    continue;
                }
                
                generalSetting.InitializeOnLoad();
            }
            
            onDone();
        }
    }
}
#endif