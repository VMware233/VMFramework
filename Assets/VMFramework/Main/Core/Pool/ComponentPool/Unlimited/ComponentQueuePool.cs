using System;
using System.Collections.Generic;
using UnityEngine;

namespace VMFramework.Core.Pool
{
    public class ComponentQueuePool<T> : ComponentReadOnlyCollectionPool<T, Queue<T>>
        where T : Component
    {
        public ComponentQueuePool(Action<T> hideAction = null,
            Action<T> showAction = null, Action<T> destroyAction = null) : base(
            hideAction, showAction, destroyAction)
        {

        }

        public override T Get(Func<T> creator, out bool isFreshlyCreated)
        {
            if (pool.Count > 0)
            {
                var newOne = pool.Dequeue();
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
            pool.Enqueue(item);
        }
    }
}