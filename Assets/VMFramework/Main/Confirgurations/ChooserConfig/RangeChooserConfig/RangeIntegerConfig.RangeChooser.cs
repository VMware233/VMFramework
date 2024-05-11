using System.Collections.Generic;
using VMFramework.Core;

namespace VMFramework.Configuration
{
    public partial class RangeIntegerConfig : ISingleRangeChooserConfig<int>
    {
        IChooser<int> ISingleChooserConfig<int>.objectChooser { get; set; }
    }
}