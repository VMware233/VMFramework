using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Sirenix.OdinInspector;
using UnityEngine;

namespace VMFramework.Core.Pool
{
    public abstract class ComponentReadOnlyCollectionPool<T, TCollection> : 
        ComponentPool<T>, IComponentCheckablePool<T>
        where T : Component
        where TCollection : IReadOnlyCollection<T>, new()
    {
        [ShowInInspector]
        protected TCollection pool = new();

        protected ComponentReadOnlyCollectionPool(Action<T> hideAction = null,
            Action<T> showAction = null, Action<T> destroyAction = null) : base(
            hideAction, showAction, destroyAction)
        {

        }

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
        public override void Clear()
        {
            foreach (var component in pool)
            {
                Destroy(component);
            }

            pool = new TCollection();
        }
    }
}