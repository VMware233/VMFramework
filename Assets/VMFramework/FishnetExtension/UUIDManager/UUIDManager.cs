#if FISHNET
using System;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using VMFramework.Core;
using Cysharp.Threading.Tasks;
using FishNet.Connection;
using UnityEngine;
using UnityEngine.Scripting;
using VMFramework.Containers;
using VMFramework.Procedure;

namespace VMFramework.Network
{
    public class UUIDManager<TInstance, TUUIDOwner> : 
        UUIDManager<TInstance, TUUIDOwner, UUIDManager<TInstance, TUUIDOwner>.OwnerInfo>
        where TInstance : UUIDManager<TInstance, TUUIDOwner>
        where TUUIDOwner : class, IUUIDOwner
    {
        [Preserve]
        public new class OwnerInfo : UUIDManager<TInstance, TUUIDOwner, OwnerInfo>.OwnerInfo
        {
            
        }
    }

    public class UUIDManager<TInstance, TUUIDOwner, TOwnerInfo> : NetworkManagerBehaviour<TInstance>
        where TInstance : UUIDManager<TInstance, TUUIDOwner, TOwnerInfo>
        where TUUIDOwner : IUUIDOwner
        where TOwnerInfo : UUIDManager<TInstance, TUUIDOwner, TOwnerInfo>.OwnerInfo, new()
    {
        public class OwnerInfo : IUUIDInfo
        {
            public TUUIDOwner owner;
            public HashSet<int> observers;
            public bool isObserver;

            #region IUUID Info

            IUUIDOwner IUUIDInfo.owner => owner;

            HashSet<int> IUUIDInfo.observers
            {
                get => observers;
                set => observers = value;
            }

            bool IUUIDInfo.isObserver
            {
                get => isObserver;
                set => isObserver = value;
            }

            #endregion
        }

        [ShowInInspector]
        private static HashSet<TOwnerInfo> allInfos = new();

        protected override void OnBeforeInit()
        {
            base.OnBeforeInit();

            allInfos.Clear();
        }

        #region Utilities

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetInfo(string uuid, out TOwnerInfo info)
        {
            if (uuid.IsNullOrEmpty())
            {
                Debug.LogWarning($"试图获取一个空uuid的{typeof(TUUIDOwner)}");
                info = null;
                return false;
            }

            if (UUIDCoreManager.TryGetInfo(uuid, out var uuidInfo))
            {
                info = uuidInfo as TOwnerInfo;
            }
            else
            {
                info = null;
            }

            return info != null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetOwner(string uuid, out TUUIDOwner owner)
        {
            if (TryGetInfo(uuid, out var info))
            {
                owner = info.owner;
                return true;
            }

            owner = default;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetOwner<TOwner>(string uuid, out TOwner owner) where TOwner : TUUIDOwner
        {
            if (TryGetInfo(uuid, out var info))
            {
                if (info.owner is TOwner typedOwner)
                {
                    owner = typedOwner;
                    return true;
                }
            }

            owner = default;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TUUIDOwner GetOwner(string uuid)
        {
            if (TryGetInfo(uuid, out var info))
            {
                return info.owner;
            }

            return default;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TOwner GetOwner<TOwner>(string uuid) where TOwner : TUUIDOwner
        {
            if (TryGetOwner<TOwner>(uuid, out var owner))
            {
                return owner;
            }

            return default;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async UniTask<TUUIDOwner> GetOwnerAsync(string uuid)
        {
            if (uuid.IsNullOrEmpty())
            {
                return default;
            }

            if (TryGetOwner(uuid, out var owner))
            {
                return owner;
            }

            await UniTask.WaitUntil(() => TryGetOwner(uuid, out owner));

            return owner;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected static IEnumerable<TOwnerInfo> GetAllOwnerInfos()
        {
            return allInfos;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<TUUIDOwner> GetAllOwners()
        {
            return GetAllOwnerInfos().Select(info => info.owner);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<NetworkConnection> GetAllObservers(string uuid)
        {
            return UUIDCoreManager.GetAllObservers(uuid);
        }

        #endregion

        #region Register & Unregister

        public static event Action<TOwnerInfo> OnRegisterEvent;
        public static event Action<TOwnerInfo> OnUnregisterEvent;

        public static void Register(TUUIDOwner owner)
        {
            if (owner is IContainer container)
            {
                Debug.LogError($"Is Registering {container}");
            }
            
            var uuid = owner.uuid;

            if (uuid.IsNullOrEmpty())
            {
                Debug.LogWarning($"试图注册一个空uuid的{typeof(TUUIDOwner)}");
                return;
            }

            TOwnerInfo newInfo;

            if (_instance.IsServerStarted)
            {
                newInfo = new TOwnerInfo()
                {
                    owner = owner,
                    observers = new()
                };

                newInfo.owner.OnObservedEvent += _OnObserved;
                newInfo.owner.OnUnobservedEvent += _OnUnobserved;
            }
            else
            {
                newInfo = new TOwnerInfo()
                {
                    owner = owner,
                    observers = null
                };
            }

            if (UUIDCoreManager.Register(newInfo))
            {
                allInfos.Add(newInfo);

                OnRegisterEvent?.Invoke(newInfo);
            }
        }

        public static void Unregister(TUUIDOwner owner)
        {
            if (owner == null)
            {
                Debug.LogWarning($"{typeof(TUUIDOwner)}为Null");
                return;
            }

            if (UUIDCoreManager.Unregister(owner.uuid, out var info))
            {
                var ownerInfo = info as TOwnerInfo;

                if (allInfos.Remove(ownerInfo))
                {
                    OnUnregisterEvent?.Invoke(ownerInfo);
                }
                else
                {
                    Debug.LogWarning(
                        $"试图移除一个不存在的uuid:{owner.uuid}的{typeof(TUUIDOwner)}");
                }
            }
        }

        #endregion

        #region Observe

        private static void _OnObserved(IUUIDOwner owner, bool isDirty,
            NetworkConnection connection)
        {
            _instance.OnObserved((TUUIDOwner)owner, isDirty, connection);
        }

        protected virtual void OnObserved(TUUIDOwner info, bool isDirty,
            NetworkConnection connection)
        {

        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Observe(TUUIDOwner owner)
        {
            UUIDCoreManager.Observe(owner?.uuid);
        }

        #endregion

        #region Unobserve

        private static void _OnUnobserved(IUUIDOwner owner,
            NetworkConnection connection)
        {
            _instance.OnUnobserved((TUUIDOwner)owner, connection);
        }

        protected virtual void OnUnobserved(TUUIDOwner owner,
            NetworkConnection connection)
        {

        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Unobserve(TUUIDOwner owner)
        {
            UUIDCoreManager.Unobserve(owner?.uuid);
        }

        #endregion
    }
}

#endif