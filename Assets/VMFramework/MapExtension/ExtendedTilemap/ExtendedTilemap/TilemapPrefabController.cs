using UnityEngine;
using UnityEngine.Tilemaps;

namespace VMFramework.Maps
{
    public sealed class TilemapPrefabController : PrefabController<Tilemap>
    {
        public override void SetPrefab(GameObject newPrefab)
        {
            base.SetPrefab(newPrefab);
            
            prefab.ClearAllTiles();
        }
    }
}