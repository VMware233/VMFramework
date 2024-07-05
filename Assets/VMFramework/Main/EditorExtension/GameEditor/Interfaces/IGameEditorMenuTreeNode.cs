#if UNITY_EDITOR
using VMFramework.Core;
using VMFramework.GameLogicArchitecture;

namespace VMFramework.Editor.GameEditor
{
    public interface IGameEditorMenuTreeNode : INameOwner, IParentProvider<IGameEditorMenuTreeNode>
    {
        public Icon icon => Icon.None;

        public IGameEditorMenuTreeNode parentNode => null;
        
        public bool isVisible => true;

        IGameEditorMenuTreeNode IParentProvider<IGameEditorMenuTreeNode>.GetParent()
        {
            return parentNode;
        }
    }
}
#endif