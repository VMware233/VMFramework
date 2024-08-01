using UnityEngine;
using VMFramework.Configuration;
using VMFramework.Core;
using VMFramework.GameLogicArchitecture;
using VMFramework.Procedure;

namespace VMFramework.UI
{
    [ManagerCreationProvider(ManagerType.UICore)]
    public sealed class ContextMenuManager : ManagerBehaviour<ContextMenuManager>
    {
        private static ContextMenuGeneralSetting ContextMenuGeneralSetting => 
            UISetting.ContextMenuGeneralSetting;
        
        public static void Open(IContextMenuProvider contextMenuProvider, IUIPanelController source)
        {
            if (contextMenuProvider == null)
            {
                Debugger.LogWarning($"{nameof(contextMenuProvider)} is Null");
                return;
            }

            if (contextMenuProvider.DisplayContextMenu() == false)
            {
                return;
            }

            string contextMenuID = null;
            
            if (contextMenuProvider is IReadOnlyGameTypeOwner readOnlyGameTypeOwner)
            {
                if (ContextMenuGeneralSetting.contextMenuIDBindConfigs.TryGetConfigRuntime(
                        readOnlyGameTypeOwner.GameTypeSet, out var idBindConfig))
                {
                    contextMenuID = idBindConfig.contextMenuID;
                }
            }

            contextMenuID ??= ContextMenuGeneralSetting.defaultContextMenuID;

            if (UIPanelPool.TryGetUniquePanelWithWarning(contextMenuID, out IContextMenu contextMenu) == false)
            {
                return;
            }

            contextMenu.Open(contextMenuProvider, source);
        }

        public static void Close(IContextMenuProvider contextMenuProvider)
        {
            if (contextMenuProvider == null)
            {
                Debugger.LogWarning($"{nameof(contextMenuProvider)} is Null");
                return;
            }

            foreach (var contextMenu in UIPanelPool.GetUniquePanels<IContextMenu>())
            {
                contextMenu.Close(contextMenuProvider);
            }
        }
    }
}