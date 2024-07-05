using UnityEngine;
using VMFramework.GameEvents;

namespace VMFramework.Examples
{
    public class GameStopEventDemo : MonoBehaviour
    {
        private void Start()
        {
            GameStopEvent.AddCallback(gameEvent =>
            {
                Debug.LogWarning($"Game has stopped with errorCode : {gameEvent.errorCode}");
                
                // If you want to stop the propagation of the event
                gameEvent.StopPropagation();
                
            }, GameEventPriority.SUPER);
            
            GameStopEvent.SetParameters(0);
            GameStopEvent.Propagate();
        }
    }
}