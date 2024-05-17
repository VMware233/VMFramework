using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace VMFramework.Core.Pool
{
    #region Interface

    public interface IReferencePoolItem
    {
        public bool isInPool { get; }

        public void OnGet();

        public void OnCreate();

        public void OnReturn();

        public void OnClear();
    }

    #endregion

    public class ReferenceStackPool<T> : ICheckablePool<T> where T : IReferencePoolItem
    {
        [ShowInInspector]
        protected Stack<T> pool = new();

        public T Get(Func<T> creator, out bool isFreshlyCreated)
        {
            if (pool.Count > 0)
            {
                var item = pool.Pop();
                item.OnGet();

                isFreshlyCreated = false;
                return item;
            }
            else
            {
                var item = creator();
                item.OnCreate();

                isFreshlyCreated = true;
                return item;
            }
        }

        public void Return(T item)
        {
            item.OnReturn();
            pool.Push(item);
        }

        public bool IsEmpty()
        {
            return pool.Count == 0;
        }

        public void Clear()
        {
            foreach (var item in pool)
            {
                item.OnClear();
            }

            pool.Clear();
        }

        public bool Contains(T item)
        {
            return pool.Contains(item);
        }
    }
}
