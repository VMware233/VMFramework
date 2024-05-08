#if UNITY_EDITOR
using Sirenix.OdinInspector;
using VMFramework.GameLogicArchitecture;
using VMFramework.Localization;

namespace VMFramework.Editor
{
    public partial class ColorfulHierarchyGeneralSetting : IGameEditorMenuTreeNode
    {
        string INameOwner.name => new LocalizedTempString()
        {
            { "zh-CN", "彩色层级" },
            { "en-US", "Colorful Hierarchy" }
        };

        string IGameEditorMenuTreeNode.folderPath => GameEditorNames.editorCategoryName;

        EditorIconType IGameEditorMenuTreeNode.iconType => EditorIconType.SdfIcon;

        SdfIconType IGameEditorMenuTreeNode.sdfIcon => SdfIconType.FileEarmarkRichtextFill;
    }
}
#endif