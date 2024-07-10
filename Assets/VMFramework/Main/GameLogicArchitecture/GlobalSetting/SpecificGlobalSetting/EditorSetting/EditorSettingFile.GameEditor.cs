#if UNITY_EDITOR
using Sirenix.Utilities.Editor;
using VMFramework.Editor;
using VMFramework.Editor.GameEditor;

namespace VMFramework.GameLogicArchitecture.Editor
{
    public partial class EditorSettingFile : IGameEditorMenuTreeNode
    {
        string INameOwner.name => EditorNames.EDITOR_SETTINGS;

        public Icon icon => EditorIcons.UnityLogo;
    }
}
#endif