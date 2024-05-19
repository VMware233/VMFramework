using System;
using UnityEngine;

namespace VMFramework.Examples
{
    public class UIOpenEventDemo : MonoBehaviour
    {
        private void Start()
        {
            UIOpenEvent.AddCallback(gameEvent =>
            {
                Debug.LogWarning($"UI:{gameEvent.uiID} has been opened.");
            });
            
            using var uiOpenEvent = UIOpenEvent.Get();
            uiOpenEvent.SetParameters("InventoryUI");
            uiOpenEvent.Propagate();
        }
    }
}