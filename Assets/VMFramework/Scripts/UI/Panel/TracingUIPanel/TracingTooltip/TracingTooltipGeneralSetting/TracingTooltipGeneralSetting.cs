using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using VMFramework.Configuration;
using Newtonsoft.Json;
using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.GameLogicArchitecture;
using VMFramework.Localization;
using VMFramework.OdinExtensions;

namespace VMFramework.UI
{
    public class TooltipPriorityConfig : BaseConfigClass
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
        public const string TOOLTIP_SETTING_CATEGORY = "提示框设置";

        #region Default Tooltip

        [LabelText("默认Tooltip框架"), TitleGroup(TOOLTIP_SETTING_CATEGORY)]
        [JsonProperty, SerializeField]
        private UIFrameworkType defaultTooltipFrameworkType = UIFrameworkType.UGUI;

        [LabelText("默认UIToolkit版本的Tooltip"), TitleGroup(TOOLTIP_SETTING_CATEGORY)]
        [ShowIf(nameof(defaultTooltipFrameworkType), UIFrameworkType.UIToolkit)]
        [GamePrefabIDValueDropdown(typeof(UIToolkitTracingTooltipPreset))]
        [IsNotNullOrEmpty]
        [JsonProperty, SerializeField]
        private string defaultUIToolkitTooltipID;

        [LabelText("默认UGUI版本的Tooltip"), TitleGroup(TOOLTIP_SETTING_CATEGORY)]
        [ShowIf(nameof(defaultTooltipFrameworkType), UIFrameworkType.UGUI)]
        [IsNotNullOrEmpty]
        [JsonProperty, SerializeField]
        private string defaultUGUITooltipID;

        public string defaultTooltipID => defaultTooltipFrameworkType switch
        {
            UIFrameworkType.UGUI => defaultUGUITooltipID,
            UIFrameworkType.UIToolkit => defaultUIToolkitTooltipID,
            _ => throw new ArgumentOutOfRangeException()
        };

        #endregion

        #region Tooltip Priority

        private class TooltipPriorityPreset : BaseConfigClass
        {
            [LabelText("ID")]
            [IsNotNullOrEmpty]
            public string presetID;

            [LabelText("优先级")]
            public int priority;
        }

        [LabelText("Tooltip优先级预设"), TitleGroup(TOOLTIP_SETTING_CATEGORY)]
        [JsonProperty, SerializeField]
        private List<TooltipPriorityPreset> tooltipPriorityPresets = new();

        [LabelText("默认优先级"), TitleGroup(TOOLTIP_SETTING_CATEGORY)]
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
    }
}
