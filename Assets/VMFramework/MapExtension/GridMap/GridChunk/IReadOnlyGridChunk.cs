using UnityEngine;
using VMFramework.Core.Pools;

namespace VMFramework.Maps
{
    public interface IReadOnlyGridChunk
        : IVector3IntPositionProvider, ICubeIntegerPositionsProvider, IReadableMap<Vector3Int, IGridTile>
    {

    }
}