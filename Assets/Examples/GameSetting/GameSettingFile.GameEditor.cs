#if UNITY_EDITOR
using VMFramework.Editor.GameEditor;
using VMFramework.GameLogicArchitecture;

namespace VMFramework.Examples
{
    public partial class GameSettingFile : IGameEditorMenuTreeNode
    {
        string INameOwner.name => "Game Setting";
    }
}
#endif