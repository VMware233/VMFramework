#if UNITY_EDITOR && ODIN_INSPECTOR
using Sirenix.OdinInspector;

namespace VMFramework.Core.Pool
{
    public partial class ComponentReadOnlyLimitedCollectionPool<T, TCollection>
    {
        [ShowInInspector]
        private TCollection poolDebug => pool;
    }
}
#endif