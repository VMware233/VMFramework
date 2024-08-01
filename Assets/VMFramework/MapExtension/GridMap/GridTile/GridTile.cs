using UnityEngine;
using VMFramework.GameLogicArchitecture;

namespace VMFramework.Maps
{
    public class GridTile : VisualGameItem, IGridTile
    {
        public IGridChunk Chunk { get; private set; }
        
        public Vector3Int Position { get; private set; }
        
        public Vector3Int PositionInChunk { get; private set; }

        protected virtual void OnPlace(GridTilePlaceInfo info)
        {
            Chunk = info.chunk;
            Position = info.position;
            PositionInChunk = info.positionInChunk;
        }
        
        void IGridTile.Place(GridTilePlaceInfo info)
        {
            OnPlace(info);
        }
    }
}