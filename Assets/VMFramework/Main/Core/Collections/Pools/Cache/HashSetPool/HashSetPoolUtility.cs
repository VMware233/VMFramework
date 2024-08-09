using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace VMFramework.Core.Pools
{
    public static class HashSetPoolUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static HashSet<T> ToHashSetPooled<T>(this IEnumerable<T> enumerable)
        {
            var hashSet = HashSetPool<T>.Shared.Get();
            hashSet.Clear();
            hashSet.UnionWith(enumerable);
            return hashSet;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static HashSet<T> ToHashSetThreadUnsafePooled<T>(this IEnumerable<T> enumerable)
        {
            var hashSet = HashSetPool<T>.Default.Get();
            hashSet.Clear();
            hashSet.UnionWith(enumerable);
            return hashSet;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ReturnToPool<T>(this HashSet<T> hashSet) => HashSetPool<T>.Shared.Return(hashSet);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ReturnToThreadUnsafePool<T>(this HashSet<T> hashSet) =>
            HashSetPool<T>.Default.Return(hashSet);
    }
}