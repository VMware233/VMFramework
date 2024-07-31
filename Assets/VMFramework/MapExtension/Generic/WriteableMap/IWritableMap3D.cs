using System.Runtime.CompilerServices;
using UnityEngine;
using VMFramework.Core;

namespace VMFramework.Maps
{
    public interface IWritableMap3D<in TTile> : IWritableMap<Vector3Int, TTile>
    {
        public void ClearCubeTiles(Vector3Int start, Vector3Int end)
        {
            foreach (var point in start.GetCube(end))
            {
                ClearTile(point);
            }
        }
        
        public void SetCubeTiles(Vector3Int start, Vector3Int end, TTile tile)
        {
            foreach (var point in start.GetCube(end))
            {
                SetTile(point, tile);
            }
        }
    }
}