#if UNITY_EDITOR
using UnityEngine;
using VMFramework.Editor;

namespace VMFramework.ResourcesManagement
{
    public partial class SpritePreset : IGameEditorMenuTreeNode
    {
        Sprite IGameEditorMenuTreeNode.spriteIcon => spritePreview;
    }
}
#endif