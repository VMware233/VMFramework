using System;
using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.Core;

namespace VMFramework.Procedure
{
    public class ManagerBehaviour<TInstance> : MonoBehaviour, IManagerBehaviour
        where TInstance : ManagerBehaviour<TInstance>
    {
        [ShowInInspector]
        [HideInEditorMode]
        protected static TInstance instance;

        protected virtual void OnBeforeInit()
        {
            instance = (TInstance)this;
            instance.AssertIsNotNull(nameof(instance));
        }

        void IInitializer.OnBeforeInit(Action onDone)
        {
            OnBeforeInit();
            onDone();
        }
    }
}
