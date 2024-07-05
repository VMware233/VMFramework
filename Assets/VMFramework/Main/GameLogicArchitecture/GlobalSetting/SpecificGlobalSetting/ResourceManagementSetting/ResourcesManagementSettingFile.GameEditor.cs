#if UNITY_EDITOR
using Sirenix.OdinInspector;
using VMFramework.Editor;
using VMFramework.Editor.GameEditor;

namespace VMFramework.GameLogicArchitecture
{
    public partial class ResourcesManagementSettingFile : IGameEditorMenuTreeNode
    {
        string INameOwner.name => "Res. Management";

        Icon IGameEditorMenuTreeNode.icon => SdfIconType.Boxes;
    }
}
#endif