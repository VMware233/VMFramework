using System;
using UnityEngine;

namespace VMFramework.Core.Pool
{
    public class ComponentArrayLimitPool<T> : ComponentLimitPool<T>
        where T : Component
    {
        private readonly T[] pool;

        private int count;

        public ComponentArrayLimitPool(int maxCapacity, Action<T> hideAction = null,
            Action<T> showAction = null, Action<T> destroyAction = null) : base(
            maxCapacity, hideAction, showAction, destroyAction)
        {
            pool = new T[maxCapacity];
        }

        public override T Get(Func<T> creator, out bool isFreshlyCreated)
        {
            if (count > 0)
            {
                var newOne = pool[--count];
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
            
            if (count < maxCapacity)
            {
                Hide(item);
                pool[count++] = item;
            }
            else
            {
                Destroy(item);
            }
        }

        public override bool IsFull()
        {
            return count >= maxCapacity;
        }

        public override bool IsEmpty()
        {
            return count == 0;
        }

        public override void Clear()
        {
            for (int i = 0; i < count; i++)
            {
                Destroy(pool[i]);
            }

            count = 0;
        }
    }
}