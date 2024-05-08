using UnityEngine;
using VMFramework.Procedure;

namespace VMFramework.UI
{
    [ManagerCreationProvider(ManagerType.UICore)]
    public class TracingTooltipManager : UniqueMonoBehaviour<TracingTooltipManager>
    {
        public static void Open(ITracingTooltipProvider tooltipProvider, IUIPanelController source)
        {
            if (tooltipProvider == null)
            {
                Debug.LogWarning($"{nameof(tooltipProvider)} is Null");
                return;
            }

            if (tooltipProvider.DisplayTooltip() == false)
            {
                return;
            }

            var tooltipID = tooltipProvider.GetTooltipID();

            var tooltip = UIPanelPool.GetUniquePanelStrictly<ITracingTooltip>(tooltipID);

            tooltip.Open(tooltipProvider, source);
        }

        public static void Close(ITracingTooltipProvider tooltipProvider)
        {
            if (tooltipProvider == null)
            {
                Debug.LogWarning($"{nameof(tooltipProvider)} is Null");
                return;
            }

            var tooltipID = tooltipProvider.GetTooltipID();

            var tooltip = UIPanelPool.GetUniquePanelStrictly<ITracingTooltip>(tooltipID);

            tooltip.Close(tooltipProvider);
        }
    }
}