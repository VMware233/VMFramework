using System.Collections;
using System.Collections.Generic;

namespace VMFramework.Maps
{
    public sealed partial class MapCore<TChunk, TTile>
    {
        public partial class Chunk : IEnumerable<TTile>
        {
            public IEnumerator<TTile> GetEnumerator()
            {
                foreach (var tile in tiles)
                {
                    yield return tile;
                }
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }
    }
}