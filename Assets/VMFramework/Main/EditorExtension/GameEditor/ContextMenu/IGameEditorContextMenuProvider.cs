#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;

namespace VMFramework.Editor.GameEditor
{
    public interface IGameEditorContextMenuProvider
    {
        public IEnumerable<MenuItemConfig> GetMenuItems()
        {
            if (this is IGameEditorToolbarProvider toolbarProvider)
            {
                return toolbarProvider.GetMenuItemsFromToolbarProvider();
            }
            
            return Enumerable.Empty<MenuItemConfig>();
        }
    }
}
#endif