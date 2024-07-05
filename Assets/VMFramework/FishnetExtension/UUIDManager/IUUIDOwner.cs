#if FISHNET
using System;
using FishNet.Connection;
using UnityEngine;
using VMFramework.Core;

namespace VMFramework.Network
{
    public interface IUUIDOwner
    {
        public string uuid { get; protected set; }

        public bool isDirty { get; set; }
        
        /// <summary>
        /// Triggered when the owner is observed. Only triggered on the server.
        /// </summary>
        public event Action<IUUIDOwner, bool, NetworkConnection> OnObservedEvent;

        /// <summary>
        /// Triggered when the owner is unobserved. Only triggered on the server.
        /// </summary>
        public event Action<IUUIDOwner, NetworkConnection> OnUnobservedEvent;

        public void OnObserved(bool isDirty, NetworkConnection connection);

        public void OnUnobserved(NetworkConnection connection);

        /// <summary>
        /// Changes the uuid of the owner.
        /// </summary>
        /// <param name="uuid"></param>
        /// <returns></returns>
        public bool SetUUID(string uuid)
        {
            if (this.uuid.IsNullOrEmpty())
            {
                if (uuid.IsNullOrEmpty())
                {
                    Debug.LogWarning($"The uuid of {this} has already been set to null or empty." +
                                     "Cannot set it to null or empty again.");
                    return false;
                }

                this.uuid = uuid;
                return true;
            }

            if (uuid.IsNullOrEmpty())
            {
                this.uuid = null;
                return true;
            }
                
            Debug.LogWarning($"The uuid of {this} has already been set to {this.uuid} and cannot be changed." +
                             "If you want to change the uuid, please set the uuid to null first.");
            return false;
        }
    }
}
#endif