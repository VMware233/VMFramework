using System;
using Newtonsoft.Json;
using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.Configuration;
using VMFramework.Core;
using VMFramework.GameLogicArchitecture;
using VMFramework.OdinExtensions;

namespace VMFramework.UI
{
    [JsonObject(MemberSerialization.OptIn)]
    [PreviewComposite]
    public struct TooltipPriority
    {
        [JsonProperty, SerializeField]
        private TooltipPriorityType priorityType;

        [TooltipPriorityPresetID]
        [ShowIf(nameof(priorityType), TooltipPriorityType.Preset)]
        [JsonProperty, SerializeField]
        private string presetID;

        [ShowIf(nameof(priorityType), TooltipPriorityType.Custom)]
        [JsonProperty, SerializeField]
        private int priority;

        public TooltipPriority(int priority)
        {
            this.priorityType = TooltipPriorityType.Custom;
            this.priority = priority;
            presetID = null;
        }

        public TooltipPriority(string presetID)
        {
            this.priorityType = TooltipPriorityType.Preset;
            this.presetID = presetID;
            priority = 0;
        }

        public override string ToString()
        {
            return priorityType switch
            {
                TooltipPriorityType.Preset => presetID,
                TooltipPriorityType.Custom => priority.ToString(),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        public int GetPriority()
        {
            switch (priorityType)
            {
                case TooltipPriorityType.Preset:
                    if (presetID.IsNullOrEmpty())
                    {
                        Debug.LogWarning("No Tooltip Priority Preset ID set.");
                        return 0;
                    }
                    
                    if (GameCoreSetting.tooltipGeneralSetting.tooltipPriorityPresets.TryGetConfigRuntime(
                            presetID, out var config))
                    {
                        return config.priority;
                    }
                    
                    Debug.LogWarning($"No Tooltip Priority Preset found with ID: {presetID}");
                    return 0;
                case TooltipPriorityType.Custom:
                    return priority;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public static implicit operator int(TooltipPriority config)
        {
            return config.GetPriority();
        }
    }
}