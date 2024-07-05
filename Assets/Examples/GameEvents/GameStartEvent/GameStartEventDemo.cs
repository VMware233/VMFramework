using System;
using System.Collections.Generic;
using UnityEngine;
using VMFramework.Core.Linq;
using VMFramework.GameEvents;
using VMFramework.Procedure;

namespace VMFramework.Examples
{
    [ManagerCreationProvider("Demo")]
    public sealed class GameStartEventDemo : ManagerBehaviour<GameStartEventDemo>
    {
        protected override IEnumerable<InitializationAction> GetInitializationActions()
        {
            return base.GetInitializationActions()
                .Concat(new(InitializationOrder.InitComplete, OnInitComplete, this));
        }

        private void OnInitComplete(Action onDone)
        {
            // Add a callback to the GameStartEvent
            GameEventManager.AddCallback(GameStartEventConfig.ID, (GameStartEvent gameEvent) =>
            {
                Debug.LogWarning($"Game Started with {gameEvent.playerCount} players");
                
                // You can stop the propagation of the event if you want to prevent others from receiving it.
                gameEvent.StopPropagation();
            }, GameEventPriority.SUPER);
            
            // Propagate the GameStartEvent
            if (GameEventManager.TryGetGameEvent(GameStartEventConfig.ID, out GameStartEvent gameStartEvent))
            {
                gameStartEvent.SetParameters(2);
                
                gameStartEvent.Propagate();
            }
            
            onDone();
        }
    }
}