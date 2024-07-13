#if FISHNET

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using FishNet;
using VMFramework.Core;
using FishNet.Connection;
using FishNet.Object;
using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.GameLogicArchitecture;
using VMFramework.Procedure;

namespace VMFramework.Network
{
    [ManagerCreationProvider(ManagerType.NetworkCore)]
    public sealed partial class UUIDCoreManager : NetworkManagerBehaviour<UUIDCoreManager>
    {
        [ShowInInspector]
        [DictionaryDrawerSettings(DisplayMode = DictionaryDisplayOptions.ExpandedFoldout)]
        private static readonly Dictionary<Guid, UUIDInfo> uuidInfos = new();

        public static event Action<IUUIDOwner> OnUUIDOwnerRegistered;
        public static event Action<IUUIDOwner> OnUUIDOwnerUnregistered;

        public static event Action<IUUIDOwner, bool, NetworkConnection> OnUUIDOwnerObserved;
        public static event Action<IUUIDOwner, NetworkConnection> OnUUIDOwnerUnobserved;

        public override void OnStartServer()
        {
            base.OnStartServer();
            
            IGameItem.OnGameItemCreated += OnGameItemCreated;
            IGameItem.OnGameItemDestroyed += OnGameItemDestroyed;
        }

        public override void OnStopServer()
        {
            base.OnStopServer();
            
            IGameItem.OnGameItemCreated -= OnGameItemCreated;
            IGameItem.OnGameItemDestroyed -= OnGameItemDestroyed;
        }

        private void OnGameItemCreated(IGameItem gameItem)
        {
            if (gameItem is not IUUIDOwner owner)
            {
                return;
            }

            owner.TrySetUUIDAndRegister(Guid.NewGuid());
        }

        private void OnGameItemDestroyed(IGameItem gameItem)
        {
            if (gameItem is not IUUIDOwner owner)
            {
                return;
            }

            if (Unregister(owner) == false)
            {
                return;
            }

            owner.SetUUID(Guid.Empty);
        }

        #region Register & Unregister

        public static bool Register(IUUIDOwner owner)
        {
            if (owner == null)
            {
                Debug.LogWarning($"Failed to register a null {nameof(IUUIDOwner)}");
                return false;
            }
            
            var uuid = owner.uuid;
            
            if (uuid == Guid.Empty)
            {
                Debug.LogWarning($"Failed to register a {owner.GetType()} with an empty uuid");
                return false;
            }

            if (uuidInfos.TryAdd(uuid, new UUIDInfo(owner, instance.IsServerInitialized)) == false)
            {
                var oldOwner = uuidInfos[uuid].owner;
                
                Debug.LogWarning($"Registering a {owner.GetType()} with an existing uuid." +
                                 $"The old owner : {oldOwner} will be overridden.");
                
                Unregister(uuid);
                
                uuidInfos[uuid] = new UUIDInfo(owner, instance.IsServerInitialized);
            }
            
            OnUUIDOwnerRegistered?.Invoke(owner);

            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Unregister(IUUIDOwner owner)
        {
            if (owner == null)
            {
                Debug.LogWarning($"Failed to unregister a null {nameof(IUUIDOwner)}");
                return false;
            }

            if (Unregister(owner.uuid, out var existingOwner) == false)
            {
                return false;
            }

            if (owner != existingOwner)
            {
                Debug.LogWarning($"Failed to unregister. " +
                                 $"The owner {owner} does not match the existing owner {existingOwner}." +
                                 $"They have the same uuid but are not the same object.");
                return false;
            }
            
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Unregister(Guid uuid)
        {
            return Unregister(uuid, out _);
        }

        public static bool Unregister(Guid uuid, out IUUIDOwner owner)
        {
            if (uuid == Guid.Empty)
            {
                Debug.LogWarning($"Failed to unregister a {nameof(IUUIDOwner)} with an empty uuid");
                owner = null;
                return false;
            }

            if (uuidInfos.Remove(uuid, out var info) == false)
            {
                Debug.LogWarning($"Failed to unregister a {nameof(IUUIDOwner)} with uuid {uuid}. It does not exist.");
                owner = null;
                return false;
            }
            
            owner = info.owner;
            
            OnUUIDOwnerUnregistered?.Invoke(info.owner);

            return true;
        }

        #endregion

        #region Observe

        [ServerRpc(RequireOwnership = false)]
        private void _Observe(Guid uuid, bool isDirty, NetworkConnection connection = null)
        {
            if (TryGetInfoWithWarning(uuid, out var info))
            {
                ObserveInstantly(info, isDirty, connection);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void ObserveInstantly(UUIDInfo info, bool isDirty, NetworkConnection connection)
        {
            info.owner.OnObserved(isDirty, connection);
            
            info.observers.Add(connection.ClientId);
            
            OnUUIDOwnerObserved?.Invoke(info.owner, isDirty, connection);
        }

        public static void Observe(Guid uuid)
        {
            if (uuid == Guid.Empty)
            {
                Debug.LogWarning($"{nameof(uuid)} is empty");
                return;
            }

            if (instance.IsClientStarted == false)
            {
                Debug.LogWarning($"The client is not started yet, cannot observe {uuid}");
                return;
            }

            if (TryGetInfoWithWarning(uuid, out var info) == false)
            {
                return;
            }
            
            if (instance.IsHostStarted)
            {
                ObserveInstantly(info, info.owner.isDirty, InstanceFinder.ClientManager.Connection);
            }
            else
            {
                instance._Observe(uuid, info.owner.isDirty);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Observe(IUUIDOwner owner)
        {
            if (owner == null)
            {
                Debug.LogWarning($"Failed to observe a null {nameof(IUUIDOwner)}");
                return;
            }
            
            Observe(owner.uuid);
        }

        #endregion

        #region Unobserve

        [ServerRpc(RequireOwnership = false)]
        private void _Unobserve(Guid uuid, NetworkConnection connection = null)
        {
            if (TryGetInfoWithWarning(uuid, out var info))
            {
                UnobserveInstantly(info, connection);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void UnobserveInstantly(UUIDInfo info, NetworkConnection connection)
        {
            info.owner.OnUnobserved(connection);

            info.observers.Remove(connection.ClientId);
            
            OnUUIDOwnerUnobserved?.Invoke(info.owner, connection);
        }

        public static void Unobserve(Guid uuid)
        {
            if (uuid == Guid.Empty)
            {
                Debug.LogWarning($"{nameof(uuid)} is null or empty");
                return;
            }
            
            if (instance.IsClientStarted == false)
            {
                Debug.LogWarning($"The client is not started yet, cannot unobserve {uuid}");
                return;
            }

            if (TryGetInfoWithWarning(uuid, out var info) == false)
            {
                return;
            }
            
            if (instance.IsHostStarted)
            {
                UnobserveInstantly(info, InstanceFinder.ClientManager.Connection);
            }
            else
            {
                instance._Unobserve(uuid);
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Unobserve(IUUIDOwner owner)
        {
            if (owner == null)
            {
                Debug.LogWarning($"Failed to unobserve a null {nameof(IUUIDOwner)}");
                return;
            }
            
            Unobserve(owner.uuid);
        }

        #endregion
    }
}

#endif