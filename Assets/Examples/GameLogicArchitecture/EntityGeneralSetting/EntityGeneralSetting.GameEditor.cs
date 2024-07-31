#if UNITY_EDITOR
using VMFramework.Editor;
using VMFramework.Editor.GameEditor;
using VMFramework.GameLogicArchitecture;

namespace VMFramework.Examples 
{
    public partial class EntityGeneralSetting : IGameEditorMenuTreeNode
    {
        string INameOwner.name => "Entity";

        Icon IGameEditorMenuTreeNode.Icon => Icon.None;
    }
}
#endif