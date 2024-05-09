#if FISHNET

using FishNet.Object;
using UnityEngine;
using VMFramework.Network;

namespace VMFramework.Procedure
{
    [ManagerCreationProvider(ManagerType.NetworkCore)]
    [RequireComponent(typeof(NetworkObject))]
    public class NetworkProcedureTriggerController : 
        UniqueNetworkBehaviour<NetworkProcedureTriggerController>
    {
        public override void OnStartServer()
        {
            base.OnStartServer();

            ProcedureManager.AddToSwitchQueue(MainMenuProcedure.ID, ServerLoadingProcedure.ID);
        }

        public override void OnStartClient()
        {
            base.OnStartClient();

            ProcedureManager.AddToSwitchQueue(MainMenuProcedure.ID, ClientLoadingProcedure.ID);
        }

        public override void OnStopClient()
        {
            base.OnStopClient();

            ProcedureManager.ExitProcedure(ClientRunningProcedure.ID);

            if (IsServerStarted == false)
            {
                ProcedureManager.EnterProcedure(MainMenuProcedure.ID);
            }
        }

        public override void OnStopServer()
        {
            base.OnStopServer();

            ProcedureManager.ExitProcedure(ServerRunningProcedure.ID);

            if (IsClientStarted == false)
            {
                ProcedureManager.EnterProcedure(MainMenuProcedure.ID);
            }
        }
    }
}

#endif
