#if FISHNET
using System;
using FishNet.Connection;
using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.Network;

namespace VMFramework.Containers
{
    public partial class Container
    {
        [ShowInInspector]
        public string uuid { get; private set; }
        
        public bool isDirty = true;
        
        public event Action<IUUIDOwner, bool, NetworkConnection> OnObservedEvent;
        public event Action<IUUIDOwner, NetworkConnection> OnUnobservedEvent;
        
        public event Action<IContainer> OnOpenOnServerEvent;
        public event Action<IContainer> OnCloseOnServerEvent;
        
        bool IUUIDOwner.isDirty
        {
            get => isDirty;
            set => isDirty = value;
        }

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

        #region Open & Close

        public void OpenOnServer()
        {
            if (isDebugging)
            {
                Debug.LogWarning($"{this}在服务器上打开");
            }

            OnOpenOnServerEvent?.Invoke(this);
        }

        public void CloseOnServer()
        {
            if (isDebugging)
            {
                Debug.LogWarning($"{this}在服务器上关闭");
            }

            OnCloseOnServerEvent?.Invoke(this);
        }

        #endregion
    }
}
#endif