#if UNITY_EDITOR
using Sirenix.OdinInspector;
using VMFramework.Editor;
using VMFramework.Editor.GameEditor;
using VMFramework.GameLogicArchitecture;

namespace VMFramework.ResourcesManagement
{
    public partial class AudioGeneralSetting : IGameEditorMenuTreeNode
    {
        string INameOwner.name => "Audio";

        Icon IGameEditorMenuTreeNode.icon => SdfIconType.MusicNoteBeamed;
    }
}
#endif