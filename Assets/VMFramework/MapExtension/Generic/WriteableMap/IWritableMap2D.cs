using System.Runtime.CompilerServices;
using UnityEngine;
using VMFramework.Core;

namespace VMFramework.Maps
{
    public interface IWritableMap2D<in TTile> : IWritableMap<Vector2Int, TTile>
    {
        public void ClearRectangleTiles(Vector2Int start, Vector2Int end);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetRectangleTiles(Vector2Int start, Vector2Int end, TTile tile)
        {
            foreach (var point in start.GetRectangle(end))
            {
                SetTile(point, tile);
            }
        }
    }
}