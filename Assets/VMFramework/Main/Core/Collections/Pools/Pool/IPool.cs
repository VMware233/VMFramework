using System.Collections.Generic;

namespace VMFramework.Core.Pools
{
    public interface IPool<TItem> : IReadOnlyLimitedCollection<TItem>
    {
        /// <summary>
        /// Get an item from the pool, if there is no item in the pool, a new item will be created automatically,
        /// and the isFreshlyCreated variable will be returned to indicate whether it is a newly created item.
        /// </summary>
        /// <param name="isFreshlyCreated"></param>
        /// <returns></returns>
        public TItem Get(out bool isFreshlyCreated);

        /// <summary>
        /// Returns an item to the pool.
        /// </summary>
        /// <returns>true if the item was returned to the pool</returns>
        public bool Return(TItem item);

        /// <summary>
        /// Clear the pool, all items in the pool will be destroyed.
        /// </summary>
        public void Clear();
    }
}
