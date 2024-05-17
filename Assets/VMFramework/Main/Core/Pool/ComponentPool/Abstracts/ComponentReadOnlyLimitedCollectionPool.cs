using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Sirenix.OdinInspector;
using UnityEngine;

namespace VMFramework.Core.Pool
{
    public abstract class ComponentReadOnlyLimitedCollectionPool<T, TCollection> :
        ComponentLimitPool<T>, IComponentCheckableLimitPool<T>
        where T : Component
        where TCollection : IReadOnlyCollection<T>, new()
    {
        [ShowInInspector]
        protected TCollection pool = new();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Contains(T item)
        {
            return pool.Contains(item);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool IsEmpty()
        {
            return pool.Count == 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool IsFull()
        {
            return pool.Count >= maxCapacity;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override void Clear()
        {
            foreach (var component in pool)
            {
                Destroy(component);
            }

            pool = new TCollection();
        }

        protected ComponentReadOnlyLimitedCollectionPool(int maxCapacity,
            Action<T> hideAction = null, Action<T> showAction = null,
            Action<T> destroyAction = null) : base(maxCapacity, hideAction,
            showAction, destroyAction)
        {

        }
    }
}