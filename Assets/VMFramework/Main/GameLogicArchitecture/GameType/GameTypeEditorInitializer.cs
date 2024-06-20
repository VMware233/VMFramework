#if UNITY_EDITOR
using System;
using UnityEngine;
using VMFramework.Procedure;
using VMFramework.Procedure.Editor;

namespace VMFramework.GameLogicArchitecture.Editor
{
    internal sealed class GameTypeEditorInitializer : IEditorInitializer
    {
        void IInitializer.OnPreInit(Action onDone)
        {
            if (GameCoreSetting.gameTypeGeneralSetting == null)
            {
                Debug.LogError("GameTypeGeneralSetting is not set. Please set it in the GameCoreSetting.");
                
                onDone();
                return;
            }
            
            GameCoreSetting.gameTypeGeneralSetting.InitGameTypeInfo();
            
            onDone();
        }
    }
}
#endif