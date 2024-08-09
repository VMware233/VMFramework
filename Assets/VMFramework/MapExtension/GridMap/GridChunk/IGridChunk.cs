using System.Collections.Generic;
using UnityEngine;
using VMFramework.Core.Pools;

namespace VMFramework.Maps
{
    public interface IGridChunk
        : IReadOnlyGridChunk, ITileFillableMap<Vector3Int, IGridTile>, ITileReplaceableMap<Vector3Int, IGridTile>,
            ITileDestructibleMap<Vector3Int, IGridTile>, IClearableMap, ICreatablePoolItem<IGridMap>
    {
        public Vector3Int MinTilePosition { get; }

        public IGridMap Map { get; }

        public Vector3Int Size { get; }

        public void Place(GridChunkPlaceInfo info);

        IEnumerable<Vector3Int> IReadableMap<Vector3Int, IGridTile>.GetAllPoints() => Positions;

    }
}