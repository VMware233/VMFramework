using System.Collections.Generic;
using System.Runtime.CompilerServices;
using VMFramework.Core;

namespace VMFramework.Maps
{
    public interface IReadableMap<TPoint, TTile> : IMapping<TPoint, TTile>
    {
        public IEnumerable<TPoint> GetAllPoints();

        public IEnumerable<TTile> GetAllTiles();

        public TTile GetTile(TPoint point);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetTile(TPoint point, out TTile tile)
        {
            tile = GetTile(point);
            return tile != null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsTile(TPoint point)
        {
            return GetTile(point) != null;
        }

        TTile IMapping<TPoint, TTile>.Map(TPoint point) => GetTile(point);
    }
}