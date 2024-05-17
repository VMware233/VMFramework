#if UNITY_EDITOR
using VMFramework.Editor;

namespace VMFramework.ResourcesManagement
{
    public partial class SpritePreset : IGameEditorMenuTreeNode
    {
        public Icon icon => preloadFlipXPreview;
    }
}
#endif