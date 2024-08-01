using VMFramework.Core;
using VMFramework.Core.Pools;

namespace VMFramework.GameLogicArchitecture
{
    public delegate IGameItem CreateGameItemHandler(string name);
    
    /// <inheritdoc cref="PoolItemsPool{TPoolItem}"/>
    public sealed class GameItemPool : PoolItemsPool<IGameItem>
    {
        private readonly string id;
        private readonly CreateGameItemHandler onCreateFunc;
        
        public override int Capacity { get; }

        public GameItemPool(string id, CreateGameItemHandler onCreateFunc, int capacity)
        {
            id.AssertIsNotNullOrWhiteSpace(nameof(id));
            onCreateFunc.AssertIsNotNull(nameof(onCreateFunc));
            capacity.AssertIsAbove(0, nameof(capacity));
            
            this.id = id;
            this.onCreateFunc = onCreateFunc;
            Capacity = capacity;
        }

        protected override IGameItem CreateItem()
        {
            var item = onCreateFunc(id);
            item.OnCreate(id);
            return item;
        }
    }
}