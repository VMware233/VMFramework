using System.Runtime.CompilerServices;

namespace VMFramework.UI
{
    public partial class TracingUIManager
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ITracingUIPanel OpenOn(string panelID, TracingConfig config)
        {
            var panel = UIPanelManager.GetClosedOrCreatePanel<ITracingUIPanel>(panelID);

            panel.Open();

            StartTracing(panel, config);

            return panel;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TPanel OpenOn<TPanel>(string panelID, TracingConfig config) where TPanel : ITracingUIPanel
        {
            var panel = UIPanelManager.GetClosedOrCreatePanel<TPanel>(panelID);

            panel.Open();

            StartTracing(panel, config);

            return panel;
        }
    }
}