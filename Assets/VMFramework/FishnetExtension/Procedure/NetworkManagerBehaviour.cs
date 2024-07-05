#if FISHNET
using System;
using System.Collections.Generic;
using FishNet.Object;
using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.Core;

namespace VMFramework.Procedure
{
    [RequireComponent(typeof(NetworkObject))]
    public class NetworkManagerBehaviour<TInstance> : NetworkBehaviour, IManagerBehaviour
        where TInstance : NetworkManagerBehaviour<TInstance>
    {
        [ShowInInspector]
        [HideInEditorMode]
        protected static TInstance instance { get; private set; }
        
        void IManagerBehaviour.SetInstance()
        {
            if (instance != null)
            {
                Debug.LogError($"Instance of {typeof(TInstance)} already exists!");
                return;
            }
            
            instance = (TInstance)this;
            instance.AssertIsNotNull(nameof(instance));
        }

        protected virtual void OnBeforeInitStart()
        {
            
        }
        
        protected virtual IEnumerable<InitializationAction> GetInitializationActions()
        {
            yield return new(InitializationOrder.BeforeInitStart, OnBeforeInitStartInternal, this);
        }

        IEnumerable<InitializationAction> IInitializer.GetInitializationActions()
        {
            return GetInitializationActions();
        }

        private void OnBeforeInitStartInternal(Action onDone)
        {
            OnBeforeInitStart();
            onDone();
        }
    }
}
#endif
