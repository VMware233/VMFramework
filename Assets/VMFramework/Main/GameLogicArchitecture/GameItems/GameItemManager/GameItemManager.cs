using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using VMFramework.Core;

namespace VMFramework.GameLogicArchitecture
{
    public static partial class GameItemManager
    {
        public sealed class GameItemManagerEventsReceiver : IGameItemEventsReceiver
        {
            public event Action<IGameItem> OnGameItemCreated
            {
                add => GameItemManager.OnGameItemCreated += value;
                remove => GameItemManager.OnGameItemCreated -= value;
            }
            public event Action<IGameItem> OnGameItemDestroyed
            {
                add => GameItemManager.OnGameItemDestroyed += value;
                remove => GameItemManager.OnGameItemDestroyed -= value;
            }
        }
        
        private static event Action<IGameItem> OnGameItemCreated;
        private static event Action<IGameItem> OnGameItemDestroyed;
        
        private static readonly Dictionary<string, GameItemPool> pools = new();
        private static readonly CreateGameItemHandler createGameItemHandler = CreateGameItem;

        private static IGameItem CreateGameItem(string id)
        {
            var gamePrefab = GamePrefabManager.GetGamePrefabStrictly(id);
            
            var gameItemType = gamePrefab.GameItemType;
            
            gameItemType.AssertIsNotNull(nameof(gameItemType));
            
            var gameItem = (IGameItem)Activator.CreateInstance(gameItemType);
            
            return gameItem;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static GameItemPool CreatePool(string id)
        {
            var pool = new GameItemPool(id, createGameItemHandler, 1000);
            pools.Add(id, pool);
            return pool;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGameItem Get(string id)
        {
            if (pools.TryGetValue(id, out var pool) == false)
            {
                pool = CreatePool(id);
            }
            
            var gameItem = pool.Get(out _);
            
            OnGameItemCreated?.Invoke(gameItem);
            
            return gameItem;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TGameItem Get<TGameItem>(string id) where TGameItem : IGameItem
        {
            if (pools.TryGetValue(id, out var pool) == false)
            {
                pool = CreatePool(id);
            }
            
            var gameItem = pool.Get(out _);
            
            OnGameItemCreated?.Invoke(gameItem);
            
            return (TGameItem)gameItem;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Return(IGameItem gameItem)
        {
            if (pools.TryGetValue(gameItem.id, out var pool) == false)
            {
                pool = CreatePool(gameItem.id);
            }
            
            pool.Return(gameItem);
            
            OnGameItemDestroyed?.Invoke(gameItem);
        }
        
        /// <summary>
        /// Clones the game item and returns the clone.
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TGameItem GetClone<TGameItem>(this TGameItem instance) where TGameItem : IGameItem
        {
            instance.AssertIsNotNull(nameof(instance));
            
            var clone = Get(instance.id);

            clone.CloneTo(instance);

            return (TGameItem)clone;
        }
    }
}