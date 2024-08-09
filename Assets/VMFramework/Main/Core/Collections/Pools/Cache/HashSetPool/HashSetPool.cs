using System.Collections.Generic;

namespace VMFramework.Core.Pools
{
    public static class HashSetPool<TItem>
    {
        public static DefaultPool<HashSet<TItem>> Default { get; } = new(new DefaultPoolPolicy<HashSet<TItem>>());

        public static DefaultConcurrentPool<HashSet<TItem>> Shared { get; } = new(new DefaultPoolPolicy<HashSet<TItem>>());
    }
}