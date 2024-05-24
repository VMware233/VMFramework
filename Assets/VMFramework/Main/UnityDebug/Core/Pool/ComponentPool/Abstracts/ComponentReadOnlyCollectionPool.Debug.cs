#if UNITY_EDITOR && ODIN_INSPECTOR
using Sirenix.OdinInspector;

namespace VMFramework.Core.Pool
{
    public partial class ComponentReadOnlyCollectionPool<TComponent, TCollection>
    {
        [ShowInInspector]
        private TCollection poolDebug => pool;
    }
}
#endif