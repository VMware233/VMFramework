#if UNITY_EDITOR
using Sirenix.OdinInspector;
using VMFramework.Editor;
using VMFramework.Editor.GameEditor;
using VMFramework.GameLogicArchitecture;

namespace VMFramework.GameEvents
{
    public partial class ColliderMouseEventGeneralSetting : IGameEditorMenuTreeNode
    {
        string INameOwner.name => "Mouse Event";

        Icon IGameEditorMenuTreeNode.icon => new(SdfIconType.Mouse2);
    }
}
#endif