using System;
using System.Collections.Generic;
using UnityEngine;

namespace VMFramework.Core.Pool
{
    public class ComponentHashSetLimitPool<T> : ComponentLimitedCollectionPool<T, HashSet<T>>
        where T : Component
    {
        public ComponentHashSetLimitPool(int maxCapacity,
            Action<T> hideAction = null, Action<T> showAction = null,
            Action<T> destroyAction = null) : base(maxCapacity, hideAction,
            showAction, destroyAction)
        {
        }
    }
}