using System.Collections.Generic;
using UnityEngine;
using VMFramework.Core;

namespace VMFramework.Configuration
{
    public partial class RectangleFloatConfig : ISingleRangeChooserConfig<Vector2>
    {
        IChooser<Vector2> ISingleChooserConfig<Vector2>.objectChooser { get; set; }
    }
}