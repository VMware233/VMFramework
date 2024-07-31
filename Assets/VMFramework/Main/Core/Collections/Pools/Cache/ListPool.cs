using System.Collections.Generic;

namespace VMFramework.Core.Pools
{
    public static class ListPool<TItem>
    {
        public static DefaultPool<List<TItem>> Default { get; } = new(new DefaultPoolPolicy<List<TItem>>());

        public static DefaultConcurrentPool<List<TItem>> Shared { get; } = new(new DefaultPoolPolicy<List<TItem>>());
    }
}