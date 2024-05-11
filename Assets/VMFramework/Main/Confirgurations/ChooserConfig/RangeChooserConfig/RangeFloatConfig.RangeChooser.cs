using System.Collections.Generic;
using VMFramework.Core;

namespace VMFramework.Configuration
{
    public partial class RangeFloatConfig : ISingleRangeChooserConfig<float>
    {
        IChooser<float> ISingleChooserConfig<float>.objectChooser { get; set; }
    }
}