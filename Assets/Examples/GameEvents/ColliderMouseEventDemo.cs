using System;
using UnityEngine;
using VMFramework.GameEvents;
using VMFramework.Procedure;

namespace VMFramework.Examples
{
    [ManagerCreationProvider("Demo")]
    public class ColliderMouseEventDemo : ManagerBehaviour<ColliderMouseEventDemo>, IManagerBehaviour
    {
        void IInitializer.OnInitComplete(Action onDone)
        {
            ColliderMouseEventManager.AddCallback(MouseEventType.PointerEnter, OnPointerEnter);
            ColliderMouseEventManager.AddCallback(MouseEventType.PointerLeave, OnPointerLeave);
        }

        private void OnPointerEnter(ColliderMouseEvent gameEvent)
        {
            Debug.Log("Pointer Entered: " + gameEvent.trigger.name);
        }
        
        private void OnPointerLeave(ColliderMouseEvent gameEvent)
        {
            Debug.Log("Pointer Left: " + gameEvent.trigger.name);
        }
    }
}