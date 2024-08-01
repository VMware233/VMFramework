using System.Runtime.CompilerServices;
using VMFramework.Core;

namespace VMFramework.Maps
{
    public static class MapUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool FillTile<TMap, TPoint, TTile>(this TMap map, TPoint point, TTile tile)
            where TMap : IReadableMap<TPoint, TTile>, IWritableMap<TPoint, TTile>
        {
            if (map.ContainsTile(point))
            {
                return false;
            }
            
            map.SetTile(point, tile);
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void FillCubeTiles<TMap, TTile>(this TMap map, CubeInteger cube, TTile tile)
            where TMap : IReadableMap3D<TTile>, IWritableMap3D<TTile>
        {
            foreach (var point in cube)
            {
                if (map.ContainsTile(point))
                {
                    continue;
                }
                
                map.SetTile(point, tile);
            }
        }
    }
}