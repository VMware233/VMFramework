using System;
using System.Collections.Generic;
using UnityEngine;
using VMFramework.Core;

namespace VMFramework.Configuration
{
    public partial class ColorRangeConfig : ISingleRangeChooserConfig<Color>
    {
        IChooser<Color> ISingleChooserConfig<Color>.objectChooser { get; set; }
        
    }
}