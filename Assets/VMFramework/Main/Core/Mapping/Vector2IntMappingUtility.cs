using System.Runtime.CompilerServices;
using UnityEngine;

namespace VMFramework.Core
{
    public static class Vector2IntMappingUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static FourDirectionsNeighbors<TResult> GetFourDirectionsNeighbors<TResult>(
            this IMapping<Vector2Int, TResult> mapping, Vector2Int point)
        {
            return point.GetFourDirectionsNeighbors().Map(mapping.Map);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EightDirectionsNeighbors<TResult> GetEightDirectionsNeighbors<TResult>(
            this IMapping<Vector2Int, TResult> mapping, Vector2Int point)
        {
            return point.GetEightDirectionsNeighbors().Map(mapping.Map);
        }
    }
}