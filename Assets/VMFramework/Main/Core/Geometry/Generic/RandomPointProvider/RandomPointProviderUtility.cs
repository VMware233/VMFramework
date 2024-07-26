using System.Collections.Generic;
using System.Runtime.CompilerServices;
using VMFramework.Core.Linq;

namespace VMFramework.Core
{
    public static class RandomPointProviderUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<TPoint> GetRandomPoints<TPoint>(this IRandomPointProvider<TPoint> randomPointProvider,
            int count)
        {
            return count.Repeat(randomPointProvider.GetRandomPoint);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void GetRandomPoints<TPoint, TRandomPointProvider>(this TRandomPointProvider randomPointProvider,
            int count, ref TPoint[] points)
        {
            count.CreateOrResizeArrayWithMinLength(ref points);
        }
    }
}