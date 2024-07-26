using System.Runtime.CompilerServices;

namespace VMFramework.Maps
{
    public static class ReadableMapUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetTile<TPoint, TTile>(this IReadableMap<TPoint, TTile> map, TPoint point, out TTile tile)
        {
            return map.TryGetTile(point, out tile);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ContainsTile<TPoint, TTile>(this IReadableMap<TPoint, TTile> map, TPoint point)
        {
            return map.ContainsTile(point);
        }
    }
}