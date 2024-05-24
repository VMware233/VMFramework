using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using Object = UnityEngine.Object;

namespace VMFramework.Core.Pool
{
    public abstract class ComponentPool<TComponent> : IComponentPool<TComponent> where TComponent : Component
    {
        private readonly Action<TComponent> hideAction;
        private readonly Action<TComponent> showAction;
        private readonly Action<TComponent> destroyAction;

        protected ComponentPool(Action<TComponent> hideAction = null,
            Action<TComponent> showAction = null, Action<TComponent> destroyAction = null)
        {
            this.hideAction = hideAction;
            this.showAction = showAction;
            this.destroyAction = destroyAction;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public abstract TComponent Get(Func<TComponent> creator, out bool isFreshlyCreated);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public abstract void Return(TComponent item);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public abstract bool IsEmpty();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public abstract void Clear();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected void Show(TComponent item)
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
        protected void Hide(TComponent item)
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
        protected void Destroy(TComponent item)
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
