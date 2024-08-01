using System;
using VMFramework.Core.Pools;

namespace VMFramework.Maps
{
    /// <inheritdoc cref="PoolItemsPool{TPoolItem}"/>
    public sealed class GridChunkPool : PoolItemsPool<IGridChunk>
    {
        private readonly IGridMap gridMap;
        private readonly Func<IGridChunk> onCreateFunc;
        
        public override int Capacity { get; }

        public GridChunkPool(IGridMap gridMap, Func<IGridChunk> onCreateFunc, int capacity)
        {
            this.gridMap = gridMap;
            this.onCreateFunc = onCreateFunc;
            Capacity = capacity;
        }

        protected override IGridChunk CreateItem()
        {
            var item = onCreateFunc();
            item.OnCreate(gridMap);
            return item;
        }
    }
}