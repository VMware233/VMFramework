#if FISHNET
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace VMFramework.Network
{
    public struct UUIDInfo
    {
        [ShowInInspector]
        public IUUIDOwner owner { get; }

        [ShowInInspector]
        public HashSet<int> observers { get; }

        public UUIDInfo(IUUIDOwner owner, bool asServer)
        {
            this.owner = owner;
            if (asServer)
            {
                observers = new();
            }
            else
            {
                observers = null;
            }
        }
    }
}
#endif