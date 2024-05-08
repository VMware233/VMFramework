#if UNITY_EDITOR
using Sirenix.OdinInspector;
using VMFramework.Core.Editor;

namespace VMFramework.UI
{
    public partial class UIPanelPreset
    {
        protected override void OnInspectorInit()
        {
            base.OnInspectorInit();

            globalEventDisabledListOnOpen ??= new();
        }

        [Button("打开控制器脚本"), TabGroup(TAB_GROUP_NAME, TOOLS_CATEGORY)]
        private void OpenControllerTypeScript()
        {
            controllerType.OpenScriptOfType();
        }
    }
}
#endif