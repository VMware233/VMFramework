#if UNITY_EDITOR
using UnityEngine;
using VMFramework.Editor;

namespace VMFramework.Property
{
    public partial class PropertyConfig : IGameEditorMenuTreeNode
    {
        Sprite IGameEditorMenuTreeNode.spriteIcon => icon;
    }
}
#endif