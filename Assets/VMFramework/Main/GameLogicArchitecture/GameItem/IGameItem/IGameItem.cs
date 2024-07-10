using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using VMFramework.Core;

namespace VMFramework.GameLogicArchitecture
{
    public partial interface IGameItem : IIDOwner, INameOwner, IReadOnlyGameTypeOwner, IDestructible
    {
        public static event Action<IGameItem> OnGameItemCreated;
        public static event Action<IGameItem> OnGameItemDestroyed;

        private static readonly Dictionary<string, Stack<IGameItem>> gameItemsPool = new();
        
        protected IGameTypedGamePrefab origin { get; set; }

        string INameOwner.name => origin.name;
        
        public bool isDebugging => origin.isDebugging;

        #region Create

        protected void OnFirstCreatedGameItem();
        
        protected void OnCreatedGameItem();
        
        public static IGameItem Create(string id)
        {
            if (GamePrefabManager.TryGetGamePrefab(id, out IGameTypedGamePrefab gamePrefab) == false)
            {
                Debug.LogError($"Could not find {typeof(IGameTypedGamePrefab)} with id: " + id);
                return null;
            }

            IGameItem gameItem;
            
            bool firstCreated = false;

            if (gameItemsPool.TryGetValue(id, out var pool) && pool.Count > 0)
            {
                gameItem = pool.Pop();
                
                firstCreated = false;
            }
            else
            {
                var gameItemType = gamePrefab.gameItemType;
            
                gameItemType.AssertIsNotNull(nameof(gameItemType));

                gameItem = (IGameItem)Activator.CreateInstance(gameItemType);
                
                firstCreated = true;
            }
            
            gameItem.origin = gamePrefab;

            gameItem.SetDestructible(false);
            
            if (firstCreated)
            {
                gameItem.OnFirstCreatedGameItem();
            }
            
            gameItem.OnCreatedGameItem();
            
            OnGameItemCreated?.Invoke(gameItem);
            
            return gameItem;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TGameItem Create<TGameItem>(string id) where TGameItem : IGameItem
        {
            return (TGameItem)Create(id);
        }

        #endregion

        #region Destroy

        protected void OnDestroyGameItem();

        public static void Destroy(IGameItem gameItem)
        {
            if (gameItem == null)
            {
                Debug.LogWarning("GameItem is null.");
                return;
            }
            
            if (gameItem.isDestroyed)
            {
                Debug.LogWarning($"GameItem with id: {gameItem.id} has already been destroyed.");
                return;
            }
            
            gameItem.OnDestroyGameItem();
            
            gameItem.SetDestructible(true);
            
            OnGameItemDestroyed?.Invoke(gameItem);

            var pool = gameItemsPool.GetValueOrAddNew(gameItem.id);
            
            pool.Push(gameItem);
        }

        #endregion
    }
}