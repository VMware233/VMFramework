#if FISHNET
using System;
using System.Collections.Generic;
using FishNet.Connection;
using FishNet.Serializing;
using VMFramework.GameLogicArchitecture;

namespace VMFramework.Network
{
    public abstract class UUIDGameItem : GameItem, IUUIDOwner
    {
        public Guid uuid { get; private set; }

        Guid IUUIDOwner.uuid
        {
            get => uuid;
            set => uuid = value;
        }

        bool IUUIDOwner.isDirty
        {
            get => false;
            set { }
        }

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

        protected override void OnWrite(Writer writer)
        {
            base.OnWrite(writer);

            writer.WriteGuidAllocated(uuid);
        }

        protected override void OnRead(Reader reader)
        {
            base.OnRead(reader);

            uuid = reader.ReadGuid();
        }

        protected override void OnGetStringProperties(
            ICollection<(string propertyID, string propertyContent)> collection)
        {
            base.OnGetStringProperties(collection);

            collection.Add(("UUID", uuid.ToString()));
        }
    }
}
#endif