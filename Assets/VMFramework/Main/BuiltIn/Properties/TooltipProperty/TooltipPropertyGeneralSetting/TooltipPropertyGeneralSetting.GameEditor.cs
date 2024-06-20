#if UNITY_EDITOR
using VMFramework.Editor;
using VMFramework.Editor.GameEditor;
using VMFramework.GameLogicArchitecture;
using VMFramework.Localization;

namespace VMFramework.Properties
{
    public partial class TooltipPropertyGeneralSetting : IGameEditorMenuTreeNode
    {
        string INameOwner.name => "Property Tooltip";

        string IGameEditorMenuTreeNode.folderPath => GameCoreSetting.propertyGeneralSetting.GetNodePath();
    }
}
#endif