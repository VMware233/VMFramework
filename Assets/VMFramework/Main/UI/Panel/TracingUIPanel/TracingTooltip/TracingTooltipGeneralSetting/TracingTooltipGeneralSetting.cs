using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using VMFramework.Configuration;
using Newtonsoft.Json;
using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.Core;
using VMFramework.GameLogicArchitecture;
using VMFramework.OdinExtensions;

namespace VMFramework.UI
{
    public class TooltipPriorityConfig : BaseConfig
    {
        private enum TooltipPriorityType
        {
            [LabelText("预设")]
            Preset,
            [LabelText("自定义")]
            Custom
        }

        [LabelText("优先级类型")]
        [JsonProperty, SerializeField]
        private TooltipPriorityType priorityType;

        [LabelText("优先级预设")]
        [ValueDropdown("@GameCoreSettingBase.tracingTooltipGeneralSetting." +
                       "GetTooltipPriorityPresetsID()")]
        [ShowIf(nameof(priorityType), TooltipPriorityType.Preset)]
        [JsonProperty, SerializeField]
        private string presetID;

        [LabelText("优先级")]
        [ShowIf(nameof(priorityType), TooltipPriorityType.Custom)]
        [JsonProperty, SerializeField]
        private int priority;

        public int GetPriority()
        {
            return priorityType switch
            {
                TooltipPriorityType.Preset => GameCoreSettingBase
                    .tracingTooltipGeneralSetting.GetTooltipPriority(presetID),
                TooltipPriorityType.Custom => priority,
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        public static implicit operator int(TooltipPriorityConfig config)
        {
            return config.GetPriority();
        }
    }

    public sealed partial class TracingTooltipGeneralSetting : GeneralSettingBase
    {
        public const string TOOLTIP_CATEGORY = "提示框设置";

        #region Default Tooltip

        [LabelText("默认的追踪提示框"), TabGroup(TAB_GROUP_NAME, TOOLTIP_CATEGORY)]
        [GamePrefabID(typeof(UIToolkitTracingTooltipPreset))]
        [IsNotNullOrEmpty]
        [JsonProperty, SerializeField]
        public string defaultTracingTooltipID;

        #endregion

        #region Tooltip Priority

        private class TooltipPriorityPreset : BaseConfig
        {
            [LabelText("ID")]
            [IsNotNullOrEmpty]
            public string presetID;

            [LabelText("优先级")]
            public int priority;
        }

        [LabelText("Tooltip优先级预设"), TabGroup(TAB_GROUP_NAME, TOOLTIP_CATEGORY)]
        [JsonProperty, SerializeField]
        private List<TooltipPriorityPreset> tooltipPriorityPresets = new();

        [LabelText("默认优先级"), TabGroup(TAB_GROUP_NAME, TOOLTIP_CATEGORY)]
        public TooltipPriorityConfig defaultPriority = new();

        public IEnumerable GetTooltipPriorityPresetsID()
        {
            return tooltipPriorityPresets.Select(preset => preset.presetID);
        }

        public int GetTooltipPriority(string presetID)
        {
            foreach (var preset in tooltipPriorityPresets)
            {
                if (preset.presetID == presetID)
                {
                    return preset.priority;
                }
            }
            
            Debug.LogWarning("未找到Tooltip优先级预设：" + presetID);
            return 0;
        }

        #endregion

        public override void CheckSettings()
        {
            base.CheckSettings();
            
            defaultTracingTooltipID.AssertIsNotNullOrEmpty(nameof(defaultTracingTooltipID));
        }
    }
}
