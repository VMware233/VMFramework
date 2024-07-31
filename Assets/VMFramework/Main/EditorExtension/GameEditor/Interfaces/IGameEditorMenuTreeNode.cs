#if UNITY_EDITOR
using VMFramework.Core;
using VMFramework.GameLogicArchitecture;

namespace VMFramework.Editor.GameEditor
{
    public interface IGameEditorMenuTreeNode : INameOwner, IParentProvider<IGameEditorMenuTreeNode>
    {
        public Icon Icon => Icon.None;

        public IGameEditorMenuTreeNode ParentNode => null;
        
        public bool IsVisible => true;

        IGameEditorMenuTreeNode IParentProvider<IGameEditorMenuTreeNode>.GetParent()
        {
            return ParentNode;
        }
    }
}
#endif