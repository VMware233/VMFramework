using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using VMFramework.Core;

namespace VMFramework.Maps
{
    public static class ReadableMap3DUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<TTile> GetCubeTiles<TTile>(this IReadableMap3D<TTile> map, Vector3Int start,
            Vector3Int end)
        {
            foreach (var point in start.GetCube(end))
            {
                yield return map.GetTile(point);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<TTile> GetCubeTiles<TTile>(this IReadableMap3D<TTile> map,
            CubeInteger rectangle)
        {
            foreach (var point in rectangle)
            {
                yield return map.GetTile(point);
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<TTile> GetValidCubeTiles<TTile>(this IReadableMap3D<TTile> map, Vector3Int start,
            Vector3Int end)
        {
            foreach (var point in start.GetCube(end))
            {
                if (map.TryGetTile(point, out var tile))
                {
                    yield return tile;
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<TTile> GetValidCubeTiles<TTile>(this IReadableMap3D<TTile> map,
            CubeInteger rectangle)
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