using VMFramework.Core;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace VMFramework.Maps
{
    public sealed class TilemapGroupController : MonoBehaviour
    {
        #region Base Order

        [field: SerializeField]
        public short baseOrder { get; private set; } = 0;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetBaseOrder(short order)
        {
            baseOrder = order;
        }

        #endregion
        
        [ShowInInspector]
        private readonly Dictionary<int, Tilemap> allTilemaps = new();
        
        [field: SerializeField]
        public TilemapPrefabController tilemapPrefabController { get; private set; }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetTilemapPrefabController(TilemapPrefabController controller)
        {
            if (tilemapPrefabController != null)
            {
                Debug.LogWarning($"{nameof(TilemapPrefabController)} has already been set.");
            }
            
            tilemapPrefabController = controller;
        }

        #region Get Tilemap

        public Tilemap GetTilemap(int layer)
        {
            if (allTilemaps.TryGetValue(layer, out var tilemap))
            {
                return tilemap;
            }

            var go = Instantiate(tilemapPrefabController.prefabObject, transform);

            go.SetActive(true);
            
            go.name = $"Tilemap_{layer}";

            tilemap = go.transform.QueryFirstComponentInChildren<Tilemap>(true);

            var tilemapRenderer = tilemap.GetComponent<TilemapRenderer>();
            
            tilemapRenderer.sortingOrder = baseOrder + layer;

            allTilemaps.Add(layer, tilemap);

            return tilemap;
        }

        public IEnumerable<Tilemap> GetAllTilemaps()
        {
            return allTilemaps.Values;
        }

        #endregion

        public void ClearAllTilemaps()
        {
            allTilemaps.Clear();
        }

        public Sprite GetSprite(int layerIndex, Vector2Int pos)
        {
            return GetTilemap(layerIndex).GetSprite(pos.As3DXY());
        }

        public Vector3 GetRealPosition(Vector2Int pos)
        {
            return tilemapPrefabController.prefab.CellToWorld(pos.As3DXY());
        }

        public Vector2Int GetTilePosition(Vector3 realPos)
        {
            return tilemapPrefabController.prefab.WorldToCell(realPos).XY();
        }

        #region Cell Size

        public Vector2 GetCellSize()
        {
            return tilemapPrefabController.prefab.cellSize.XY();
        }

        // public void SetCellSize(Vector2 cellSize)
        // {
        //     tilemapPrefabController.tilemapPrefab.layoutGrid.cellSize = cellSize.As3DXY();
        //
        //     foreach (var tilemap in allTilemaps.Values)
        //     {
        //         tilemap.layoutGrid.cellSize = cellSize.As3DXY();
        //     }
        // }

        #endregion
    }
}
