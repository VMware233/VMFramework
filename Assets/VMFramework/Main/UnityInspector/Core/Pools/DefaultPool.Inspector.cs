#if UNITY_EDITOR && ODIN_INSPECTOR
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace VMFramework.Core.Pools
{
    public partial class DefaultPool<TItem>
    {
        [ShowInInspector]
        private Queue<TItem> itemsOnInspector => _items;
    }
}
#endif