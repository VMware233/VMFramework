#if UNITY_EDITOR
using System.Collections.Generic;
using VMFramework.GameLogicArchitecture;

namespace VMFramework.Editor.GameEditor
{
    public interface IGameEditorMenuTreeNodesProvider : INameOwner
    {
        public bool autoStackMenuTreeNodes => false;

        public bool isMenuTreeNodesVisible { get; }

        public IEnumerable<IGameEditorMenuTreeNode> GetAllMenuTreeNodes();
    }
}
#endif