namespace VMFramework.UI
{
    public interface ITracingTooltip : IUIPanelController
    {
        public void Open(ITracingTooltipProvider tooltipProvider,
            IUIPanelController source);

        public void Close(ITracingTooltipProvider tooltipProvider);
    }
}