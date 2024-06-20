#if UNITY_EDITOR
using System;

namespace VMFramework.GameEvents
{
    public partial class ColliderMouseEventTrigger
    {
        private void Reset()
        {
            if (owner == null)
            {
                owner = transform;
            }
        }
    }
}
#endif