#if UNITY_EDITOR
using Sirenix.OdinInspector;
using VMFramework.Editor;
using VMFramework.Editor.GameEditor;
using VMFramework.GameLogicArchitecture;

namespace VMFramework.UI
{
    public partial class DebugUIPanelGeneralSetting : IGameEditorMenuTreeNode
    {
        string INameOwner.name => "Debug Entry";

        Icon IGameEditorMenuTreeNode.icon => SdfIconType.Bug;
    }
}
#endif