#if UNITY_EDITOR
using Sirenix.OdinInspector;
using VMFramework.GameLogicArchitecture;
using VMFramework.Localization;

namespace VMFramework.Editor
{
    public partial class HierarchyComponentIconGeneralSetting : IGameEditorMenuTreeNode
    {
        string INameOwner.name => new LocalizedTempString()
        {
            { "zh-CN", "层级组件图标" },
            { "en-US", "Hierarchy" }
        };

        string IGameEditorMenuTreeNode.folderPath => GameEditorNames.editorCategoryName;

        EditorIconType IGameEditorMenuTreeNode.iconType => EditorIconType.SdfIcon;

        SdfIconType IGameEditorMenuTreeNode.sdfIcon => SdfIconType.BarChartSteps;
    }
}
#endif