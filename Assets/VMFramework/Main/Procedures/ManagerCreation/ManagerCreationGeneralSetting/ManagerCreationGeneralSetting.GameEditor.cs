#if UNITY_EDITOR
using Sirenix.OdinInspector;
using VMFramework.Editor;
using VMFramework.Editor.GameEditor;
using VMFramework.GameLogicArchitecture;
using VMFramework.Localization;

namespace VMFramework.Procedure
{
    public partial class ManagerCreationGeneralSetting : IGameEditorMenuTreeNode
    {
        string INameOwner.name => new LocalizedTempString
        {
            { "zh-CN", "管理器创建" },
            { "en-US", "Manager Creation" }
        };

        Icon IGameEditorMenuTreeNode.icon => new(SdfIconType.Collection);

        string IGameEditorMenuTreeNode.folderPath => GameEditorNames.coreCategoryName;
    }
}
#endif