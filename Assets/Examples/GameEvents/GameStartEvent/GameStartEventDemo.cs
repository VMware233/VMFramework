using System;
using UnityEngine;
using VMFramework.GameEvents;
using VMFramework.Procedure;

namespace VMFramework.Examples
{
    [ManagerCreationProvider("Demo")]
    public class GameStartEventDemo : ManagerBehaviour<GameStartEventDemo>, IManagerBehaviour
    {
        void IInitializer.OnInitComplete(Action onDone)
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