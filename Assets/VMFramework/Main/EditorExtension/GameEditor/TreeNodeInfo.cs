#if UNITY_EDITOR
namespace VMFramework.Editor.GameEditor
{
    internal sealed class TreeNodeInfo
    {
        public IGameEditorMenuTreeNode parent;

        public IGameEditorMenuTreeNodesProvider provider;
    }
}
#endif