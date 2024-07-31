using System;
using System.Collections.Generic;
using UnityEngine;
using VMFramework.Procedure;

namespace VMFramework.GameLogicArchitecture
{
    public partial class GamePrefab
    {
        protected virtual IEnumerable<InitializationAction> GetInitializationActions()
        {
            yield return new(InitializationOrder.Init, OnInitInternal, this);
        }
        
        IEnumerable<InitializationAction> IInitializer.GetInitializationActions()
        {
            return GetInitializationActions();
        }

        public virtual void CheckSettings()
        {
            if (GameItemType is { IsAbstract: true })
            {
                Debug.LogError($"{nameof(GameItemType)} is abstract. " +
                               $"Please override with a concrete type instead of {GameItemType}");
            }
        }

        private void OnInitInternal(Action onDone)
        {
            OnInit();
            onDone();
        }

        protected virtual void OnInit()
        {
            
        }
    }
}