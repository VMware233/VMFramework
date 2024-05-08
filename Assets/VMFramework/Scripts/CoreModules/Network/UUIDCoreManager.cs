#if FISHNET

using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using VMFramework.Core;
using FishNet;
using FishNet.Connection;
using FishNet.Object;
using UnityEngine;
using UnityEngine.Scripting;
using VMFramework.Procedure;

namespace VMFramework.Network
{
    public interface IUUIDInfo
    {
        public IUUIDOwner owner { get; }

        public HashSet<int> observers { get; set; }

        public bool isObserver { get; set; }
    }

    public static class UUIDInfoUtility
    {
        public static IEnumerable<NetworkConnection> GetObserverConnections(
            this IUUIDInfo info)
        {
            var clients = InstanceFinder.ServerManager.Clients;

            foreach (var observer in info.observers)
            {
                if (clients.TryGetValue(observer, out var connection))
                {
                    if (connection.IsHost)
                    {
                        continue;
                    }

                    yield return connection;
                }
            }
        }
    }

    [ManagerCreationProvider(ManagerType.NetworkCore)]
    public class UUIDCoreManager : NetworkManagerBehaviour<UUIDCoreManager>
    {
        private static Dictionary<string, IUUIDInfo> uuidInfos = new();

        #region Utilities

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetInfo(string uuid, out IUUIDInfo info)
        {
            if (uuid.IsNullOrEmpty())
            {
                Debug.LogWarning($"试图获取一个空uuid的{typeof(IUUIDInfo)}");
                info = null;
                return false;
            }

            return uuidInfos.TryGetValue(uuid, out info);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetOwner(string uuid, out IUUIDOwner owner)
        {
            if (uuid.IsNullOrEmpty())
            {
                Debug.LogWarning($"试图获取一个空uuid的{typeof(IUUIDOwner)}");
                owner = null;
                return false;
            }

            if (uuidInfos.TryGetValue(uuid, out var info))
            {
                owner = info.owner;
                return true;
            }

            owner = null;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IReadOnlyCollection<IUUIDInfo> GetAllOwnerInfos()
        {
            return uuidInfos.Values;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IUUIDOwner> GetAllOwners()
        {
            return GetAllOwnerInfos().Select(info => info.owner);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<NetworkConnection> GetAllObservers(string uuid)
        {
            if (TryGetInfo(uuid, out var info))
            {
                return info.observers.Select(
                    id => _instance.ServerManager.Clients[id]);
            }

            return null;
        }

        #endregion

        #region Register & Unregister

        public static bool Register(IUUIDInfo info)
        {
            if (info.owner == null)
            {
                Debug.LogWarning($"{info.GetType()}的{nameof(info.owner)}为Null");
                return false;
            }

            var uuid = info.owner.uuid;

            if (uuid.IsNullOrEmpty())
            {
                Debug.LogWarning($"试图注册一个空uuid的{typeof(IUUIDOwner)}");
                return false;
            }

            if (uuidInfos.TryGetValue(uuid, out var oldInfo))
            {
                Debug.LogWarning($"重复注册uuid，旧的{oldInfo}将被覆盖");
            }

            uuidInfos[uuid] = info;

            return true;
        }

        public static bool Unregister(string uuid, out IUUIDInfo info)
        {
            if (uuid.IsNullOrEmpty())
            {
                Debug.LogWarning($"试图取消注册一个空的uuid");
                info = null;
                return false;
            }

            if (uuidInfos.Remove(uuid, out info) == false)
            {
                Debug.LogWarning($"试图移除一个不存在的uuid:{uuid}");
                return false;
            }

            return true;
        }

        #endregion

        #region Observe

        [ServerRpc(RequireOwnership = false)]
        [Preserve]
        private void _Observe(string uuid, bool isDirty,
            NetworkConnection connection = null)
        {
            if (TryGetInfo(uuid, out var info))
            {
                info.owner.OnObserved(isDirty, connection);

                info.observers ??= new();

                info.observers.Add(connection.ClientId);
            }
            else
            {
                Debug.LogWarning(
                    $"不存在此{nameof(uuid)}:{uuid}对应的{typeof(IUUIDInfo)}");
            }
        }

        public static void Observe(string uuid)
        {
            //if (instance.IsServer)
            //{
            //    return;
            //}

            if (uuid.IsNullOrEmpty())
            {
                Debug.LogWarning("uuid为Null或空");
                return;
            }

            if (TryGetInfo(uuid, out var info))
            {
                info.isObserver = true;
                _instance._Observe(uuid, info.owner.isDirty);
            }
            else
            {
                Debug.LogWarning(
                    $"不存在此{nameof(uuid)}:{uuid}对应的{typeof(IUUIDOwner)}");
            }
        }

        #endregion

        #region Unobserve

        [ServerRpc(RequireOwnership = false)]
        [Preserve]
        private void _Unobserve(string uuid, NetworkConnection connection = null)
        {
            if (TryGetInfo(uuid, out var info))
            {
                info.owner.OnUnobserved(connection);

                info.observers.Remove(connection.ClientId);
            }
            else
            {
                Debug.LogWarning(
                    $"不存在此{nameof(uuid)}:{uuid}对应的{typeof(IUUIDInfo)}");
            }
        }

        public static void Unobserve(string uuid)
        {
            //if (instance.IsServer)
            //{
            //    return;
            //}

            if (uuid.IsNullOrEmpty())
            {
                Debug.LogWarning("uuid为Null或空");
                return;
            }

            if (TryGetInfo(uuid, out var info))
            {
                info.isObserver = false;
                _instance._Unobserve(uuid);
            }
            else
            {
                Debug.LogWarning(
                    $"不存在此{nameof(uuid)}:{uuid}对应的{typeof(IUUIDOwner)}");
            }
        }

        #endregion
    }
}

#endif
