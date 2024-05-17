#if FISHNET
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using FishNet;
using FishNet.Connection;
using FishNet.Object;
using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.Core;

namespace VMFramework.Containers
{
    public partial class ContainerManager
    {
        #region Reconcile Item On Observers

        private static void ReconcileItemOnObservers(ContainerInfo containerInfo, int slotIndex)
        {
            foreach (var observer in containerInfo.observers)
            {
                var observerConn = InstanceFinder.ServerManager.Clients[observer];

                if (observerConn.IsHost)
                {
                    continue;
                }

                if (instance.isDebugging)
                {
                    Debug.LogWarning($"准备Reconcile客户端：{observer}");
                }

                instance.ReconcileOnTarget(observerConn, containerInfo.owner.uuid,
                    slotIndex, containerInfo.owner.GetItem(slotIndex) as ContainerItem);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected static void ReconcileItemOnObservers(string containerUUID, int slotIndex)
        {
            if (TryGetInfo(containerUUID, out var info) == false)
            {
                Debug.LogWarning($"不存在此{containerUUID}对应的{nameof(info)}");
                return;
            }

            ReconcileItemOnObservers(info, slotIndex);
        }

        #endregion

        #region Reconcile Some Items On Observers

        private static void ReconcileSomeItemsOnObservers(ContainerInfo containerInfo, HashSet<int> slotIndices)
        {
            var items = new Dictionary<int, IContainerItem>();

            foreach (var slotIndex in slotIndices)
            {
                items.Add(slotIndex, containerInfo.owner.GetItem(slotIndex));
            }

            foreach (var observer in containerInfo.observers)
            {
                var observerConn = InstanceFinder.ServerManager.Clients[observer];

                if (observerConn.IsHost)
                {
                    continue;
                }

                if (instance.isDebugging)
                {
                    Debug.LogWarning($"准备Reconcile客户端：{observer}");
                }

                instance.ReconcileSomeOnTarget(observerConn, containerInfo.owner.uuid, items);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected static void ReconcileSomeItemsOnObservers(string containerUUID, HashSet<int> slotIndices)
        {
            if (TryGetInfo(containerUUID, out var info) == false)
            {
                Debug.LogWarning($"不存在此{containerUUID}对应的{nameof(info)}");
                return;
            }

            ReconcileSomeItemsOnObservers(info, slotIndices);
        }

        #endregion
        
        #region Reconcile On Observers

        private static void ReconcileAllItemsOnObservers(ContainerInfo containerInfo)
        {
            foreach (var observer in containerInfo.observers)
            {
                var observerConn = InstanceFinder.ServerManager.Clients[observer];

                if (observerConn.IsHost)
                {
                    continue;
                }

                if (instance.isDebugging)
                {
                    Debug.LogWarning($"准备Reconcile客户端：{observer}");
                }

                instance.ReconcileAllOnTarget(observerConn, containerInfo.owner.uuid,
                    containerInfo.owner.GetItemArray());
            }
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [Button]
        protected static void ReconcileAllItemsOnObservers(string containerUUID)
        {
            if (TryGetInfo(containerUUID, out var info) == false)
            {
                Debug.LogWarning($"不存在此{containerUUID}对应的{nameof(info)}");
                return;
            }

            ReconcileAllItemsOnObservers(info);
        }

        #endregion
        
        #region Reconcile On Target

        [TargetRpc(ExcludeServer = true)]
        [ObserversRpc(ExcludeServer = true)]
        private void ReconcileOnTarget(NetworkConnection connection, string containerUUID,
            int slotIndex, IContainerItem item)
        {
            if (isDebugging)
            {
                Debug.LogWarning($"正在恢复{containerUUID}的第{slotIndex}个物品，恢复为：{item}");
            }

            if (TryGetOwner(containerUUID, out var container))
            {
                container.SetItem(slotIndex, item);
            }
            else
            {
                Debug.LogWarning(
                    $"不存在此{nameof(containerUUID)}:{containerUUID}对应的{nameof(container)}");
            }
        }

        [TargetRpc(ExcludeServer = true)]
        [ObserversRpc(ExcludeServer = true)]
        private void ReconcileSomeOnTarget(NetworkConnection connection, string containerUUID,
            Dictionary<int, IContainerItem> items)
        {
            if (TryGetOwner(containerUUID, out var container))
            {
                foreach (var (slotIndex, item) in items)
                {
                    container.SetItem(slotIndex, item);
                }
            }
            else
            {
                Debug.LogWarning(
                    $"不存在此{nameof(containerUUID)}:{containerUUID}对应的{nameof(container)}");
            }
        }

        [TargetRpc(ExcludeServer = true)]
        [ObserversRpc(ExcludeServer = true)]
        private void ReconcileAllOnTarget(NetworkConnection connection, string containerUUID,
            IContainerItem[] items)
        {
            if (TryGetOwner(containerUUID, out var container))
            {
                container.LoadFromItemArray(items);
            }
            else
            {
                Debug.LogWarning(
                    $"不存在此{nameof(containerUUID)}:{containerUUID}对应的{nameof(container)}");
            }
        }

        private static void ReconcileAllOnTarget(NetworkConnection connection,
            string containerUUID)
        {
            if (TryGetInfo(containerUUID, out var info))
            {
                instance.ReconcileAllOnTarget(connection, containerUUID,
                    info.owner.GetItemArray());
            }
        }

        #endregion
        
        #region Request Reconcile

        [Client]
        public static void RequestReconcile(IContainer container, int slotIndex)
        {
            container.AssertIsNotNull(nameof(container));

            instance.RequestReconcile(container.uuid, slotIndex);
        }

        [ServerRpc(RequireOwnership = false)]
        private void RequestReconcile(string containerUUID, int slotIndex,
            NetworkConnection connection = null)
        {
            if (TryGetOwner(containerUUID, out var container))
            {
                var item = container.GetItem(slotIndex);

                ReconcileOnTarget(connection, containerUUID, slotIndex, item);
            }
            else
            {
                Debug.LogWarning(
                    $"不存在此{nameof(containerUUID)}:{containerUUID}对应的{nameof(container)}");
            }
        }

        [Client]
        public static void RequestReconcileAll(IContainer container)
        {
            container.AssertIsNotNull(nameof(container));

            instance.RequestReconcileAll(container.uuid);
        }

        [ServerRpc(RequireOwnership = false)]
        private void RequestReconcileAll(string containerUUID,
            NetworkConnection connection = null)
        {
            if (TryGetOwner(containerUUID, out var container))
            {
                ReconcileAllOnTarget(connection, containerUUID,
                    container.GetItemArray());
            }
        }

        #endregion
    }
}
#endif