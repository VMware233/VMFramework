#if UNITY_EDITOR
using Sirenix.OdinInspector;
using VMFramework.Editor;
using VMFramework.Editor.GameEditor;

namespace VMFramework.GameLogicArchitecture
{
    public partial class GameTypeGeneralSetting : IGameEditorMenuTreeNode
    {
        string INameOwner.name => "Game Type";

        Icon IGameEditorMenuTreeNode.icon => new(SdfIconType.Collection);
    }
}
#endif