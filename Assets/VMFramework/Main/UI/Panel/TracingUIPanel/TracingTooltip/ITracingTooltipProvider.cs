using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using VMFramework.GameLogicArchitecture;
using VMFramework.GlobalEvent;
using VMFramework.Localization;

namespace VMFramework.UI
{
    public interface ITracingTooltipProvider
    {
        public struct PropertyConfig
        {
            public Func<string> attributeValueGetter;
            public bool isStatic;
            public Sprite icon;
            public string groupName;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetTooltipBindGlobalEvent(out GlobalEventConfig globalEvent)
        {
            globalEvent = null;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string GetTooltipID() =>
            GameCoreSettingBase.tracingTooltipGeneralSetting.defaultTracingTooltipID;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool DisplayTooltip() => true;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetTooltipPriority() =>
            GameCoreSettingBase.tracingTooltipGeneralSetting.defaultPriority;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string GetTooltipTitle();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<PropertyConfig> GetTooltipProperties();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string GetTooltipDescription();
    }

    public class TempTracingTooltipProvider : ITracingTooltipProvider
    {
        private LocalizedStringReference title;
        private LocalizedStringReference description;

        public TempTracingTooltipProvider(LocalizedStringReference title,
            LocalizedStringReference description = null)
        {
            this.title = title;
            this.description = description;
        }

        string ITracingTooltipProvider.GetTooltipTitle() => title;

        IEnumerable<ITracingTooltipProvider.PropertyConfig>
            ITracingTooltipProvider.GetTooltipProperties()
        {
            yield break;
        }

        string ITracingTooltipProvider.GetTooltipDescription() => description;
    }
}
