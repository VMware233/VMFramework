namespace VMFramework.Maps
{
    public interface ITileReplaceableMap<in TPoint, in TTileInfo>
    {
        public void ReplaceTile(TPoint position, TTileInfo info);
    }
}