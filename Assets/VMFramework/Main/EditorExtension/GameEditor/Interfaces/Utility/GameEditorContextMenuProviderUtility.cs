#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace VMFramework.Editor.GameEditor
{
    public static class GameEditorContextMenuProviderUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<MenuItemConfig> GetMenuItemsFromToolbarProvider(
            this IGameEditorToolbarProvider toolbarProvider)
        {
            return toolbarProvider.GetToolbarButtons().Select(MenuItemConfigUtility.ToMenuItemConfig);
        }
    }
}
#endif