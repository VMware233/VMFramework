#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;
using VMFramework.Core.Editor;
using VMFramework.Editor;

namespace VMFramework.GameLogicArchitecture
{
    public partial class GamePrefabSingleWrapper : IGameEditorMenuTreeNode, IGameEditorToolBarProvider
    {
        Sprite IGameEditorMenuTreeNode.spriteIcon
        {
            get
            {
                if (gamePrefab is IGameEditorMenuTreeNode node)
                {
                    return node.spriteIcon;
                }
                
                return null;
            }
        }

        protected override IEnumerable<IGameEditorToolBarProvider.ToolbarButtonConfig> GetToolbarButtons()
        {
            if (gamePrefab != null)
            {
                yield return new(GameEditorNames.openGamePrefabScriptButtonName, OpenScriptOfGamePrefab);
            }

            if (gamePrefab?.gameItemType != null)
            {
                yield return new(GameEditorNames.openGameItemScriptButtonName, OpenScriptOfGameItem);
            }
            
            foreach (var config in base.GetToolbarButtons())
            {
                yield return config;
            }
        }

        private void OpenScriptOfGamePrefab()
        {
            gamePrefab?.OpenScriptOfObject();
        }

        private void OpenScriptOfGameItem()
        {
            gamePrefab?.gameItemType?.OpenScriptOfType();
        }
    }
}
#endif