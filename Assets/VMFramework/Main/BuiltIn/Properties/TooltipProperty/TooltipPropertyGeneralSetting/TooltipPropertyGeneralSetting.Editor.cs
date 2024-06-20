#if UNITY_EDITOR
namespace VMFramework.Properties
{
    public partial class TooltipPropertyGeneralSetting
    {
        protected override void OnInspectorInit()
        {
            base.OnInspectorInit();

            tooltipPropertyConfigs ??= new();
        }
    }
}
#endif