#if UNITY_EDITOR
using Sirenix.OdinInspector;
using VMFramework.Editor;
using VMFramework.GameLogicArchitecture;
using VMFramework.Localization;

namespace VMFramework.ResourcesManagement
{
    public partial class AudioGeneralSetting : IGameEditorMenuTreeNode
    {
        string INameOwner.name => new LocalizedTempString()
        {
            { "zh-CN", "音效" },
            { "en-US", "Audio" }
        };

        string IGameEditorMenuTreeNode.folderPath => GameEditorNames.resourcesManagementCategoryName;

        EditorIconType IGameEditorMenuTreeNode.iconType => EditorIconType.SdfIcon;

        SdfIconType IGameEditorMenuTreeNode.sdfIcon => SdfIconType.MusicNoteBeamed;
    }
}
#endif