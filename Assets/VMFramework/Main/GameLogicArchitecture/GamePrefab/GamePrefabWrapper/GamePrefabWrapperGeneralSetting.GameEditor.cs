#if UNITY_EDITOR
using VMFramework.Editor.GameEditor;

namespace VMFramework.GameLogicArchitecture.Editor
{
    public partial class GamePrefabWrapperEditorGeneralSetting : IGameEditorMenuTreeNode
    {
        string INameOwner.name => "Game Prefab Wrapper";
    }
}
#endif