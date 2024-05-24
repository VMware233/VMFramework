#if UNITY_EDITOR && ODIN_INSPECTOR
using Sirenix.OdinInspector;

namespace VMFramework.Core.Pool
{
    public partial class ComponentLimitPool<TComponent>
    {
        [ShowInInspector]
        public int maxCapacityDebug => maxCapacity;
    }
}
#endif