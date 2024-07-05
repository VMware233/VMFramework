#if UNITY_EDITOR
using VMFramework.Core;

namespace VMFramework.UI
{
    public partial class UIPanelGeneralSetting
    {
        // private const string DEFAULT_THEME_STYLE_SHEET_NAME = "UnityDefaultRuntimeTheme";
        
        protected override void OnInspectorInit()
        {
            base.OnInspectorInit();

            container ??= new();
            
            container.SetDefaultContainerID("$UI");

            var testContainer = container.GetContainer();

            testContainer.AssertIsNotNull(nameof(testContainer));

            // if (defaultTheme == null)
            // {
            //     defaultTheme = DEFAULT_THEME_STYLE_SHEET_NAME.FindAssetOfName<ThemeStyleSheet>();
            // }
        }
    }
}
#endif