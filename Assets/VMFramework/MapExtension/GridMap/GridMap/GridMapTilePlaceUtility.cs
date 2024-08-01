using System.Runtime.CompilerServices;
using UnityEngine;

namespace VMFramework.Maps
{
    public static class GridMapTilePlaceUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetTile(this IGridMap map, Vector3Int tilePosition, IGridTile tile)
        {
            map.GetChunkPositionAndRelativePosition(tilePosition, out var chunkPosition, out var relativePosition);

            var chunk = map.GetOrCreateChunk(chunkPosition);
            chunk.SetTile(relativePosition, tile);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ClearTile(this IGridMap map, Vector3Int tilePosition)
        {
            map.GetChunkPositionAndRelativePosition(tilePosition, out var chunkPosition, out var relativePosition);

            if (map.TryGetChunk(chunkPosition, out var chunk) == false)
            {
                return;
            }

            chunk.ClearTile(relativePosition);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool FillTile(this IGridMap map, Vector3Int tilePosition, IGridTile tile)
        {
            map.GetChunkPositionAndRelativePosition(tilePosition, out var chunkPosition, out var relativePosition);

            if (map.TryGetChunk(chunkPosition, out var chunk) == false)
            {
                chunk = map.CreateChunk(chunkPosition);
                chunk.SetTile(relativePosition, tile);
                return true;
            }
            
            return chunk.FillTile(relativePosition, tile);
        }
    }
}