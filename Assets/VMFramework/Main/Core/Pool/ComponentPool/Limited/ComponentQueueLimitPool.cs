using System;
using System.Collections.Generic;
using UnityEngine;

namespace VMFramework.Core.Pool
{
    public sealed class ComponentQueueLimitPool<T> : ComponentReadOnlyLimitedCollectionPool<T, Queue<T>> 
        where T : Component
    {
        public ComponentQueueLimitPool(int maxCapacity, Action<T> hideAction = null,
            Action<T> showAction = null, Action<T> destroyAction = null) : base(
            maxCapacity, hideAction, showAction, destroyAction)
        {

        }

        public override T Get(Func<T> creator, out bool isFreshlyCreated)
        {
            if (IsEmpty() == false)
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
            
            if (IsFull())
            {
                Destroy(item);
            }
            else
            {
                Hide(item);
                pool.Enqueue(item);
            }
        }
    }
}