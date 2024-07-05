#if FISHNET

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using FishNet;
using VMFramework.Core;
using FishNet.Connection;
using FishNet.Object;
using UnityEngine;
using VMFramework.GameLogicArchitecture;
using VMFramework.Procedure;

namespace VMFramework.Network
{
    [ManagerCreationProvider(ManagerType.NetworkCore)]
    public sealed partial class UUIDCoreManager : NetworkManagerBehaviour<UUIDCoreManager>
    {
        private static readonly Dictionary<string, UUIDInfo> uuidInfos = new();

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
            
            string uuid = Guid.NewGuid().ToString();

            owner.TrySetUUIDAndRegister(uuid);
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

            if (owner.SetUUID(null) == false)
            {
                Debug.LogWarning($"设置{owner.GetType()}的uuid失败，{owner}的uuid已经被清空");
            }
        }

        #region Register & Unregister

        public static bool Register(IUUIDOwner owner)
        {
            if (owner == null)
            {
                Debug.LogError($"试图注册一个空的{nameof(IUUIDOwner)}");
                return false;
            }
            
            var uuid = owner.uuid;
            
            if (uuid.IsNullOrEmpty())
            {
                Debug.LogWarning($"试图注册一个空uuid的{owner.GetType()}");
                return false;
            }
            
            if (uuidInfos.ContainsKey(uuid))
            {
                Debug.LogWarning($"重复注册uuid，旧的{owner.GetType()}将被覆盖");
            }

            uuidInfos[uuid] = new UUIDInfo(owner, instance.IsServerInitialized);
            
            OnUUIDOwnerRegistered?.Invoke(owner);

            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Unregister(IUUIDOwner owner)
        {
            if (owner == null)
            {
                Debug.LogError($"试图取消注册一个空的{nameof(IUUIDOwner)}");
                return false;
            }

            if (Unregister(owner.uuid, out var existingOwner) == false)
            {
                return false;
            }

            if (owner != existingOwner)
            {
                Debug.LogWarning($"取消注册uuid失败，{owner}与{existingOwner}的UUID一样，但不匹配");
                return false;
            }
            
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Unregister(string uuid)
        {
            return Unregister(uuid, out _);
        }

        public static bool Unregister(string uuid, out IUUIDOwner owner)
        {
            if (uuid.IsNullOrEmpty())
            {
                Debug.LogWarning($"试图取消注册一个空的uuid");
                owner = null;
                return false;
            }

            if (uuidInfos.Remove(uuid, out var info) == false)
            {
                Debug.LogWarning($"试图移除一个不存在的uuid:{uuid}");
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
        private void _Observe(string uuid, bool isDirty, NetworkConnection connection = null)
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

        public static void Observe(string uuid)
        {
            if (uuid.IsNullOrEmpty())
            {
                Debug.LogWarning("uuid is null or empty");
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
            Observe(owner?.uuid);
        }

        #endregion

        #region Unobserve

        [ServerRpc(RequireOwnership = false)]
        private void _Unobserve(string uuid, NetworkConnection connection = null)
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

        public static void Unobserve(string uuid)
        {
            if (uuid.IsNullOrEmpty())
            {
                Debug.LogWarning("uuid is null or empty");
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
            Unobserve(owner?.uuid);
        }

        #endregion
    }
}

#endif