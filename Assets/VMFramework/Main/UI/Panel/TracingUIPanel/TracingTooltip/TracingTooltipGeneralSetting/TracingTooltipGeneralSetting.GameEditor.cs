#if UNITY_EDITOR
using Sirenix.OdinInspector;
using VMFramework.Editor;
using VMFramework.Editor.GameEditor;
using VMFramework.GameLogicArchitecture;
using VMFramework.Localization;

namespace VMFramework.UI
{
    public partial class TracingTooltipGeneralSetting : IGameEditorMenuTreeNode
    {
        string INameOwner.name => new LocalizedTempString()
        {
            { "zh-CN", "追踪提示框" },
            { "en-US", "Tracing Tooltip" }
        };

        Icon IGameEditorMenuTreeNode.icon => SdfIconType.CardHeading;

        string IGameEditorMenuTreeNode.folderPath =>
            (GameCoreSettingBase.uiPanelGeneralSetting as IGameEditorMenuTreeNode)?.nodePath;
    }
}
#endif