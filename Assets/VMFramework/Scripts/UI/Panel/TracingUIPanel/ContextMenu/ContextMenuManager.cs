using UnityEngine;
using VMFramework.Procedure;

namespace VMFramework.UI
{
    [ManagerCreationProvider(ManagerType.UICore)]
    public sealed class ContextMenuManager : UniqueMonoBehaviour<ContextMenuManager>
    {
        public static void Open(IContextMenuProvider contextMenuProvider, IUIPanelController source)
        {
            if (contextMenuProvider == null)
            {
                Debug.LogWarning($"{nameof(contextMenuProvider)} is Null");
                return;
            }

            if (contextMenuProvider.DisplayContextMenu() == false)
            {
                return;
            }

            var contextMenuID = contextMenuProvider.GetContextMenuID();

            var contextMenu = UIPanelPool.GetUniquePanelStrictly<IContextMenu>(contextMenuID);

            contextMenu.Open(contextMenuProvider, source);
        }

        public static void Close(IContextMenuProvider contextMenuProvider)
        {
            if (contextMenuProvider == null)
            {
                Debug.LogWarning($"{nameof(contextMenuProvider)} is Null");
                return;
            }

            var contextMenuID = contextMenuProvider.GetContextMenuID();

            var contextMenu = UIPanelPool.GetUniquePanelStrictly<IContextMenu>(contextMenuID);

            contextMenu.Close(contextMenuProvider);
        }
    }
}