using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace VMFramework.Core
{
    public static class RandomPointProviderUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TPoint GetRandomPoint<TPoint>(this IRandomPointProvider<TPoint> randomPointProvider)
        {
            return randomPointProvider.GetRandomPoint(GlobalRandom.Default);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<TPoint> GetRandomPoints<TPoint>(this IRandomPointProvider<TPoint> randomPointProvider,
            int count)
        {
            for (int i = 0; i < count; i++)
            {
                yield return randomPointProvider.GetRandomPoint();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void GetRandomPoints<TPoint, TRandomPointProvider>(this TRandomPointProvider randomPointProvider,
            int count, ref TPoint[] points, Random random) where TRandomPointProvider : IRandomPointProvider<TPoint>
        {
            count.CreateOrResizeArrayWithMinLength(ref points);
            
            for (int i = 0; i < count; i++)
            {
                points[i] = randomPointProvider.GetRandomPoint(random);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void GetRandomPoints<TPoint, TRandomPointProvider>(this TRandomPointProvider randomPointProvider,
            int count, ref TPoint[] points)
            where TRandomPointProvider : IRandomPointProvider<TPoint>
        {
            randomPointProvider.GetRandomPoints(count, ref points, GlobalRandom.Default);
        }
    }
}