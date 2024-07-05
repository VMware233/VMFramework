#if UNITY_EDITOR
using Sirenix.OdinInspector;
using VMFramework.Editor;
using VMFramework.Editor.GameEditor;

namespace VMFramework.GameLogicArchitecture
{
    public partial class BuiltInModulesSettingFile : IGameEditorMenuTreeNode
    {
        string INameOwner.name => "Built-In Modules";

        Icon IGameEditorMenuTreeNode.icon => SdfIconType.Inboxes;
    }
}
#endif