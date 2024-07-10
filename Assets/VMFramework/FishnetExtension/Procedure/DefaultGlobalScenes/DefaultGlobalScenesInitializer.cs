#if FISHNET
using System;
using System.Collections.Generic;
using FishNet;
using FishNet.Managing.Scened;
using UnityEngine;
using UnityEngine.Scripting;
using VMFramework.Core;
using VMFramework.Network;

namespace VMFramework.Procedure
{
    [GameInitializerRegister(ServerRunningProcedure.ID, ProcedureLoadingType.OnEnter)]
    [Preserve]
    public sealed class DefaultGlobalScenesInitializer : IGameInitializer
    {
        IEnumerable<InitializationAction> IInitializer.GetInitializationActions()
        {
            yield return new(InitializationOrder.AfterInitComplete, OnAfterInitComplete, this);
        }

        private static void OnAfterInitComplete(Action onDone)
        {
            var sceneNames = NetworkSetting.defaultGlobalScenesGeneralSetting.sceneNames;

            Debug.Log("Loading Default Global Scenes : " + sceneNames.Join(", "));

            InstanceFinder.SceneManager.LoadGlobalScenes(new SceneLoadData(sceneNames));

            onDone();
        }
    }
}

#endif