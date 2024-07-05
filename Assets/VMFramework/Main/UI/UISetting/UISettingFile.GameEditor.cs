#if UNITY_EDITOR
using Sirenix.OdinInspector;
using VMFramework.Editor;
using VMFramework.Editor.GameEditor;
using VMFramework.GameLogicArchitecture;

namespace VMFramework.UI
{
    public partial class UISettingFile : IGameEditorMenuTreeNode
    {
        string INameOwner.name => "UI";

        Icon IGameEditorMenuTreeNode.icon => SdfIconType.Window;
    }
}
#endif