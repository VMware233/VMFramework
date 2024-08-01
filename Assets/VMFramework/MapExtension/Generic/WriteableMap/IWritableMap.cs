using VMFramework.Core;

namespace VMFramework.Maps
{
    public interface IWritableMap<in TPoint, in TTile>
    {
        public void SetTile(TPoint point, TTile tile);

        public void ClearTile(TPoint point);
        
        public void ClearAll();
    }
}