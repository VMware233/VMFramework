#if UNITY_EDITOR
using System.Collections.Generic;
using VMFramework.Core.Editor;
using VMFramework.Editor;
using VMFramework.Editor.GameEditor;

namespace VMFramework.GameLogicArchitecture
{
    public partial class GameSettingBase : IGameEditorToolbarProvider, IGameEditorContextMenuProvider
    {
        protected virtual IEnumerable<ToolbarButtonConfig> GetToolbarButtons()
        {
            yield return new(EditorNames.SELECT_ASSET_BUTTON_PATH, this.SelectObject);
            yield return new(EditorNames.OPEN_ASSET_IN_NEW_INSPECTOR_BUTTON_PATH, this.OpenInNewInspector);
            yield return new(EditorNames.OPEN_THIS_SCRIPT_BUTTON_PATH, this.OpenScriptOfObject);
            yield return new(EditorNames.SAVE_BUTTON, this.EnforceSave);
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
#endif