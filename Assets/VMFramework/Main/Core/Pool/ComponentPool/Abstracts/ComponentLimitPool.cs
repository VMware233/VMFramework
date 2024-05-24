using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace VMFramework.Core.Pool
{
    public abstract partial class ComponentLimitPool<TComponent> : ComponentPool<TComponent>, 
        IComponentLimitPool<TComponent>
        where TComponent : Component
    {
        public int maxCapacity { get; private set; }

        protected ComponentLimitPool(int maxCapacity, Action<TComponent> hideAction = null,
            Action<TComponent> showAction = null, Action<TComponent> destroyAction = null) : base(
            hideAction, showAction, destroyAction)
        {
            this.maxCapacity = maxCapacity;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public abstract bool IsFull();
    }
}