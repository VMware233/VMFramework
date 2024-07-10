#if UNITY_EDITOR
using System.Collections.Generic;
using VMFramework.Core.Editor;
using VMFramework.Editor;
using VMFramework.Editor.GameEditor;
using VMFramework.GameLogicArchitecture.Editor;

namespace VMFramework.GameLogicArchitecture
{
    public partial class GamePrefabGeneralSetting : IGameEditorMenuTreeNodesProvider, IGameEditorMenuTreeNode
    {
        bool IGameEditorMenuTreeNodesProvider.isMenuTreeNodesVisible => true;

        IEnumerable<IGameEditorMenuTreeNode> IGameEditorMenuTreeNodesProvider.GetAllMenuTreeNodes()
        {
            return initialGamePrefabWrappers;
        }

        #region Toolbar

        protected override IEnumerable<ToolbarButtonConfig> GetToolbarButtons()
        {
            yield return new(EditorNames.OPEN_GAME_PREFAB_SCRIPT_PATH, OpenGamePrefabScript);
            yield return new(EditorNames.OPEN_INITIAL_GAME_PREFABS_SCRIPTS_PATH, OpenInitialGamePrefabScripts);

            if (initialGamePrefabWrappers.HasAnyGameItem())
            {
                yield return new(EditorNames.OPEN_GAME_ITEMS_OF_INITIAL_GAME_PREFABS_SCRIPTS_PATH,
                    OpenGameItemsOfInitialGamePrefabsScripts);
            }
            
            foreach (var config in base.GetToolbarButtons())
            {
                yield return config;
            }

            yield return new(EditorNames.SAVE_ALL, SaveAllGamePrefabs);
        }

        private void OpenGamePrefabScript()
        {
            baseGamePrefabType.OpenScriptOfType();
        }

        private void OpenInitialGamePrefabScripts()
        {
            foreach (var wrapper in initialGamePrefabWrappers)
            {
                wrapper.OpenGamePrefabScripts();
            }
        }

        private void OpenGameItemsOfInitialGamePrefabsScripts()
        {
            foreach (var wrapper in initialGamePrefabWrappers)
            {
                wrapper.OpenGameItemScripts();
            }
        }

        private void SaveAllGamePrefabs()
        {
            this.EnforceSave();
            
            foreach (var wrapper in initialGamePrefabWrappers)
            {
                wrapper.SetEditorDirty();
            }
        }

        #endregion

        #region Icon

        private IGameEditorMenuTreeNode iconGamePrefab;

        Icon IGameEditorMenuTreeNode.icon
        {
            get
            {
                foreach (var gamePrefab in GamePrefabManager.GetAllGamePrefabs(baseGamePrefabType))
                {
                    if (gamePrefab is not IGameEditorMenuTreeNode node)
                    {
                        continue;
                    }

                    if (node.icon.IsNull() == false)
                    {
                        iconGamePrefab = node;
                        return node.icon;
                    }
                }
                
                return Icon.None;
            }
        }

        #endregion
    }
}
#endif