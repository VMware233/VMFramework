#if UNITY_EDITOR
using System.Collections.Generic;
using VMFramework.Editor;
using VMFramework.Editor.GameEditor;

namespace VMFramework.GameLogicArchitecture
{
    public partial class GamePrefabSingleWrapper : IGameEditorMenuTreeNode
    {
        Icon IGameEditorMenuTreeNode.Icon
        {
            get
            {
                if (gamePrefab is IGameEditorMenuTreeNode node)
                {
                    return node.Icon;
                }
                
                return Icon.None;
            }
        }

        protected override IEnumerable<ToolbarButtonConfig> GetToolbarButtons()
        {
            if (gamePrefab is IGameEditorToolbarProvider provider)
            {
                foreach (var config in provider.GetToolbarButtons())
                {
                    yield return config;
                }
            }
            
            foreach (var config in base.GetToolbarButtons())
            {
                yield return config;
            }
        }
    }
}
#endif