#if FISHNET
using System;
using FishNet.Connection;
using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.Core;
using VMFramework.Network;

namespace VMFramework.Containers
{
    public partial class Container
    {
        [ShowInInspector]
        public Guid uuid { get; private set; }
        
        public bool isDirty = true;

        public event Action<IUUIDOwner, bool, NetworkConnection> OnObservedEvent;
        public event Action<IUUIDOwner, NetworkConnection> OnUnobservedEvent;
        
        public event Action<IContainer> OnOpenOnServerEvent;
        public event Action<IContainer> OnCloseOnServerEvent;

        Guid IUUIDOwner.uuid
        {
            get => uuid;
            set => uuid = value;
        }

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

        #region Open & Close

        public void OpenOnServer()
        {
            if (isDebugging)
            {
                Debugger.LogWarning($"{this} opened on server");
            }

            OnOpenOnServerEvent?.Invoke(this);
        }

        public void CloseOnServer()
        {
            if (isDebugging)
            {
                Debugger.LogWarning($"{this} closed on server");
            }

            OnCloseOnServerEvent?.Invoke(this);
        }

        #endregion
    }
}
#endif