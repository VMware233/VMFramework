#if FISHNET

using UnityEngine;

namespace VMFramework.Procedure
{
    [ManagerCreationProvider(ManagerType.NetworkCore)]
    public class NetworkProcedureTriggerController : NetworkManagerBehaviour<NetworkProcedureTriggerController>
    {
        public override void OnStartServer()
        {
            base.OnStartServer();

            Debug.Log("启动服务器");
            ProcedureManager.EnterProcedure(MainMenuProcedure.ID, ServerRunningProcedure.ID);
        }

        public override void OnStartClient()
        {
            base.OnStartClient();

            Debug.Log("启动客户端");

            if (ProcedureManager.HasCurrentProcedure(MainMenuProcedure.ID))
            {
                ProcedureManager.EnterProcedure(MainMenuProcedure.ID, ClientRunningProcedure.ID);
            }
            else
            {
                ProcedureManager.EnterProcedure(ClientRunningProcedure.ID);
            }
        }

        public override void OnStopClient()
        {
            base.OnStopClient();

            if (IsServerStarted == false)
            {
                ProcedureManager.EnterProcedure(ClientRunningProcedure.ID, MainMenuProcedure.ID);
            }
            else
            {
                ProcedureManager.ExitProcedureImmediately(ClientRunningProcedure.ID);
            }
        }

        public override void OnStopServer()
        {
            base.OnStopServer();

            if (IsClientStarted == false)
            {
                ProcedureManager.EnterProcedure(ServerRunningProcedure.ID, MainMenuProcedure.ID);
            }
            else
            {
                ProcedureManager.ExitProcedureImmediately(ServerRunningProcedure.ID);
            }
        }
    }
}

#endif
