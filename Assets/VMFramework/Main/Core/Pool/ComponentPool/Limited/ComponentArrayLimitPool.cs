using System;
using UnityEngine;

namespace VMFramework.Core.Pool
{
    public sealed partial class ComponentArrayLimitPool<TComponent> : ComponentLimitPool<TComponent>
        where TComponent : Component
    {
        private readonly TComponent[] pool;

        private int count;

        public ComponentArrayLimitPool(int maxCapacity, Action<TComponent> hideAction = null,
            Action<TComponent> showAction = null, Action<TComponent> destroyAction = null) : base(
            maxCapacity, hideAction, showAction, destroyAction)
        {
            pool = new TComponent[maxCapacity];
        }

        public override TComponent Get(Func<TComponent> creator, out bool isFreshlyCreated)
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

        public override void Return(TComponent item)
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