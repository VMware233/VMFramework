using UnityEngine;
using VMFramework.GameLogicArchitecture;

namespace VMFramework.Maps
{
    public class GridTile : VisualGameItem, IGridTile
    {
        public IGridChunk Chunk { get; private set; }
        
        public Vector3Int Position { get; private set; }

        public void Init()
        {
            
        }
    }
}