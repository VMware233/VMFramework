#if UNITY_EDITOR
using System;
using VMFramework.Procedure.Editor;

namespace VMFramework.GameLogicArchitecture.Editor
{
    public class GameTypeEditorInitializer : IEditorInitializer
    {
        public void OnPreInit(Action onDone)
        {
            GameCoreSettingBase.gameTypeGeneralSetting.InitGameTypeInfo();
            
            onDone();
        }
    }
}
#endif