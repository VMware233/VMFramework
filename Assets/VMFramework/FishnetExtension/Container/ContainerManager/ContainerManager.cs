#if FISHNET
using System;
using FishNet.Object;
using FishNet.Connection;
using UnityEngine;
using VMFramework.GameEvents;
using VMFramework.Network;
using VMFramework.Procedure;

namespace VMFramework.Containers
{
    [ManagerCreationProvider(ManagerType.NetworkCore)]
    public partial class ContainerManager : UUIDManager<ContainerManager, IContainer>
    {
        [SerializeField]
        protected bool isDebugging;

        protected override void OnBeforeInitStart()
        {
            base.OnBeforeInitStart();
            
            OnRegisterEvent += OnRegister;
            OnUnregisterEvent += OnUnregister;
        }

        #region Register & Unregister

        private static void OnRegister(IContainer container)
        {
            if (instance.IsServerStarted)
            {
                container.OnItemCountChangedEvent += OnContainerItemCountChanged;
                container.ItemAddedEvent.AddCallback(OnItemAdded, GameEventPriority.TINY);
                container.ItemRemovedEvent.AddCallback(OnItemRemoved, GameEventPriority.TINY);
                container.OnObservedEvent += OnObserved;
                container.OnUnobservedEvent += OnUnobserved;
            }
        }
        
        private static void OnUnregister(IContainer container)
        {
            if (instance.IsServerStarted)
            {
                container.OnItemCountChangedEvent -= OnContainerItemCountChanged;
                container.ItemAddedEvent.AddCallback(OnItemAdded, GameEventPriority.TINY);
                container.ItemRemovedEvent.AddCallback(OnItemRemoved, GameEventPriority.TINY);
                container.OnObservedEvent -= OnObserved;
                container.OnUnobservedEvent -= OnUnobserved;
                
                RemoveContainerDirtySlotsInfo(container);
            }
        }

        #endregion

        #region Observe & Unobserve

        private static void OnObserved(IUUIDOwner container, bool isDirty, NetworkConnection connection)
        {
            if (UUIDCoreManager.TryGetInfo(container.uuid, out var info))
            {
                if (info.observers.Count == 0)
                {
                    ((IContainer)container).OpenOnServer();
                }
            }
            else
            {
                Debug.LogWarning($"不存在此{container.uuid}对应的{nameof(UUIDInfo)}");
            }
            
            if (isDirty)
            {
                ReconcileAllOnTarget(connection, (IContainer)container);
            }
        }

        private static void OnUnobserved(IUUIDOwner container, NetworkConnection connection)
        {
            if (UUIDCoreManager.TryGetInfo(container.uuid, out var info))
            {
                if (info.observers.Count <= 0)
                {
                    ((IContainer)container).CloseOnServer();
                }
            }
            else
            {
                Debug.LogWarning(
                    $"不存在此{container.uuid}对应的{nameof(UUIDInfo)}");
            }
        }

        #endregion

        #region Set Dirty

        [ObserversRpc(ExcludeServer = true)]
        private void SetDirty(Guid containerUUID)
        {
            if (UUIDCoreManager.TryGetOwner(containerUUID, out IContainer container))
            {
                if (container.isOpen == false)
                {
                    container.isDirty = true;
                }
            }
        }

        #endregion
    }
}

#endif