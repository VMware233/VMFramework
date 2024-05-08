#if FISHNET

using FishNet.Object;
using Sirenix.OdinInspector;

namespace VMFramework.Network
{
    public class UniqueNetworkBehaviour<T> : NetworkBehaviour
        where T : UniqueNetworkBehaviour<T>
    {
        [ShowInInspector]
        [HideInEditorMode]
        public static T instance;

        protected virtual void Awake()
        {
            instance = (T)this;
        }
    }
}

#endif
