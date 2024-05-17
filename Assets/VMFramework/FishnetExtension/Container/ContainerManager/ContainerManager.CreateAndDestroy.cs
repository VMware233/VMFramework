#if FISHNET
using System;
using UnityEngine;
using VMFramework.GameEvents;
using VMFramework.Procedure;

namespace VMFramework.Containers
{
    public partial class ContainerManager : IManagerBehaviour
    {
        void IInitializer.OnInitComplete(Action onDone)
        {
            GameEventManager.AddCallback<ContainerCreateEvent>(ContainerCreateEventConfig.ID,
                OnContainerCreate, GameEventPriority.SUPER);
            
            GameEventManager.AddCallback<ContainerDestroyEvent>(ContainerDestroyEventConfig.ID,
                OnContainerDestroy, GameEventPriority.SUPER);
            
            onDone();
        }

        private static void OnContainerCreate(ContainerCreateEvent gameEvent)
        {
            if (gameEvent.container == null)
            {
                Debug.LogError("Container is null in ContainerCreateEvent");
                return;
            }

            if (instance.IsServerStarted)
            {
                string uuid = Guid.NewGuid().ToString();
                gameEvent.container.SetUUID(uuid);
            }
            
            gameEvent.container.OnOpenEvent += OnContainerOpen;
            gameEvent.container.OnCloseEvent += OnContainerClose;
        }

        private static void OnContainerDestroy(ContainerDestroyEvent gameEvent)
        {
            if (gameEvent.container == null)
            {
                Debug.LogError("Container is null in ContainerDestroyEvent");
                return;
            }
            
            gameEvent.container.OnOpenEvent -= OnContainerOpen;
            gameEvent.container.OnCloseEvent -= OnContainerClose;
            
            Unregister(gameEvent.container);
        }
        
        private static void OnContainerOpen(IContainer container)
        {
            if (instance.IsServerStarted)
            {
                container.isDirty = false;
            }

            Observe(container);
        }

        private static void OnContainerClose(IContainer container)
        {
            Unobserve(container);
        }
    }
}
#endif