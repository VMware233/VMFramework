using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace VMFramework.Core.Pool
{
    public class ComponentLimitedCollectionPool<TComponent, TCollection>
        : ComponentReadOnlyLimitedCollectionPool<TComponent, TCollection>
        where TComponent : Component
        where TCollection : IReadOnlyCollection<TComponent>, ICollection<TComponent>, new()
    {
        public ComponentLimitedCollectionPool(int maxCapacity, Action<TComponent> hideAction = null,
            Action<TComponent> showAction = null, Action<TComponent> destroyAction = null) : base(maxCapacity, hideAction,
            showAction, destroyAction)
        {
        }

        public override TComponent Get(Func<TComponent> creator, out bool isFreshlyCreated)
        {
            if (IsEmpty() == false)
            {
                var newOne = pool.First();
                pool.Remove(newOne);

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

            if (IsFull())
            {
                Destroy(item);
            }
            else
            {
                Hide(item);
                pool.Add(item);
            }
        }
    }
}