using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using UnityEngine;
using VMFramework.Core;

namespace VMFramework.Maps
{
    public class GridChunk : IGridChunk
    {
        public Vector3Int Position { get; private set; }
        
        public Vector3Int MinTilePosition { get; private set; }
        
        public CubeInteger Positions { get; private set; }
        
        public Vector3Int Size { get; private set; }
        
        public IGridMap Map { get; private set; }

        private IGridTile[,,] tiles;

        public void OnCreate(IGridMap map)
        {
            Map = map;
            Size = Map.ChunkSize;
            Size.CreateArray(ref tiles);
        }

        public void Place(GridChunkPlaceInfo info)
        {
            Position = info.position;
            
            MinTilePosition = Map.ChunkSize * Position;
            Positions = new(MinTilePosition, MinTilePosition + Map.ChunkSize - Vector3Int.one);
        }

        public IEnumerable<IGridTile> GetAllTiles()
        {
            foreach (var tile in tiles)
            {
                if (tile != null)
                {
                    yield return tile;
                }
            }
        }

        public IGridTile GetTile(Vector3Int relativePosition)
        {
            relativePosition.AssertContainsBy(Positions, nameof(relativePosition), nameof(Positions));

            return tiles.Get(relativePosition);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool DestructTileWithoutChecking(Vector3Int relativePosition, out IGridTile tile)
        {
            if (tiles.Remove(relativePosition, out tile) == false)
            {
                return false;
            }
                
            return true;
        }

        public bool FillTile(Vector3Int relativePosition, [NotNull] IGridTile tile)
        {
            tile.AssertIsNotNull(nameof(tile));
            relativePosition.AssertContainsBy(Positions, nameof(relativePosition), nameof(Positions));

            if (tiles.TrySet(relativePosition, tile) == false)
            {
                return false;
            }
            
            tile.InitGridTileInfo(new(this, MinTilePosition + relativePosition, relativePosition));
            return true;
        }

        public void ReplaceTile(Vector3Int relativePosition, IGridTile tile)
        {
            relativePosition.AssertContainsBy(Positions, nameof(relativePosition), nameof(Positions));
            
            if (tile == null)
            {
                DestructTileWithoutChecking(relativePosition, out _);
                return;
            }

            DestructTileWithoutChecking(relativePosition, out _);
            
            tiles.Set(relativePosition, tile);
            tile.InitGridTileInfo(new(this, MinTilePosition + relativePosition, relativePosition));
        }

        public bool DestructTile(Vector3Int relativePosition, out IGridTile tile)
        {
            relativePosition.AssertContainsBy(Positions, nameof(relativePosition), nameof(Positions));
            
            return DestructTileWithoutChecking(relativePosition, out tile);
        }

        public void ClearMap()
        {
            foreach (var position in Positions)
            {
                DestructTileWithoutChecking(position, out _);
            }
        }
    }
}