using System.Diagnostics.CodeAnalysis;

namespace VMFramework.Maps
{
    public interface ITileFillableMap<in TPoint, in TTileInfo>
    {
        public bool FillTile(TPoint position, [NotNull] TTileInfo info);
    }
}