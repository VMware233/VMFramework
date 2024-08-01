using UnityEngine;
using VMFramework.Core.Pools;

namespace VMFramework.Maps
{
    public struct GridChunkPlaceInfo
    {
        public readonly Vector3Int position;
        
        public GridChunkPlaceInfo(Vector3Int position)
        {
            this.position = position;
        }
    }
}