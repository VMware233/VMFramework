using System;
using System.Runtime.CompilerServices;
using Sirenix.OdinInspector;
using UnityEngine;

namespace VMFramework.Core.Pool
{
    public abstract class ComponentLimitPool<T> : ComponentPool<T>, IComponentLimitPool<T>
        where T : Component
    {
        [ShowInInspector]
        public int maxCapacity { get; private set; }

        protected ComponentLimitPool(int maxCapacity, Action<T> hideAction = null,
            Action<T> showAction = null, Action<T> destroyAction = null) : base(
            hideAction, showAction, destroyAction)
        {
            this.maxCapacity = maxCapacity;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public abstract bool IsFull();
    }
}