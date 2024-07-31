using System.Runtime.CompilerServices;
using UnityEngine;
using VMFramework.Core;

namespace VMFramework.Maps
{
    public static class GridMapChunkUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int GetChunkPosition<TMap>(this TMap map, Vector3Int tilePosition) where TMap : IGridMap
        {
            return tilePosition.Divide(map.ChunkSize);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGridChunk GetChunkByTilePosition<TMap>(this TMap map, Vector3Int tilePosition)
            where TMap : IGridMap
        {
            return map.GetChunk(map.GetChunkPosition(tilePosition));
        }
    }
}