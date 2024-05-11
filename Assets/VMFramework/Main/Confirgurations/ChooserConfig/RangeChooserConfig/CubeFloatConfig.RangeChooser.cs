using System.Collections.Generic;
using UnityEngine;
using VMFramework.Core;

namespace VMFramework.Configuration
{
    public partial class CubeFloatConfig : ISingleRangeChooserConfig<Vector3>
    {
        IChooser<Vector3> ISingleChooserConfig<Vector3>.objectChooser { get; set; }
    }
}