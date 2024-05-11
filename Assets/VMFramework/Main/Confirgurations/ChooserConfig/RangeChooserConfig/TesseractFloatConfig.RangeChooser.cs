using System.Collections.Generic;
using UnityEngine;
using VMFramework.Core;

namespace VMFramework.Configuration
{
    public partial class TesseractFloatConfig : ISingleRangeChooserConfig<Vector4>
    {
        IChooser<Vector4> ISingleChooserConfig<Vector4>.objectChooser { get; set; }
    }
}