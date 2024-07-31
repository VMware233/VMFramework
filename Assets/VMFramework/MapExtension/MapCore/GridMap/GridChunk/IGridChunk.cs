using System.Collections.Generic;
using UnityEngine;

namespace VMFramework.Maps
{
    public interface IGridChunk : IVector3IntPositionProvider, ICubeIntegerPositionsProvider, IReadableMap3D<IGridTile>,
        IWritableMap3D<IGridTile>
    {
        public Vector3Int MinTilePosition { get; }
        
        public IGridMap Map { get; }

        public void Init(GridChunkInitializationInfo info);

        public void Destroy();

        IEnumerable<Vector3Int> IReadableMap<Vector3Int, IGridTile>.GetAllPoints() => Positions;
    }
}