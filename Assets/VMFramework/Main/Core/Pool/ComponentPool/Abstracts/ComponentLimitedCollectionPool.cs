using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace VMFramework.Core.Pool
{
    public class ComponentLimitedCollectionPool<T, TCollection>
        : ComponentReadOnlyLimitedCollectionPool<T, TCollection>
        where T : Component
        where TCollection : IReadOnlyCollection<T>, ICollection<T>, new()
    {
        public ComponentLimitedCollectionPool(int maxCapacity, Action<T> hideAction = null,
            Action<T> showAction = null, Action<T> destroyAction = null) : base(maxCapacity, hideAction,
            showAction, destroyAction)
        {
        }

        public override T Get(Func<T> creator, out bool isFreshlyCreated)
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
                pool.Add(item);
            }
        }
    }
}