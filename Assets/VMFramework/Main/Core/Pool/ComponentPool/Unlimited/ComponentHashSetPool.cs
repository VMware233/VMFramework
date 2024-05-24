using System;
using System.Collections.Generic;
using UnityEngine;

namespace VMFramework.Core.Pool
{
    public sealed class ComponentHashSetPool<T> : ComponentCollectionPool<T, HashSet<T>>
        where T : Component
    {
        public ComponentHashSetPool(Action<T> hideAction = null,
            Action<T> showAction = null, Action<T> destroyAction = null) : base(
            hideAction, showAction, destroyAction)
        {

        }
    }
}