#if UNITY_EDITOR
using Sirenix.OdinInspector;
using VMFramework.Editor.GameEditor;
using VMFramework.GameLogicArchitecture;

namespace VMFramework.Editor
{
    public partial class HierarchyGeneralSetting : IGameEditorMenuTreeNode
    {
        string INameOwner.name => "Hierarchy";

        Icon IGameEditorMenuTreeNode.icon => new(SdfIconType.FileEarmarkRichtextFill);
    }
}
#endif