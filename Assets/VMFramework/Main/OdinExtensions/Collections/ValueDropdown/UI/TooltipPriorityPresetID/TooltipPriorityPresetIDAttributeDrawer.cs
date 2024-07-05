#if UNITY_EDITOR
using System.Collections.Generic;
using Sirenix.OdinInspector;
using VMFramework.Configuration;
using VMFramework.UI;

namespace VMFramework.OdinExtensions
{
    public sealed class TooltipPriorityPresetIDAttributeDrawer
        : GeneralValueDropdownAttributeDrawer<TooltipPriorityPresetIDAttribute>
    {
        protected override IEnumerable<ValueDropdownItem> GetValues()
        {
            return UISetting.tooltipGeneralSetting.tooltipPriorityPresets.GetNameList();
        }
    }
}
#endif