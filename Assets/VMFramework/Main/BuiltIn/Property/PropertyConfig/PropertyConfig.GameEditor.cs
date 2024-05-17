#if UNITY_EDITOR
using VMFramework.Editor;

namespace VMFramework.Property
{
    public partial class PropertyConfig : IGameEditorMenuTreeNode
    {
        Icon IGameEditorMenuTreeNode.icon => icon;
    }
}
#endif