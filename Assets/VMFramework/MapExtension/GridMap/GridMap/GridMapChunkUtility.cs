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
            return tilePosition.CircularDivide(map.ChunkSize);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void GetChunkPositionAndRelativePosition<TMap>(this TMap map, Vector3Int tilePosition,
            out Vector3Int chunkPosition, out Vector3Int relativePosition) where TMap : IGridMap
        {
            chunkPosition = map.GetChunkPosition(tilePosition);
            relativePosition = tilePosition - chunkPosition * map.ChunkSize;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGridChunk GetChunkByTilePosition<TMap>(this TMap map, Vector3Int tilePosition)
            where TMap : IGridMap
        {
            return map.GetChunk(map.GetChunkPosition(tilePosition));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGridChunk GetOrCreateChunk<TMap>(this TMap map, Vector3Int chunkPosition)
            where TMap : IGridMap
        {
            if (map.TryGetChunk(chunkPosition, out var chunk))
            {
                return chunk;
            }

            chunk = map.CreateChunk(chunkPosition);
            
            return chunk;
        }
    }
}