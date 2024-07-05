#if UNITY_EDITOR
using Sirenix.OdinInspector;
using VMFramework.Editor;
using VMFramework.Editor.GameEditor;
using VMFramework.GameLogicArchitecture;

namespace VMFramework.UI
{
    public partial class UIPanelGeneralSetting : IGameEditorMenuTreeNode
    {
        string INameOwner.name => "UI Panel";

        Icon IGameEditorMenuTreeNode.icon => SdfIconType.LayoutWtf;
    }
}
#endif