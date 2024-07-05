#if UNITY_EDITOR
using Sirenix.OdinInspector;
using VMFramework.Editor;
using VMFramework.Editor.GameEditor;

namespace VMFramework.GameLogicArchitecture
{
    public partial class CoreSettingFile : IGameEditorMenuTreeNode
    {
        string INameOwner.name => "Core Runtime Settings";

        Icon IGameEditorMenuTreeNode.icon => SdfIconType.Asterisk;
    }
}
#endif