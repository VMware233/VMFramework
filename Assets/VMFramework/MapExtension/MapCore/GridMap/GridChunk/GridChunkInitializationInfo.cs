using UnityEngine;
using VMFramework.Core.Pools;

namespace VMFramework.Maps
{
    public struct GridChunkInitializationInfo
    {
        public readonly IGridMap map;
        public readonly Vector3Int position;
        
        public GridChunkInitializationInfo(IGridMap map, Vector3Int position)
        {
            this.map = map;
            this.position = position;
        }
    }
}