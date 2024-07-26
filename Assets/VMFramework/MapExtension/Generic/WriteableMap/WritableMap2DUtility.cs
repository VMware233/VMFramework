using System.Runtime.CompilerServices;
using VMFramework.Core;

namespace VMFramework.Maps
{
    public static class WritableMap2DUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetRectangleTiles<TTile>(this IWritableMap2D<TTile> map, RectangleInteger rectangle,
            TTile tile)
        {
            map.SetRectangleTiles(rectangle.min, rectangle.max, tile);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetRandomRectangleTiles<TTile>(this IWritableMap2D<TTile> map, RectangleInteger rectangle,
            TTile tile, int count)
        {
            foreach (var pos in rectangle.GetRandomPoints(count))
            {
                map.SetTile(pos, tile);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetRandomRectangleTiles<TTile>(this IWritableMap2D<TTile> map, RectangleInteger rectangle,
            TTile tile, float ratio)
        {
            foreach (var pos in rectangle.GetRandomPoints((ratio * rectangle.Count).Round()))
            {
                map.SetTile(pos, tile);
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ClearRectangleTiles<TTile>(this IWritableMap2D<TTile> map, RectangleInteger rectangle)
        {
            map.ClearRectangleTiles(rectangle.min, rectangle.max);
        }
    }
}