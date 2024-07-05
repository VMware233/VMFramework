#if UNITY_EDITOR
using Sirenix.OdinInspector;
using VMFramework.Editor;
using VMFramework.Editor.GameEditor;
using VMFramework.GameLogicArchitecture;

namespace VMFramework
{
    public partial class CameraGeneralSetting : IGameEditorMenuTreeNode
    {
        string INameOwner.name => "Camera";

        Icon IGameEditorMenuTreeNode.icon => SdfIconType.Camera;
    }
}
#endif