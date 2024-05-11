using System.Collections.Generic;
using UnityEngine;
using VMFramework.Core;

namespace VMFramework.Configuration
{
    public partial class RectangleIntegerConfig : ISingleRangeChooserConfig<Vector2Int>
    {
        IChooser<Vector2Int> ISingleChooserConfig<Vector2Int>.objectChooser { get; set; }
    }
}