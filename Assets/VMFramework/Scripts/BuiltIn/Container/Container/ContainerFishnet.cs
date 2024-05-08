#if FISHNET
using System;
using FishNet.Connection;
using UnityEngine;
using VMFramework.Network;

namespace VMFramework.Containers
{
    public partial class Container : IUUIDOwner
    {
        public event Action<IUUIDOwner, bool, NetworkConnection> OnObservedEvent;
        public event Action<IUUIDOwner, NetworkConnection> OnUnobservedEvent;

        void IUUIDOwner.OnObserved(bool isDirty, NetworkConnection connection)
        {
            OnObservedEvent?.Invoke(this, isDirty, connection);
        }

        void IUUIDOwner.OnUnobserved(NetworkConnection connection)
        {
            OnUnobservedEvent?.Invoke(this, connection);
        }
        
        public void SetUUID(string uuid)
        {
            if (string.IsNullOrEmpty(uuid))
            {
                Debug.LogWarning("试图设置UUID为null或空字符串");
                return;
            }

            if (string.IsNullOrEmpty(this.uuid))
            {
                this.uuid = uuid;
                ContainerManager.Register(this);
            }
            else
            {
                Debug.LogWarning("试图修改已经生成的容器UUID");
            }
        }
    }
}
#endif