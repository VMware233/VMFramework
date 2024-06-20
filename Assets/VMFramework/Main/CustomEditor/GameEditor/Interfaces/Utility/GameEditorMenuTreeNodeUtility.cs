#if UNITY_EDITOR
using System.Runtime.CompilerServices;

namespace VMFramework.Editor.GameEditor
{
    public static class GameEditorMenuTreeNodeUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetNodePath(this IGameEditorMenuTreeNode node)
        {
            return node?.nodePath;
        }
    }
}
#endif