using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.Core;

namespace VMFramework.Procedure
{
    /// <summary>
    /// 非线程安全的管理器基类，用于实现单例。
    /// Non-thread-safe manager base class used for singleton implementation.
    /// </summary>
    /// <typeparam name="TInstance"></typeparam>
    public class ManagerBehaviour<TInstance> : MonoBehaviour, IManagerBehaviour
        where TInstance : ManagerBehaviour<TInstance>
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
