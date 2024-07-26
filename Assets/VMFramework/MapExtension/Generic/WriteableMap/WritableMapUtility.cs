using System.Runtime.CompilerServices;
using UnityEngine;
using VMFramework.Core;

namespace VMFramework.Maps
{
    public static class WritableMapUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetRectangleTiles<TTile>(this IWritableMap<Vector2Int, TTile> map,
            RectangleInteger rectangle, TTile tile)
        {
            foreach (var point in rectangle)
            {
                map.SetTile(point, tile);
            }
        }
    }
}