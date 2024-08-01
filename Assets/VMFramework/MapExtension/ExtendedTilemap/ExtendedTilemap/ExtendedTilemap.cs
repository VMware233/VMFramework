using System.Collections.Generic;
using System.Runtime.CompilerServices;
using VMFramework.Core;
using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.GameLogicArchitecture;

namespace VMFramework.Maps
{
    [RequireComponent(typeof(TilemapGroupController))]
    public sealed partial class ExtendedTilemap
        : SerializedMonoBehaviour, IReadableMap2D<ExtendedRuleTile>, IWritableMap2D<string>,
            IWritableMap2D<ExtendedRuleTile>
    {
        private ExtendedRuleTileGeneralSetting setting => BuiltInModulesSetting.extendedRuleTileGeneralSetting;

        [SerializeField]
        private bool clearMapOnAwake = false;

        [ShowInInspector]
        private readonly Dictionary<Vector2Int, ExtendedRuleTile> allRuleTiles = new();

        [ShowInInspector]
        [ReadOnly]
        public TilemapGroupController tilemapGroupController { get; private set; }

        private void Awake()
        {
            tilemapGroupController = GetComponent<TilemapGroupController>();

            if (clearMapOnAwake)
            {
                ClearAll();
            }
        }

        #region RealPosition

        public Vector3 GetRealPosition(Vector2Int pos)
        {
            return tilemapGroupController.GetRealPosition(pos);
        }

        public Vector2Int GetTilePosition(Vector3 realPos)
        {
            return tilemapGroupController.GetTilePosition(realPos);
        }

        #endregion

        #region Cell Size

        public Vector2 GetCellSize()
        {
            return tilemapGroupController.GetCellSize();
        }

        #endregion

        #region Sprite

        public Sprite GetSprite(int layerIndex, Vector2Int pos)
        {
            return tilemapGroupController.GetSprite(layerIndex, pos);
        }

        #endregion

        #region Query Tile

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<Vector2Int> GetAllPoints()
        {
            return allRuleTiles.Keys;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<ExtendedRuleTile> GetAllTiles()
        {
            return allRuleTiles.Values;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetTile(Vector2Int pos, out ExtendedRuleTile tile)
        {
            return allRuleTiles.TryGetValue(pos, out tile);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ExtendedRuleTile GetTile(Vector2Int pos)
        {
            return allRuleTiles.GetValueOrDefault(pos);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsTile(Vector2Int point)
        {
            return allRuleTiles.ContainsKey(point);
        }

        #endregion

        #region Update

        private void ForceUpdate(Vector2Int pos)
        {
            if (TryGetTile(pos, out var extRuleTile))
            {
                ForceUpdate(pos, extRuleTile);
            }
        }

        private void ForceUpdate(Vector2Int pos, ExtendedRuleTile extendedRuleTile)
        {
            SetEmpty(pos);

            if (extendedRuleTile == null)
            {
                return;
            }
            
            var neighbor = this.GetEightDirectionsNeighbors(pos);

            var spriteLayers = extendedRuleTile.GetSpriteLayers(neighbor);

            if (spriteLayers == null)
            {
                return;
            }

            foreach (var spriteLayer in spriteLayers)
            {
                var layerIndex = spriteLayer.layer;

                var tilemap = tilemapGroupController.GetTilemap(layerIndex);

                var tileBase = TileBaseManager.GetTileBase(spriteLayer.sprite.GetValue());

                tilemap.SetTile(pos.As3DXY(), tileBase);
            }
        }
        
        /// <summary>
        /// 更新瓦片贴图
        /// </summary>
        /// <param name="pos"></param>
        public void UpdateTile(Vector2Int pos)
        {
            if (allRuleTiles.TryGetValue(pos, out var extRuleTile))
            {
                if (extRuleTile.enableUpdate)
                {
                    ForceUpdate(pos);
                }
            }
            else
            {
                SetTileWithoutUpdate(pos, (ExtendedRuleTile)null);
            }
        }
        
        /// <summary>
        /// 更新所有瓦片的贴图
        /// </summary>
        public void RefreshMap()
        {
            foreach (var pos in allRuleTiles.Keys)
            {
                UpdateTile(pos);
            }
        }

        #endregion

        #region Indexer

        public ExtendedRuleTile this[Vector2Int point]
        {
            get => GetTile(point);
            set => SetTile(point, value);
        }

        #endregion

        #region Set Tile

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void SetTileWithoutUpdate(Vector2Int pos, ExtendedRuleTile extendedRuleTile)
        {
            if (extendedRuleTile == null)
            {
                allRuleTiles.Remove(pos);
            }
            else
            {
                allRuleTiles[pos] = extendedRuleTile;
            }

            ForceUpdate(pos, extendedRuleTile);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void SetTileWithoutUpdate(Vector2Int pos, string id)
        {
            if (id.IsNullOrEmpty())
            {
                SetTileWithoutUpdate(pos, (ExtendedRuleTile)null);
                return;
            }
            
            var extendedRuleTile = GamePrefabManager.GetGamePrefabStrictly<ExtendedRuleTile>(id);

            SetTileWithoutUpdate(pos, extendedRuleTile);
        }

        /// <summary>
        /// 放置瓦片
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="id"></param>
        public void SetTile(Vector2Int pos, string id)
        {
            SetTileWithoutUpdate(pos, id);

            foreach (var neighborPos in pos.GetEightDirectionsNeighbors())
            {
                UpdateTile(neighborPos);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetTile(Vector2Int pos, ExtendedRuleTile extendedRuleTile)
        {
            SetTileWithoutUpdate(pos, extendedRuleTile);

            foreach (var neighborPos in pos.GetEightDirectionsNeighbors())
            {
                UpdateTile(neighborPos);
            }
        }

        #endregion

        #region Set Tiles

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void SetTilesWithoutUpdate(Vector2Int startPos, Vector2Int endPos, string id)
        {
            var extendedRuleTile = GamePrefabManager.GetGamePrefabStrictly<ExtendedRuleTile>(id);

            foreach (var pos in startPos.GetRectangle(endPos))
            {
                SetTileWithoutUpdate(pos, extendedRuleTile);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void SetTilesWithoutUpdate(Vector2Int startPos, Vector2Int endPos, ExtendedRuleTile extendedRuleTile)
        {
            foreach (var pos in startPos.GetRectangle(endPos))
            {
                SetTileWithoutUpdate(pos, extendedRuleTile);
            }
        }

        /// <summary>
        /// 在矩形区域放置特定ID的瓦片
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="tile"></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetRectangleTiles(Vector2Int start, Vector2Int end, string tile)
        {
            SetTilesWithoutUpdate(start, end, tile);

            foreach (var nearPoint in start.GetOuterRectangle(end))
            {
                UpdateTile(nearPoint);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetRectangleTiles(Vector2Int start, Vector2Int end, ExtendedRuleTile tile)
        {
            SetTilesWithoutUpdate(start, end, tile);
            
            foreach (var nearPoint in start.GetOuterRectangle(end))
            {
                UpdateTile(nearPoint);
            }
        }

        #endregion

        #region Clear

        /// <summary>
        /// 清除瓦片
        /// </summary>
        /// <param name="pos"></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ClearTile(Vector2Int pos)
        {
            SetTile(pos, (ExtendedRuleTile)null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ClearRectangleTiles(Vector2Int start, Vector2Int end)
        {
            SetRectangleTiles(start, end, (ExtendedRuleTile)null);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void SetEmpty(Vector2Int pos)
        {
            foreach (var tilemap in tilemapGroupController.GetAllTilemaps())
            {
                tilemap.SetTile(pos.As3DXY(), TileBaseManager.emptyTileBase);
            }
        }

        /// <summary>
        /// 清空地图
        /// </summary>
        public void ClearAll()
        {
            allRuleTiles.Clear();
            tilemapGroupController.ClearAllTilemaps();
        }

        #endregion
    }
}
