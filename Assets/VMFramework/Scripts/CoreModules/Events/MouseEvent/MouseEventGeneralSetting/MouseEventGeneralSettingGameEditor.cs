#if UNITY_EDITOR
using Sirenix.OdinInspector;
using VMFramework.Editor;
using VMFramework.GameLogicArchitecture;
using VMFramework.Localization;

namespace VMFramework
{
    public partial class MouseEventGeneralSetting : IGameEditorMenuTreeNode
    {
        string INameOwner.name => new LocalizedTempString()
        {
            { "zh-CN", "鼠标事件" },
            { "en-US", "MouseEvent" }
        };

        EditorIconType IGameEditorMenuTreeNode.iconType => EditorIconType.SdfIcon;

        SdfIconType IGameEditorMenuTreeNode.sdfIcon => SdfIconType.Mouse2;

        string IGameEditorMenuTreeNode.folderPath => GameEditorNames.coreCategoryName;
    }
}
#endif