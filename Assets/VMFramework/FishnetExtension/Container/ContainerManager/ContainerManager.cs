#if FISHNET
using FishNet.Object;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using FishNet.Connection;
using UnityEngine;
using VMFramework.Network;
using VMFramework.Procedure;

namespace VMFramework.Containers
{
    [ManagerCreationProvider(ManagerType.NetworkCore)]
    public partial class ContainerManager : 
        UUIDManager<ContainerManager, IContainer, ContainerManager.ContainerInfo>
    { 
        public class ContainerInfo : OwnerInfo
        {
            public HashSet<int> dirtySlots;
        }

        [SerializeField]
        protected bool isDebugging;

        protected override void OnBeforeInit()
        {
            base.OnBeforeInit();

            OnRegisterEvent += OnRegister;
            OnUnregisterEvent += OnUnregister;
        }

        #region Register & Unregister

        private static void OnRegister(ContainerInfo info)
        {
            if (_instance.IsServerStarted)
            {
                info.dirtySlots = new();
                info.owner.OnItemCountChangedEvent += OnContainerItemCountChanged;
                info.owner.OnItemAddedEvent += OnItemAdded;
                info.owner.OnItemRemovedEvent += OnItemRemoved;
            }
        }

        private static void OnUnregister(ContainerInfo info)
        {
            if (_instance.IsServerStarted)
            {
                info.owner.OnItemCountChangedEvent -= OnContainerItemCountChanged;
                info.owner.OnItemAddedEvent -= OnItemAdded;
                info.owner.OnItemRemovedEvent -= OnItemRemoved;
            }
        }

        #endregion

        #region Observe & Unobserve

        protected override void OnObserved(IContainer container, bool isDirty,
            NetworkConnection connection)
        {
            base.OnObserved(container, isDirty, connection);

            if (TryGetInfo(container.uuid, out var containerInfo))
            {
                if (containerInfo.observers.Count == 0)
                {
                    container.OpenOnServer();
                }
            }
            else
            {
                Debug.LogWarning(
                    $"不存在此{container.uuid}对应的{nameof(ContainerInfo)}");
            }
            
            if (isDirty)
            {
                ReconcileAllOnTarget(connection, container.uuid);
            }
        }

        protected override void OnUnobserved(IContainer container,
            NetworkConnection connection)
        {
            base.OnUnobserved(container, connection);

            if (TryGetInfo(container.uuid, out var containerInfo))
            {
                if (containerInfo.observers.Count <= 0)
                {
                    container.CloseOnServer();
                }
            }
            else
            {
                Debug.LogWarning(
                    $"不存在此{container.uuid}对应的{nameof(ContainerInfo)}");
            }
        }

        #endregion

        #region Container Changed

        private void Update()
        {
            if (IsServerStarted == false)
            {
                return;
            }

            var isHost = IsHostStarted;
            var hostClientID = isHost ? ClientManager.Connection.ClientId : -1;
            foreach (var containerInfo in GetAllOwnerInfos())
            {
                if (containerInfo.dirtySlots.Count == 0)
                {
                    continue;
                }

                if (containerInfo.observers.Count == 0)
                {
                    //containerInfo.dirtySlots.Clear();
                    continue;
                }

                if (containerInfo.observers.Count == 1 &&
                    containerInfo.observers.Contains(hostClientID))
                {
                    containerInfo.dirtySlots.Clear();
                    continue;
                }

                var ratio = containerInfo.dirtySlots.Count /
                            (float)containerInfo.owner.size;

                if (ratio > 0.6f)
                {
                    ReconcileAllItemsOnObservers(containerInfo);
                }
                else
                {
                    if (containerInfo.dirtySlots.Count == 1)
                    {
                        var slotIndex = containerInfo.dirtySlots.First();
                        ReconcileItemOnObservers(containerInfo, slotIndex);
                    }
                    else
                    {
                        ReconcileSomeItemsOnObservers(containerInfo,
                            containerInfo.dirtySlots);
                    }
                }

                SetDirty(containerInfo.owner.uuid);

                containerInfo.dirtySlots.Clear();
            }
        }

        private static void OnContainerItemCountChanged(IContainer container, 
            int slotIndex, IContainerItem item, int previous, int current)
        {
            if (current != previous)
            {
                SetSlotDirty(container.uuid, slotIndex);
            }
        }

        private static void OnItemRemoved(IContainer container, int slotIndex, 
            IContainerItem item)
        {
            SetSlotDirty(container.uuid, slotIndex);
        }

        private static void OnItemAdded(IContainer container, int slotIndex, 
            IContainerItem item)
        {
            SetSlotDirty(container.uuid, slotIndex);
        }

        #endregion

        #region Set Slot Dirty

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetSlotDirty(IContainer container, int slotIndex)
        {
            SetSlotDirty(container.uuid, slotIndex);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetSlotDirty(string containerUUID, int slotIndex)
        {
            if (TryGetInfo(containerUUID, out var info))
            {
                SetSlotDirty(info, slotIndex);
            }
            else
            {
                Debug.LogWarning($"试图设置一个不存在的{typeof(IContainer)}的slot为脏");
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetSlotDirty(IContainerItem item)
        {
            var container = item.sourceContainer;
            if (container.TryGetSlotIndex(item, out var slotIndex))
            {
                SetSlotDirty(container.uuid, slotIndex);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void SetSlotDirty(ContainerInfo containerInfo, int slotIndex)
        {
            containerInfo.dirtySlots.Add(slotIndex);
        }

        #endregion

        #region Set Dirty

        [ObserversRpc(ExcludeServer = true)]
        private void SetDirty(string containerUUID)
        {
            if (TryGetInfo(containerUUID, out var containerInfo))
            {
                if (containerInfo.owner.isOpen == false)
                {
                    containerInfo.owner.isDirty = true;
                }
            }
        }

        #endregion
    }
}

#endif