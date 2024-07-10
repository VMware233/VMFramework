#if UNITY_EDITOR
using System.Collections.Generic;
using VMFramework.Core.Linq;
using VMFramework.Editor;
using VMFramework.Editor.GameEditor;

namespace VMFramework.GameLogicArchitecture
{
    public partial class GamePrefabMultipleWrapper : IGameEditorMenuTreeNode
    {
        Icon IGameEditorMenuTreeNode.icon
        {
            get
            {
                if (gamePrefabs.IsNullOrEmpty())
                {
                    return Icon.None;
                }
                
                foreach (var gamePrefab in gamePrefabs)
                {
                    if (gamePrefab is IGameEditorMenuTreeNode node)
                    {
                        if (node.icon.IsNone() == false)
                        {
                            return node.icon;
                        }
                    }
                }
                
                return Icon.None;
            }
        }

        protected override IEnumerable<ToolbarButtonConfig> GetToolbarButtons()
        {
            foreach (var gamePrefab in gamePrefabs)
            {
                if (gamePrefab is IGameEditorToolbarProvider provider)
                {
                    foreach (var config in provider.GetToolbarButtons())
                    {
                        yield return config;
                    }
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