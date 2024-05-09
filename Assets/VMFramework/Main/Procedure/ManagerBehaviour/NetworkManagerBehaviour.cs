#if FISHNET
using System;
using FishNet.Object;
using Sirenix.OdinInspector;
using VMFramework.Core;

namespace VMFramework.Procedure
{
    public class NetworkManagerBehaviour<TInstance> : NetworkBehaviour, IManagerBehaviour
        where TInstance : NetworkManagerBehaviour<TInstance>
    {
        [ShowInInspector]
        [HideInEditorMode]
        protected static TInstance _instance;

        public static TInstance instance => _instance;

        protected virtual void OnPreInit()
        {
            _instance = (TInstance)this;
            _instance.AssertIsNotNull(nameof(_instance));
        }

        public void OnPreInit(Action onDone)
        {
            OnPreInit();
            onDone();
        }
    }
}
#endif
