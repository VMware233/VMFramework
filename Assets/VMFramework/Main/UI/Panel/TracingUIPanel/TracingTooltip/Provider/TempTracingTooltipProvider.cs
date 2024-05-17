using System.Collections.Generic;
using VMFramework.Localization;

namespace VMFramework.UI
{
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