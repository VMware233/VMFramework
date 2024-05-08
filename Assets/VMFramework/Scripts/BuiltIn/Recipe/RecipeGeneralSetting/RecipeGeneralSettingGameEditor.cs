#if UNITY_EDITOR
using Sirenix.OdinInspector;
using VMFramework.Editor;
using VMFramework.GameLogicArchitecture;
using VMFramework.Localization;

namespace VMFramework.Recipe
{
    public partial class RecipeGeneralSetting : IGameEditorMenuTreeNode
    {
        string INameOwner.name => new LocalizedTempString()
        {
            { "zh-CN", "配方" },
            { "en-US", "Recipe" }
        };

        string IGameEditorMenuTreeNode.folderPath => GameEditorNames.builtInCategoryName;

        EditorIconType IGameEditorMenuTreeNode.iconType => EditorIconType.SdfIcon;

        SdfIconType IGameEditorMenuTreeNode.sdfIcon => SdfIconType.Grid3x3GapFill;
    }
}
#endif