#if UNITY_EDITOR
namespace VMFramework.UI
{
    public partial class TracingTooltipGeneralSetting
    {
        protected override void OnInspectorInit()
        {
            base.OnInspectorInit();

            tooltipPriorityPresets ??= new();

            defaultPriority ??= new();
        }
    }
}
#endif