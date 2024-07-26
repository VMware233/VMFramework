using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using VMFramework.Core;

namespace VMFramework.Maps
{
    public static class ReadableMap2DUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<TTile> GetRectangleTiles<TTile>(this IReadableMap2D<TTile> map, Vector2Int start,
            Vector2Int end)
        {
            foreach (var point in start.GetRectangle(end))
            {
                yield return map.GetTile(point);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<TTile> GetRectangleTiles<TTile>(this IReadableMap2D<TTile> map,
            RectangleInteger rectangle)
        {
            foreach (var point in rectangle)
            {
                yield return map.GetTile(point);
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<TTile> GetValidRectangleTiles<TTile>(this IReadableMap2D<TTile> map, Vector2Int start,
            Vector2Int end)
        {
            foreach (var point in start.GetRectangle(end))
            {
                if (map.TryGetTile(point, out var tile))
                {
                    yield return tile;
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<TTile> GetValidRectangleTiles<TTile>(this IReadableMap2D<TTile> map,
            RectangleInteger rectangle)
        {
            foreach (var point in rectangle)
            {
                if (map.TryGetTile(point, out var tile))
                {
                    yield return tile;
                }
            }
        }
    }
}