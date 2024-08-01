using UnityEngine;

namespace VMFramework.Maps
{
    public interface IFixedGridMap : IGridMap
    {
        public Vector3Int FixedSize { get; }
    }
}