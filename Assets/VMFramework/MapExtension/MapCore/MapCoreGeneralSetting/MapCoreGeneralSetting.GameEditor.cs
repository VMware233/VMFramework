#if UNITY_EDITOR
using Sirenix.OdinInspector;
using VMFramework.Editor;
using VMFramework.Editor.GameEditor;
using VMFramework.GameLogicArchitecture;

namespace VMFramework.Maps
{
    public partial class MapCoreGeneralSetting : IGameEditorMenuTreeNode
    {
        string INameOwner.name => "Map Core";

        Icon IGameEditorMenuTreeNode.icon => SdfIconType.PinMap;
    }
}
#endif