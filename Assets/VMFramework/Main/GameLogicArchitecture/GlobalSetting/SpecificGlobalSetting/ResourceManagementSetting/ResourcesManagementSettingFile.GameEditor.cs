#if UNITY_EDITOR
using Sirenix.OdinInspector;
using VMFramework.Editor;
using VMFramework.Editor.GameEditor;

namespace VMFramework.GameLogicArchitecture
{
    public partial class ResourcesManagementSettingFile : IGameEditorMenuTreeNode
    {
        string INameOwner.name => EditorNames.RESOURCES_MANAGEMENT_SETTINGS;

        Icon IGameEditorMenuTreeNode.icon => SdfIconType.Boxes;
    }
}
#endif