#if UNITY_EDITOR
using Sirenix.OdinInspector;
using VMFramework.Editor;
using VMFramework.Editor.GameEditor;
using VMFramework.GameLogicArchitecture;

namespace VMFramework.ExtendedTilemap
{
    public partial class ExtendedRuleTileGeneralSetting : IGameEditorMenuTreeNode
    {
        string INameOwner.name => "Ext Rule Tile";

        public Icon icon => SdfIconType.Grid3x3;
    }
}
#endif