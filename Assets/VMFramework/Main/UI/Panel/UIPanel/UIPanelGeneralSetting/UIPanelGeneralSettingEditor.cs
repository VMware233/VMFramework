#if UNITY_EDITOR
using VMFramework.Core;

namespace VMFramework.UI
{
    public partial class UIPanelGeneralSetting
    {
        protected override void OnInspectorInit()
        {
            base.OnInspectorInit();

            container ??= new();
            
            container.SetDefaultContainerID("$UI");

            var testContainer = container.GetContainer();

            testContainer.AssertIsNotNull(nameof(testContainer));
        }
    }
}
#endif