#if UNITY_EDITOR
using Sirenix.OdinInspector;
using VMFramework.Editor;
using VMFramework.Editor.GameEditor;
using VMFramework.GameLogicArchitecture;

namespace VMFramework.GameEvents
{
    public partial class GameEventGeneralSetting : IGameEditorMenuTreeNode
    {
        string INameOwner.name => "Game Event";

        Icon IGameEditorMenuTreeNode.icon => new(SdfIconType.Dpad);
    }
}
#endif