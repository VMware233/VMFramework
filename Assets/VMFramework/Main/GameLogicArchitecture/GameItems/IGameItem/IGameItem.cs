using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using VMFramework.Core;
using VMFramework.Core.Pools;

namespace VMFramework.GameLogicArchitecture
{
    public partial interface IGameItem : IIDOwner, INameOwner, IReadOnlyGameTypeOwner, IDestructible, IPoolItem
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void CloneTo(IGameItem other);

        void IDestructible.Destruct()
        {
            GameItemManager.Return(this);
        }

        public void OnCreate(string id);

        // #region Create

        // protected void OnFirstCreatedGameItem();
        //
        // protected void OnCreatedGameItem();
        //
        // public static IGameItem Create(string id)
        // {
        //     if (GamePrefabManager.TryGetGamePrefab(id, out IGameTypedGamePrefab gamePrefab) == false)
        //     {
        //         Debug.LogError($"Could not find {typeof(IGameTypedGamePrefab)} with id: " + id);
        //         return null;
        //     }
        //
        //     IGameItem gameItem;
        //     
        //     bool firstCreated = false;
        //
        //     if (gameItemsPool.TryGetValue(id, out var pool) && pool.Count > 0)
        //     {
        //         gameItem = pool.Pop();
        //         
        //         firstCreated = false;
        //     }
        //     else
        //     {
        //         var gameItemType = gamePrefab.GameItemType;
        //     
        //         gameItemType.AssertIsNotNull(nameof(gameItemType));
        //
        //         gameItem = (IGameItem)Activator.CreateInstance(gameItemType);
        //         
        //         firstCreated = true;
        //     }
        //     
        //     gameItem.Origin = gamePrefab;
        //
        //     gameItem.SetDestructible(false);
        //     
        //     if (firstCreated)
        //     {
        //         gameItem.OnFirstCreatedGameItem();
        //     }
        //     
        //     gameItem.OnCreatedGameItem();
        //     
        //     OnGameItemCreated?.Invoke(gameItem);
        //     
        //     return gameItem;
        // }
        //
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        // public static TGameItem Create<TGameItem>(string id) where TGameItem : IGameItem
        // {
        //     return (TGameItem)Create(id);
        // }
        //
        // #endregion
        //
        // #region Destroy
        //
        // protected void OnDestroyGameItem();
        //
        // public static void Destroy(IGameItem gameItem)
        // {
        //     if (gameItem == null)
        //     {
        //         Debug.LogWarning("GameItem is null.");
        //         return;
        //     }
        //     
        //     if (gameItem.isDestroyed)
        //     {
        //         Debug.LogWarning($"GameItem with id: {gameItem.id} has already been destroyed.");
        //         return;
        //     }
        //     
        //     gameItem.OnDestroyGameItem();
        //     
        //     gameItem.SetDestructible(true);
        //     
        //     OnGameItemDestroyed?.Invoke(gameItem);
        //
        //     var pool = gameItemsPool.GetValueOrAddNew(gameItem.id);
        //     
        //     pool.Push(gameItem);
        // }

        // #endregion
    }
}