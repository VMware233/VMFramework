#if UNITY_EDITOR
using System;

namespace VMFramework.Editor.GameEditor
{
    public struct MenuItemConfig
    {
        public string name;
        public string tooltip;
        public Action onClick;
            
        public MenuItemConfig(string name, Action onClick)
        {
            this.name = name;
            this.tooltip = name;
            this.onClick = onClick;
        }
            
        public MenuItemConfig(string name, string tooltip, Action onClick)
        {
            this.name = name;
            this.tooltip = tooltip;
            this.onClick = onClick;
        }
    }
}
#endif