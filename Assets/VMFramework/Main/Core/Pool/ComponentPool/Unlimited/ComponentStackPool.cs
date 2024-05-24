using System;
using System.Collections.Generic;
using UnityEngine;

namespace VMFramework.Core.Pool
{
    public sealed class ComponentStackPool<T> : ComponentReadOnlyCollectionPool<T, Stack<T>>
        where T : Component
    {
        public ComponentStackPool(Action<T> hideAction = null,
            Action<T> showAction = null, Action<T> destroyAction = null) : base(
            hideAction, showAction, destroyAction)
        {

        }

        public override T Get(Func<T> creator, out bool isFreshlyCreated)
        {
            if (pool.Count > 0)
            {
                var newOne = pool.Pop();
                Show(newOne);

                isFreshlyCreated = false;
                return newOne;
            }

            isFreshlyCreated = true;
            return creator();
        }

        public override void Return(T item)
        {
            item.AssertIsNotNull(nameof(item));
            
            Hide(item);
            pool.Push(item);
        }
    }
}