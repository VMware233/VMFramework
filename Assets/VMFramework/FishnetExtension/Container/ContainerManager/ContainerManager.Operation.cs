#if FISHNET
using FishNet.Connection;
using FishNet.Object;
using UnityEngine;

namespace VMFramework.Containers
{
    public partial class ContainerManager
    {
        #region Add Or Swap Item

        [Client]
        public static void AddOrSwapItem(IContainer fromContainer, int fromSlotIndex, IContainer toContainer,
            int toSlotIndex)
        {
            if (fromContainer == null || toContainer == null)
            {
                Debug.LogWarning($"{nameof(fromContainer)}或{nameof(toContainer)}为Null");
                return;
            }

            fromContainer.AddOrSwapItemTo(fromSlotIndex, toContainer, toSlotIndex);

            instance.RequestAddOrSwapItem(fromContainer.uuid, fromSlotIndex, toContainer.uuid, toSlotIndex);
        }

        [ServerRpc(RequireOwnership = false)]
        public void RequestAddOrSwapItem(string fromContainerUUID, int fromSlotIndex, string toContainerUUID,
            int toSlotIndex, NetworkConnection connection = null)
        {
            if (TryGetInfo(fromContainerUUID, out var fromContainerInfo) == false)
            {
                Debug.LogWarning($"不存在此{fromContainerUUID}对应的Container");
                return;
            }

            if (TryGetInfo(toContainerUUID, out var toContainerInfo) == false)
            {
                Debug.LogWarning($"不存在此{toContainerUUID}对应的Container");
                return;
            }

            var fromContainer = fromContainerInfo.owner;
            var toContainer = toContainerInfo.owner;

            if (connection.IsHost == false)
            {
                fromContainer.AddOrSwapItemTo(fromSlotIndex, toContainer, toSlotIndex);
            }
        }

        #endregion

        #region Split Item To

        [Client]
        public static void SplitItemTo(IContainer fromContainer, int fromSlotIndex, int count,
            IContainer toContainer, int toSlotIndex)
        {
            if (fromContainer == null || toContainer == null)
            {
                Debug.LogWarning($"{nameof(fromContainer)}或{nameof(toContainer)}为Null");
                return;
            }

            if (fromContainer.TrySplitItemTo(fromSlotIndex, count, toContainer, toSlotIndex))
            {
                instance.RequestSplitItemTo(fromContainer.uuid, fromSlotIndex, count, toContainer.uuid,
                    toSlotIndex);
            }
            else
            {
                RequestReconcile(fromContainer, fromSlotIndex);
                RequestReconcile(toContainer, toSlotIndex);
            }
        }

        [ServerRpc(RequireOwnership = false)]
        private void RequestSplitItemTo(string fromContainerUUID, int fromSlotIndex, int count,
            string toContainerUUID, int toSlotIndex, NetworkConnection connection = null)
        {
            if (TryGetInfo(fromContainerUUID, out var fromContainerInfo) == false)
            {
                Debug.LogWarning($"不存在此{fromContainerUUID}对应的Container");
                return;
            }

            if (TryGetInfo(toContainerUUID, out var toContainerInfo) == false)
            {
                Debug.LogWarning($"不存在此{toContainerUUID}对应的Container");
                return;
            }

            var fromContainer = fromContainerInfo.owner;
            var toContainer = toContainerInfo.owner;

            if (connection.IsHost == false)
            {
                fromContainer.TrySplitItemTo(fromSlotIndex, count, toContainer, toSlotIndex);
            }
        }

        #endregion

        #region Pop Items To

        /// <summary>
        ///     Pop Items to Other Container either on Client or Server
        /// </summary>
        /// <param name="fromContainer"></param>
        /// <param name="count"></param>
        /// <param name="toContainer"></param>
        public static void PopItemsTo(Container fromContainer, int count, Container toContainer)
        {
            if (fromContainer == null || toContainer == null)
            {
                Debug.LogWarning($"{nameof(fromContainer)}或{nameof(toContainer)}为Null");
                return;
            }

            fromContainer.PopItemsTo(count, toContainer, out var remainingCount);

            if (remainingCount < count)
            {
                instance.RequestPopItemsTo(fromContainer.uuid, count, toContainer.uuid);
            }
            else if (instance.IsClientStarted)
            {
                RequestReconcileAll(fromContainer);
                RequestReconcileAll(toContainer);
            }
        }

        [ServerRpc(RequireOwnership = false)]
        private void RequestPopItemsTo(string fromContainerUUID, int count, string toContainerUUID,
            NetworkConnection connection = null)
        {
            if (TryGetInfo(fromContainerUUID, out var fromContainerInfo) == false)
            {
                Debug.LogWarning($"不存在此{fromContainerUUID}对应的Container");
                return;
            }

            if (TryGetInfo(toContainerUUID, out var toContainerInfo) == false)
            {
                Debug.LogWarning($"不存在此{toContainerUUID}对应的Container");
                return;
            }

            var fromContainer = fromContainerInfo.owner;
            var toContainer = toContainerInfo.owner;

            if (connection.IsHost == false)
            {
                fromContainer.PopItemsTo(count, toContainer, out _);
            }
        }

        #endregion

        #region Pop All Items To

        [Client]
        public static void PopAllItemsTo(Container fromContainer, Container toContainer)
        {
            if (fromContainer == null || toContainer == null)
            {
                Debug.LogWarning($"{nameof(fromContainer)}或{nameof(toContainer)}为Null");
                return;
            }

            fromContainer.PopAllItemsTo(toContainer);

            instance.RequestPopAllItemsTo(fromContainer.uuid, toContainer.uuid);
        }

        [ServerRpc(RequireOwnership = false)]
        private void RequestPopAllItemsTo(string fromContainerUUID, string toContainerUUID,
            NetworkConnection connection = null)
        {
            if (TryGetInfo(fromContainerUUID, out var fromContainerInfo) == false)
            {
                Debug.LogWarning($"不存在此{fromContainerUUID}对应的Container");
                return;
            }

            if (TryGetInfo(toContainerUUID, out var toContainerInfo) == false)
            {
                Debug.LogWarning($"不存在此{toContainerUUID}对应的Container");
                return;
            }

            var fromContainer = fromContainerInfo.owner;
            var toContainer = toContainerInfo.owner;

            if (connection.IsHost == false)
            {
                fromContainer.PopAllItemsTo(toContainer);
            }
        }

        #endregion

        #region Stack Item

        [Client]
        public static void StackItem(Container container)
        {
            if (container == null)
            {
                Debug.LogWarning($"{nameof(container)}为Null");
                return;
            }

            container.StackItems();

            instance.StackItemRequest(container.uuid);
        }

        [ServerRpc(RequireOwnership = false)]
        private void StackItemRequest(string containerUUID, NetworkConnection connection = null)
        {
            if (TryGetInfo(containerUUID, out var containerInfo) == false)
            {
                Debug.LogWarning($"不存在此{containerUUID}对应的Container");
                return;
            }

            var container = containerInfo.owner;

            if (connection.IsHost == false)
            {
                container.StackItems();
            }
        }

        #endregion
    }
}
#endif