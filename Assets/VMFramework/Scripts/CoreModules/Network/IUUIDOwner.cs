using System;

#if FISHNET
using FishNet.Connection;
#endif

namespace VMFramework.Network
{
    public interface IUUIDOwner
    {
        public string uuid { get; }


        public bool isDirty { get; set; }
        
#if FISHNET
        /// <summary>
        /// 当被观察时触发，仅在服务器上触发
        /// </summary>
        public event Action<IUUIDOwner, bool, NetworkConnection> OnObservedEvent;

        /// <summary>
        /// 当不再被观察时触发，仅在服务器上触发
        /// </summary>
        public event Action<IUUIDOwner, NetworkConnection> OnUnobservedEvent;

        public void OnObserved(bool isDirty, NetworkConnection connection);

        public void OnUnobserved(NetworkConnection connection);
#endif
    }
}