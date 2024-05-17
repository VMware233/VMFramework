using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using Object = UnityEngine.Object;

namespace VMFramework.Core.Pool
{
    public abstract class ComponentPool<T> : IComponentPool<T> where T : Component
    {
        private readonly Action<T> hideAction;
        private readonly Action<T> showAction;
        private readonly Action<T> destroyAction;

        protected ComponentPool(Action<T> hideAction = null,
            Action<T> showAction = null, Action<T> destroyAction = null)
        {
            this.hideAction = hideAction;
            this.showAction = showAction;
            this.destroyAction = destroyAction;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public abstract T Get(Func<T> creator, out bool isFreshlyCreated);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public abstract void Return(T item);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public abstract bool IsEmpty();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public abstract void Clear();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected void Show(T item)
        {
            if (showAction != null)
            {
                showAction(item);
            }
            else
            {
                item.SetActive(true);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected void Hide(T item)
        {
            if (hideAction != null)
            {
                hideAction(item);
            }
            else
            {
                item.SetActive(false);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected void Destroy(T item)
        {
            if (destroyAction != null)
            {
                destroyAction(item);
            }
            else
            {
                Object.Destroy(item.gameObject);
            }
        }
    }
}
