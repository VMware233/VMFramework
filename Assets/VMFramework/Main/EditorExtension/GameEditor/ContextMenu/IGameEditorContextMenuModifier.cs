#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;

namespace VMFramework.Editor.GameEditor
{
    public interface IGameEditorContextMenuModifier
    {
        public void ModifyContextMenu(IGameEditorContextMenuProvider provider,
            IReadOnlyList<IGameEditorMenuTreeNode> selectedNodes, GenericMenu menu);
    }
}
#endif