#if UNITY_EDITOR && ODIN_INSPECTOR
using System.Collections.Concurrent;
using Sirenix.OdinInspector;

namespace VMFramework.Core.Pools
{
    public partial class DefaultConcurrentPool<TItem>
    {
        [ShowInInspector]
        private ConcurrentQueue<TItem> _itemsOnInspector => _items;
    }
}
#endif