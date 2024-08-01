using System;
using System.Collections.Generic;
using UnityEngine;
using VMFramework.Core;
using VMFramework.GameLogicArchitecture;

namespace VMFramework.Maps
{
    public delegate void TileChangedHandler(IGridChunk chunk, IGridTile tile);
    
    public class GridChunk : IGridChunk
    {
        public Vector3Int Position { get; private set; }
        
        public Vector3Int MinTilePosition { get; private set; }
        
        public CubeInteger Positions { get; private set; }
        
        public Vector3Int Size { get; private set; }
        
        public IGridMap Map { get; private set; }

        private IGridTile[,,] tiles;

        public event TileChangedHandler OnTileAdded;
        public event TileChangedHandler OnTileRemoved;

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

        public virtual void SetTile(Vector3Int relativePosition, IGridTile tile)
        {
            relativePosition.AssertContainsBy(Positions, nameof(relativePosition), nameof(Positions));

            if (tiles.Remove(relativePosition, out var removedTile))
            {
                OnTileRemoved?.Invoke(this, removedTile);
                
                if (removedTile is IGameItem gameItem)
                {
                    GameItemManager.Return(gameItem);
                }
            }

            if (tile != null)
            {
                tiles.Set(relativePosition, tile);
                tile.Place(new(this, MinTilePosition + relativePosition, relativePosition));
                OnTileAdded?.Invoke(this, tile);
            }
        }

        public virtual void ClearTile(Vector3Int point)
        {
            if (tiles.Remove(point, out var removedTile))
            {
                OnTileRemoved?.Invoke(this, removedTile);
                
                if (removedTile is IGameItem gameItem)
                {
                    GameItemManager.Return(gameItem);
                }
            }
        }

        public void ClearAll()
        {
            foreach (var position in Positions)
            {
                ClearTile(position);
            }
        }
    }
}