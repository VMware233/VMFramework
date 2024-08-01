using System;
using System.Collections.Generic;

namespace VMFramework.Core.Pools
{
    /// <inheritdoc cref="PoolItemsPool{TPoolItem}"/>
    public sealed class DefaultPoolItemsPool<TPoolItem> : PoolItemsPool<TPoolItem> where TPoolItem : IDefaultPoolItem
    {
        private readonly Func<TPoolItem> onCreateFunc;
        
        public override int Capacity { get; }

        public DefaultPoolItemsPool(Func<TPoolItem> onCreateFunc, int capacity)
        {
            onCreateFunc.AssertIsNotNull(nameof(onCreateFunc));
            this.onCreateFunc = onCreateFunc;
            Capacity = capacity;
        }

        protected override TPoolItem CreateItem()
        {
            var item = onCreateFunc();
            item.OnCreate();
            return item;
        }
    }
}