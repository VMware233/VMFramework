using System.Collections.Generic;
using VMFramework.Core.Editor;
using VMFramework.Editor;
using VMFramework.Editor.GameEditor;

namespace VMFramework.GameLogicArchitecture
{
    public partial class GameEditorScriptableObject
        : IGameEditorMenuTreeNode, IGameEditorToolbarProvider, IGameEditorContextMenuProvider
    {
        protected virtual IEnumerable<ToolbarButtonConfig> GetToolbarButtons()
        {
            yield return new(EditorNames.SELECT_ASSET_PATH, this.SelectObject);
            yield return new(EditorNames.DELETE_ASSET_PATH, this.DeleteAssetWithDialog);
            yield return new(EditorNames.OPEN_IN_EXPLORER_PATH, this.OpenInExplorer);
            yield return new(EditorNames.OPEN_ASSET_IN_NEW_INSPECTOR_PATH, this.OpenInNewInspector);
            yield return new(EditorNames.OPEN_THIS_SCRIPT_PATH, this.OpenScriptOfObject);
            yield return new(EditorNames.SELECT_THIS_SCRIPT_PATH, this.SelectScriptOfObject);
            yield return new(EditorNames.SAVE, this.EnforceSave);
        }

        IEnumerable<ToolbarButtonConfig> IGameEditorToolbarProvider.GetToolbarButtons() =>
            GetToolbarButtons();

        protected virtual IEnumerable<MenuItemConfig> GetMenuItems()
        {
            return this.GetMenuItemsFromToolbarProvider();
        }

        IEnumerable<MenuItemConfig> IGameEditorContextMenuProvider.GetMenuItems()
        {
            return GetMenuItems();
        }
    }
}