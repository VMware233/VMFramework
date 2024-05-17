using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using VMFramework.GameEvents;
using VMFramework.GameLogicArchitecture;

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
        
        public bool isDestroyed => false;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetTooltipBindGlobalEvent(out IGameEvent gameEvent)
        {
            gameEvent = null;
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
}
