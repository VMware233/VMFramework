using System.Collections.Generic;
using System.Runtime.CompilerServices;
using VMFramework.Core;
using VMFramework.GameEvents;

namespace VMFramework.UI
{
    public interface ITooltipProvider : IReadOnlyDestructible
    {
        bool IReadOnlyDestructible.isDestroyed => false;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetTooltipBindGlobalEvent(out IGameEvent gameEvent)
        {
            gameEvent = null;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ShowTooltip() => true;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string GetTooltipTitle();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<TooltipPropertyInfo> GetTooltipProperties();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string GetTooltipDescription();
    }
}
