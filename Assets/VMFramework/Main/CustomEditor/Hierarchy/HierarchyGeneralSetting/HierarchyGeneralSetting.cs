#if UNITY_EDITOR

using System.Collections.Generic;
using Newtonsoft.Json;
using Sirenix.OdinInspector;
using VMFramework.GameLogicArchitecture;

namespace VMFramework.Editor
{
    public sealed partial class HierarchyGeneralSetting : GeneralSetting
    {
        private const string HIERARCHY_COLOR_CATEGORY = "Hierarchy Color";
        
        private const string HIERARCHY_COMPONENT_ICON_CATEGORY = "Hierarchy Component Icon";
        
        [TabGroup(TAB_GROUP_NAME, HIERARCHY_COLOR_CATEGORY)]
        [JsonProperty]
        public List<HierarchyColorPreset> colorPresets = new();
        
        [TabGroup(TAB_GROUP_NAME, HIERARCHY_COMPONENT_ICON_CATEGORY)]
        [PropertyRange(1, 10)]
        [JsonProperty]
        public int maxIconNum = 5;

        [TabGroup(TAB_GROUP_NAME, HIERARCHY_COMPONENT_ICON_CATEGORY)]
        [PropertyRange(1, 24)]
        [JsonProperty]
        public int iconSize = 16;
    }
}

#endif