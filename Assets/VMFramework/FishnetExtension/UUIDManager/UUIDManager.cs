#if FISHNET
using System;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using VMFramework.Procedure;

namespace VMFramework.Network
{
    public abstract class UUIDManager<TInstance, TUUIDOwner> : NetworkManagerBehaviour<TInstance>
        where TInstance : UUIDManager<TInstance, TUUIDOwner>
        where TUUIDOwner : IUUIDOwner
    {
        [ShowInInspector]
        [ListDrawerSettings(DefaultExpandedState = false)]
        private static readonly HashSet<TUUIDOwner> allInfos = new();
        
        public static Action<TUUIDOwner> OnRegisterEvent;
        public static Action<TUUIDOwner> OnUnregisterEvent;

        protected override void OnBeforeInitStart()
        {
            base.OnBeforeInitStart();
            
            allInfos.Clear();
            
            UUIDCoreManager.OnUUIDOwnerRegistered += OnRegister;
            UUIDCoreManager.OnUUIDOwnerUnregistered += OnUnregister;
        }

        #region Utilities

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IReadOnlyCollection<TUUIDOwner> GetAllOwners() => allInfos;

        #endregion

        #region Register & Unregister

        private static void OnRegister(IUUIDOwner owner)
        {
            if (owner is TUUIDOwner typedOwner)
            {
                allInfos.Add(typedOwner);
                
                OnRegisterEvent?.Invoke(typedOwner);
            }
        }
        
        private static void OnUnregister(IUUIDOwner owner)
        {
            if (owner is TUUIDOwner typedOwner)
            {
                allInfos.Remove(typedOwner);
                
                OnUnregisterEvent?.Invoke(typedOwner);
            }
        }

        #endregion
    }
}

#endif