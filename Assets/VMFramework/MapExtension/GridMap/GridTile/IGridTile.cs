namespace VMFramework.Maps
{
    public interface IGridTile : IVector3IntPositionProvider
    {
        public IGridChunk Chunk { get; }

        public void Place(GridTilePlaceInfo info);
    }
}