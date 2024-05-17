using System;
using UnityEngine;

namespace VMFramework.Examples
{
    public class GameStopEventDemo : MonoBehaviour
    {
        private void Start()
        {
            GameStopEvent.AddCallback(gameEvent =>
            {
                Debug.LogWarning($"Game has stopped with errorCode : {gameEvent.errorCode}");
            });
            
            GameStopEvent.SetParameters(0);
            GameStopEvent.Propagate();
        }
    }
}