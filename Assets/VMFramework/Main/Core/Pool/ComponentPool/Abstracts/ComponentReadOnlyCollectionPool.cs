using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace VMFramework.Core.Pool
{
    public abstract partial class ComponentReadOnlyCollectionPool<TComponent, TCollection> : 
        ComponentPool<TComponent>, IComponentCheckablePool<TComponent>
        where TComponent : Component
        where TCollection : IReadOnlyCollection<TComponent>, new()
    {
        protected TCollection pool = new();

        protected ComponentReadOnlyCollectionPool(Action<TComponent> hideAction = null,
            Action<TComponent> showAction = null, Action<TComponent> destroyAction = null) : base(
            hideAction, showAction, destroyAction)
        {

        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Contains(TComponent item)
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