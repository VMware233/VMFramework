#if UNITY_EDITOR
using Sirenix.OdinInspector;
using VMFramework.Editor;
using VMFramework.Editor.GameEditor;
using VMFramework.GameLogicArchitecture;

namespace VMFramework.Properties
{
    public partial class GamePropertyGeneralSetting : IGameEditorMenuTreeNode
    {
        string INameOwner.name => "Game Property";

        Icon IGameEditorMenuTreeNode.icon => SdfIconType.JournalText;
    }
}
#endif