using System.Collections.Generic;
using UnityEngine;
using VMFramework.Core.Pools;

namespace VMFramework.Maps
{
    public interface IGridChunk : IVector3IntPositionProvider, ICubeIntegerPositionsProvider, IReadableMap3D<IGridTile>,
        IWritableMap3D<IGridTile>, IPoolItem
    {
        public Vector3Int MinTilePosition { get; }
        
        public IGridMap Map { get; }
        
        public Vector3Int Size { get; }

        public void OnCreate(IGridMap map);

        public void Place(GridChunkPlaceInfo info);

        IEnumerable<Vector3Int> IReadableMap<Vector3Int, IGridTile>.GetAllPoints() => Positions;
    }
}