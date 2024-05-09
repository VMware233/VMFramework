using System;
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

        protected void OnCreate();
        
        public static IGameItem Create(string id)
        {
            if (GamePrefabManager.TryGetGamePrefab(id, out IGameTypedGamePrefab gamePrefab) == false)
            {
                Debug.LogError($"Could not find {typeof(IGameTypedGamePrefab)} with id: " + id);
                return null;
            }

            var gameItem = (IGameItem)Activator.CreateInstance(gamePrefab.gameItemType);
            
            gameItem.origin = gamePrefab;
            
            gameItem.OnCreate();
            
            return gameItem;
        }

        public static TGameItem Create<TGameItem>(string id) where TGameItem : IGameItem
        {
            return (TGameItem)Create(id);
        }

        #endregion
    }
}