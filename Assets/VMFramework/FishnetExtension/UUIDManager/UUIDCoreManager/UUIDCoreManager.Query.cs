#if FISHNET
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using FishNet.Connection;
using UnityEngine;
using VMFramework.Core;

namespace VMFramework.Network
{
    public partial class UUIDCoreManager
    {
        #region Try Get Info

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetInfo(string uuid, out UUIDInfo info)
        {
            if (uuid.IsNullOrEmpty())
            {
                Debug.LogWarning($"Try to get {nameof(UUIDInfo)} with empty uuid");
                info = default;
                return false;
            }

            return uuidInfos.TryGetValue(uuid, out info);
        }

        #endregion

        #region Try Get Owner

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetOwner(string uuid, out IUUIDOwner owner)
        {
            if (uuid.IsNullOrEmpty())
            {
                Debug.LogWarning($"Try to get {typeof(IUUIDOwner)} with empty uuid");
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
        public static bool TryGetOwner<TUUIDOwner>(string uuid, out TUUIDOwner owner)
            where TUUIDOwner : IUUIDOwner
        {
            if (uuid.IsNullOrEmpty())
            {
                Debug.LogWarning($"Try to get {typeof(TUUIDOwner)} with empty uuid");
                owner = default;
                return false;
            }

            if (uuidInfos.TryGetValue(uuid, out var info))
            {
                if (info.owner is TUUIDOwner tOwner)
                {
                    owner = tOwner;
                    return true;
                }
            }

            owner = default;
            return false;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetOwnerWithWarning<TUUIDOwner>(string uuid, out TUUIDOwner owner)
            where TUUIDOwner : IUUIDOwner
        {
            if (TryGetOwner(uuid, out owner) == false)
            {
                Debug.LogWarning($"The {typeof(TUUIDOwner)} with uuid {uuid} does not exist");
                return false;
            }
            
            return true;
        }

        #endregion

        #region Has UUID

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasUUID(string uuid)
        {
            if (uuid.IsNullOrEmpty())
            {
                Debug.LogWarning($"Try to check if uuid exists with empty uuid");
                return false;
            }
            
            return uuidInfos.ContainsKey(uuid);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasUUIDWithWarning(string uuid)
        {
            if (uuid.IsNullOrEmpty())
            {
                Debug.LogWarning($"Try to check if uuid exists with empty uuid");
                return false;
            }
            
            if (uuidInfos.ContainsKey(uuid) == false)
            {
                Debug.LogWarning($"The uuid {uuid} does not exist");
                return false;
            }
            
            return true;
        }

        #endregion

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IReadOnlyCollection<UUIDInfo> GetAllOwnerInfos()
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
                return info.observers.Select(id => _instance.ServerManager.Clients[id]);
            }

            return null;
        }
    }
}
#endif