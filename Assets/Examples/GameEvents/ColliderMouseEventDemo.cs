using System;
using System.Collections.Generic;
using UnityEngine;
using VMFramework.Core.Linq;
using VMFramework.GameEvents;
using VMFramework.Procedure;

namespace VMFramework.Examples
{
    [ManagerCreationProvider("Demo")]
    public sealed class ColliderMouseEventDemo : ManagerBehaviour<ColliderMouseEventDemo>
    {
        protected override IEnumerable<InitializationAction> GetInitializationActions()
        {
            return base.GetInitializationActions()
                .Concat(new(InitializationOrder.InitComplete, OnInitComplete, this));
        }

        private void OnInitComplete(Action onDone)
        {
            ColliderMouseEventManager.AddCallback(MouseEventType.PointerEnter, OnPointerEnter);
            ColliderMouseEventManager.AddCallback(MouseEventType.PointerExit, OnPointerLeave);
            
            onDone();
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