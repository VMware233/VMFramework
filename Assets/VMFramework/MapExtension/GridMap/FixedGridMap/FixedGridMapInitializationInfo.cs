using VMFramework.Core.Pools;

namespace VMFramework.Maps
{
    public class FixedGridMapInitializationInfo
    {
        public IPool<IGridChunk> ChunkPool { get; init; }
    }
}