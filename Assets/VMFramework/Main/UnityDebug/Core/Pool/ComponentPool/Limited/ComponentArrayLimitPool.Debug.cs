#if UNITY_EDITOR && ODIN_INSPECTOR
using Sirenix.OdinInspector;

namespace VMFramework.Core.Pool
{
    public partial class ComponentArrayLimitPool<TComponent>
    {
        [ShowInInspector]
        private TComponent[] poolDebug => pool;
    }
}
#endif