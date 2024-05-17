using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace VMFramework.Core.Pool
{
    public class ComponentCollectionPool<T, TCollection> : 
        ComponentReadOnlyCollectionPool<T, TCollection>
        where T : Component
        where TCollection : IReadOnlyCollection<T>, ICollection<T>, new()
    {
        public ComponentCollectionPool(Action<T> hideAction = null,
            Action<T> showAction = null, Action<T> destroyAction = null) : base(
            hideAction, showAction, destroyAction)
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
            
            Hide(item);
            pool.Add(item);
        }
    }
}