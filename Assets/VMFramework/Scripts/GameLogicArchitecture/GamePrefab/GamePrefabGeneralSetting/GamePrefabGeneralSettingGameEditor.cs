#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.Core;
using VMFramework.Core.Editor;
using VMFramework.Editor;

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

        protected override IEnumerable<IGameEditorToolBarProvider.ToolbarButtonConfig> GetToolbarButtons()
        {
            yield return new(GameEditorNames.openGamePrefabScriptButtonName, OpenGamePrefabScript);
            
            foreach (var config in base.GetToolbarButtons())
            {
                yield return config;
            }

            yield return new(GameEditorNames.saveAllButtonName, SaveAllGamePrefabs);
        }

        private void OpenGamePrefabScript()
        {
            baseGamePrefabType.OpenScriptOfType();
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

        EditorIconType IGameEditorMenuTreeNode.iconType
        {
            get
            {
                foreach (var gamePrefab in GamePrefabManager.GetAllGamePrefabs(baseGamePrefabType))
                {
                    if (gamePrefab is not IGameEditorMenuTreeNode node)
                    {
                        continue;
                    }

                    switch (node.iconType)
                    {
                        case EditorIconType.Sprite:
                            if (node.spriteIcon != null)
                            {
                                iconGamePrefab = node;
                                return EditorIconType.Sprite;
                            }
                            break;
                        case EditorIconType.SdfIcon:
                            if (node.sdfIcon != SdfIconType.None)
                            {
                                iconGamePrefab = node;
                                return EditorIconType.SdfIcon;
                            }
                            break;
                        case EditorIconType.Texture2D:
                            if (node.texture2DIcon != null)
                            {
                                iconGamePrefab = node;
                                return EditorIconType.Texture2D;
                            }
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }

                return EditorIconType.SdfIcon;
            }
        }

        Sprite IGameEditorMenuTreeNode.spriteIcon => iconGamePrefab?.spriteIcon;

        Texture2D IGameEditorMenuTreeNode.texture2DIcon => iconGamePrefab?.texture2DIcon;

        SdfIconType IGameEditorMenuTreeNode.sdfIcon => iconGamePrefab?.sdfIcon ?? SdfIconType.None;

        #endregion
    }
}
#endif