using System;
using System.Collections.Generic;
using VMFramework.Containers;
using VMFramework.Core.Linq;
using VMFramework.Procedure;

namespace VMFramework.UI
{
    public partial class ContainerUIManager : IInitializer
    {
        protected override IEnumerable<InitializationAction> GetInitializationActions()
        {
            return base.GetInitializationActions()
                .Concat(new(InitializationOrder.InitComplete, OnInitComplete, this));
        }

        private static void OnInitComplete(Action onDone)
        {
            ContainerDestroyEvent.AddCallback(OnContainerDestroy);
            
            onDone();
        }

        private static void OnContainerDestroy(ContainerDestroyEvent gameEvent)
        {
            foreach (var panel in openedPanels)
            {
                foreach (var container in panel.GetBindContainers())
                {
                    if (gameEvent.container == container)
                    {
                        panel.Close();
                        
                        break;
                    }
                }
            }
        }
    }
}