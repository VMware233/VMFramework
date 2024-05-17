using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using VMFramework.Core;

namespace VMFramework.GameLogicArchitecture
{
    public partial interface IGameItem : IIDOwner, INameOwner, IReadOnlyGameTypeOwner
    {
        protected IGameTypedGamePrefab origin { get; set; }

        string INameOwner.name => origin.name;
        
        public bool isDebugging => origin.isDebugging;

        #region Create

        protected void OnCreateGameItem();
        
        public static IGameItem Create(string id)
        {
            if (GamePrefabManager.TryGetGamePrefab(id, out IGameTypedGamePrefab gamePrefab) == false)
            {
                Debug.LogError($"Could not find {typeof(IGameTypedGamePrefab)} with id: " + id);
                return null;
            }
            
            var gameItemType = gamePrefab.gameItemType;
            
            gameItemType.AssertIsNotNull(nameof(gameItemType));

            var gameItem = (IGameItem)Activator.CreateInstance(gameItemType);
            
            gameItem.origin = gamePrefab;
            
            gameItem.OnCreateGameItem();
            
            return gameItem;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TGameItem Create<TGameItem>(string id) where TGameItem : IGameItem
        {
            return (TGameItem)Create(id);
        }

        #endregion
    }
}