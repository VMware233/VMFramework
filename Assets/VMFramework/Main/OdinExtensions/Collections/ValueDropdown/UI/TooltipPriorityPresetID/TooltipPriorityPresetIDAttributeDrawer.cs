#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.Configuration;
using VMFramework.GameLogicArchitecture;

namespace VMFramework.OdinExtensions
{
    public sealed class TooltipPriorityPresetIDAttributeDrawer
        : GeneralValueDropdownAttributeDrawer<TooltipPriorityPresetIDAttribute>
    {
        protected override IEnumerable<ValueDropdownItem> GetValues()
        {
            return GameCoreSetting.tooltipGeneralSetting.tooltipPriorityPresets.GetNameList();
        }
    }
}
#endif