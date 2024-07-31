using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VMFramework.Core;

namespace VMFramework.Maps
{
    public class GridChunk : IGridChunk
    {
        public Vector3Int Position { get; private set; }
        
        public Vector3Int MinTilePosition { get; private set; }
        
        public CubeInteger Positions { get; private set; }
        
        public IGridMap Map { get; private set; }

        private IGridTile[,,] tiles;

        public void Init(GridChunkInitializationInfo info)
        {
            Map = info.map;
            Position = info.position;
            
            MinTilePosition = Map.ChunkSize * Position;
            Positions = new CubeInteger(MinTilePosition, MinTilePosition + Map.ChunkSize - Vector3Int.one);
            
            Map.ChunkSize.TryCreateArray(ref tiles);
        }

        public virtual void Destroy()
        {
            
        }

        public IEnumerable<IGridTile> GetAllTiles() => tiles.Cast<IGridTile>();

        public IGridTile GetTile(Vector3Int relativePosition)
        {
            relativePosition.AssertContainsBy(Positions, nameof(relativePosition), nameof(Positions));

            return tiles.Get(relativePosition);
        }

        public void SetTile(Vector3Int relativePosition, IGridTile tile)
        {
            relativePosition.AssertContainsBy(Positions, nameof(relativePosition), nameof(Positions));

            if (tile == null)
            {
                tiles.Remove(relativePosition, out var removedTile);
            }
            tiles.Set(relativePosition, tile);
        }

        public void ClearTile(Vector3Int point)
        {
            
        }

        public void ClearMap()
        {
            throw new System.NotImplementedException();
        }
    }
}