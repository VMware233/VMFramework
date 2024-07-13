#if FISHNET
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace VMFramework.Network
{
    public static class UUIDOwnerUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool SetUUIDWithWarning(this IUUIDOwner owner, Guid uuid)
        {
            if (owner == null)
            {
                Debug.LogWarning("Owner is null");
                return false;
            }
            
            if (owner.SetUUID(uuid) == false)
            {
                Debug.LogWarning($"Failed to set UUID: {uuid} for {owner.GetType()}");
                return false;
            }
            
            return true;
        }
        
        /// <summary>
        /// Sets the UUID of the owner and registers it with the <see cref="UUIDCoreManager"/>.
        /// Often used in OnRead function of a <see cref="GameLogicArchitecture.IGameItem"/>
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="uuid"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TrySetUUIDAndRegister(this IUUIDOwner owner, Guid uuid)
        {
            if (owner.SetUUIDWithWarning(uuid) == false)
            {
                return false;
            }

            UUIDCoreManager.Register(owner);
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<int> GetObservers(this IUUIDOwner owner)
        {
            if (UUIDCoreManager.TryGetInfo(owner, out var info))
            {
                return info.observers;
            }
            
            return Enumerable.Empty<int>();
        }
    }
}
#endif