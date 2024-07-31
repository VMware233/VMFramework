using System;
using System.Collections.Generic;

namespace VMFramework.Core.Pools
{
    /// <summary>
    /// This pool is not thread-safe.
    /// </summary>
    /// <typeparam name="TPoolItem"></typeparam>
    public sealed class DefaultPoolItemsPool<TPoolItem> : Pool<TPoolItem> where TPoolItem : IDefaultPoolItem
    {
        private readonly Func<TPoolItem> onCreateFunc;
        private readonly Queue<TPoolItem> pool = new();
        
        public override int Count => pool.Count;
        public override int Capacity { get; }

        public DefaultPoolItemsPool(Func<TPoolItem> onCreateFunc, int capacity)
        {
            onCreateFunc.AssertIsNotNull(nameof(onCreateFunc));
            this.onCreateFunc = onCreateFunc;
            Capacity = capacity;
        }
        
        public override TPoolItem Get(out bool isFreshlyCreated)
        {
            if (pool.TryDequeue(out TPoolItem item))
            {
                isFreshlyCreated = false;
                item.OnGet();
                return item;
            }
            
            item = onCreateFunc();
            isFreshlyCreated = true;
            item.OnCreate();
            return item;
        }

        public override bool Return(TPoolItem item)
        {
            if (pool.Count >= Capacity)
            {
                item.OnClear();
                return false;
            }
            
            pool.Enqueue(item);
            item.OnReturn();
            return true;
        }

        public override void Clear()
        {
            foreach (var item in pool)
            {
                item.OnClear();
            }
            
            pool.Clear();
        }

        public override IEnumerator<TPoolItem> GetEnumerator() => pool.GetEnumerator();
    }
}