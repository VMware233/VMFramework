#if UNITY_EDITOR && ODIN_INSPECTOR
using Sirenix.OdinInspector;

namespace VMFramework.Core
{
    public partial class WeightedSelectChooser<TItem>
    {
        [ShowInInspector]
        private (TItem item, float weight)[] _infos => infos;
    }
}
#endif