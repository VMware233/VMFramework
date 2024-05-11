using System.Collections.Generic;
using UnityEngine;
using VMFramework.Core;

namespace VMFramework.Configuration
{
    public partial class CubeIntegerConfig : ISingleRangeChooserConfig<Vector3Int>
    {
        IChooser<Vector3Int> ISingleChooserConfig<Vector3Int>.objectChooser { get; set; }
    }
}