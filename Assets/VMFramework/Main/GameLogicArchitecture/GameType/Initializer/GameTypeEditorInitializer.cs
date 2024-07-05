#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEngine;
using VMFramework.Procedure;
using VMFramework.Procedure.Editor;

namespace VMFramework.GameLogicArchitecture.Editor
{
    internal sealed class GameTypeEditorInitializer : IEditorInitializer
    {
        IEnumerable<InitializationAction> IInitializer.GetInitializationActions()
        {
            yield return new(InitializationOrder.PreInit, OnPreInit, this);
        }

        private static void OnPreInit(Action onDone)
        {
            if (CoreSetting.gameTypeGeneralSetting == null)
            {
                Debug.LogError("GameTypeGeneralSetting is not set. Please set it in the GameCoreSetting.");
                
                onDone();
                return;
            }
            
            CoreSetting.gameTypeGeneralSetting.InitGameTypeInfo();
            
            onDone();
        }
    }
}
#endif