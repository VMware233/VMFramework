#if UNITY_EDITOR
using Sirenix.OdinInspector;
using VMFramework.Editor;
using VMFramework.Editor.GameEditor;

namespace VMFramework.GameLogicArchitecture
{
    public partial class CoreSettingFile : IGameEditorMenuTreeNode
    {
        string INameOwner.name => EditorNames.CORE_SETTINGS;

        Icon IGameEditorMenuTreeNode.icon => SdfIconType.Asterisk;
    }
}
#endif