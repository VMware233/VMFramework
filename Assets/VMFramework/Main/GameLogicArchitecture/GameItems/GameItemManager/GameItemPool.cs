using System;
using System.Collections.Generic;
using VMFramework.Core;
using VMFramework.Core.Pools;

namespace VMFramework.GameLogicArchitecture
{
    public delegate IGameItem CreateGameItemHandler(string name);
    
    public sealed class GameItemPool : Pool<IGameItem>
    {
        private readonly string id;
        private readonly CreateGameItemHandler onCreateFunc;
        private readonly Queue<IGameItem> pool = new();
        
        public override int Count => pool.Count;
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
        
        public override IGameItem Get(out bool isFreshlyCreated)
        {
            if (pool.TryDequeue(out IGameItem item))
            {
                isFreshlyCreated = false;
                item.OnGet();
                return item;
            }
            
            item = onCreateFunc(id);
            isFreshlyCreated = true;
            item.OnCreate(id);
            return item;
        }

        public override bool Return(IGameItem item)
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

        public override IEnumerator<IGameItem> GetEnumerator() => pool.GetEnumerator();
    }
}