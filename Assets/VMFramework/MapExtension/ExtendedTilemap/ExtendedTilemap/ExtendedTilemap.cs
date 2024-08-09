using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using VMFramework.Core;
using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.GameLogicArchitecture;

namespace VMFramework.Maps
{
    [RequireComponent(typeof(TilemapGroupController))]
    public sealed partial class ExtendedTilemap
        : SerializedMonoBehaviour, IReadableMap<Vector2Int, ExtendedRuleTile>, ITileFillableMap<Vector2Int, ExtendedRuleTile>,
            ITileReplaceableMap<Vector2Int, ExtendedRuleTile>, ITileDestructibleMap<Vector2Int, ExtendedRuleTile>,
            ITilesRectangleFillableGridMap<ExtendedRuleTile>, ITilesRectangleReplaceableGridMap<ExtendedRuleTile>,
            ITilesRectangleDestructibleGridMap, IClearableMap
    {
        private ExtendedRuleTileGeneralSetting Setting => BuiltInModulesSetting.ExtendedRuleTileGeneralSetting;

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
                ClearMap();
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ForcedUpdate(Vector2Int pos)
        {
            TryGetTile(pos, out var extendedRuleTile);
            ForcedUpdate(pos, extendedRuleTile);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ForcedUpdate(Vector2Int pos, [NotNull] ExtendedRuleTile extendedRuleTile)
        {
            SetEmpty(pos);

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
            if (allRuleTiles.TryGetValue(pos, out var extendedRuleTile))
            {
                if (extendedRuleTile.enableUpdate)
                {
                    ForcedUpdate(pos, extendedRuleTile);
                }
            }
            else
            {
                SetEmpty(pos);
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

        #region Fill

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool FillTileWithoutUpdate(Vector2Int position, [NotNull] ExtendedRuleTile extendedRuleTile)
        {
            extendedRuleTile.AssertIsNotNull(nameof(extendedRuleTile));

            return allRuleTiles.TryAdd(position, extendedRuleTile);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool FillTile(Vector2Int position, [NotNull] ExtendedRuleTile extendedRuleTile)
        {
            if (FillTileWithoutUpdate(position, extendedRuleTile) == false)
            {
                return false;
            }

            ForcedUpdate(position, extendedRuleTile);

            foreach (var neighborPos in position.GetEightDirectionsNeighbors())
            {
                UpdateTile(neighborPos);
            }
            
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void FillRectangleTiles(RectangleInteger rectangle, [NotNull] ExtendedRuleTile extendedRuleTile)
        {
            foreach (var position in rectangle)
            {
                FillTileWithoutUpdate(position, extendedRuleTile);
            }

            foreach (var position in rectangle)
            {
                ForcedUpdate(position, extendedRuleTile);
            }

            foreach (var position in rectangle.GetOuterRectangle().GetBoundary())
            {
                ForcedUpdate(position);
            }
        }

        #endregion

        #region Replace

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ReplaceTileWithoutUpdate(Vector2Int position, [NotNull] ExtendedRuleTile extendedRuleTile)
        {
            allRuleTiles[position] = extendedRuleTile;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ReplaceTile(Vector2Int position, ExtendedRuleTile extendedRuleTile)
        {
            if (extendedRuleTile == null)
            {
                DestructTile(position, out _);
                return;
            }

            ReplaceTileWithoutUpdate(position, extendedRuleTile);

            ForcedUpdate(position, extendedRuleTile);

            foreach (var neighborPos in position.GetEightDirectionsNeighbors())
            {
                UpdateTile(neighborPos);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ReplaceRectangleTiles(RectangleInteger rectangle, ExtendedRuleTile extendedRuleTile)
        {
            if (extendedRuleTile == null)
            {
                DestructRectangleTiles(rectangle);
                return;
            }

            foreach (var position in rectangle)
            {
                ReplaceTileWithoutUpdate(position, extendedRuleTile);
            }

            foreach (var position in rectangle)
            {
                ForcedUpdate(position, extendedRuleTile);
            }

            foreach (var position in rectangle.GetOuterRectangle().GetBoundary())
            {
                ForcedUpdate(position);
            }
        }

        #endregion

        #region Destruct

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool DestructTileWithoutUpdate(Vector2Int position, out ExtendedRuleTile tile)
        {
            if (allRuleTiles.Remove(position, out tile))
            {
                SetEmpty(position);
                return true;
            }
            
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool DestructTile(Vector2Int position, out ExtendedRuleTile tile)
        {
            if (DestructTileWithoutUpdate(position, out tile))
            {
                foreach (var neighborPos in position.GetEightDirectionsNeighbors())
                {
                    UpdateTile(neighborPos);
                }
                
                return true;
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void DestructRectangleTiles(RectangleInteger rectangle)
        {
            foreach (var position in rectangle)
            {
                DestructTileWithoutUpdate(position, out _);
            }

            foreach (var position in rectangle.GetOuterRectangle().GetBoundary())
            {
                ForcedUpdate(position);
            }
        }

        #endregion

        #region Clear

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void SetEmpty(Vector2Int pos)
        {
            foreach (var tilemap in tilemapGroupController.GetAllTilemaps())
            {
                tilemap.SetTile(pos.As3DXY(), TileBaseManager.EmptyTileBase);
            }
        }

        /// <summary>
        /// 清空地图
        /// </summary>
        public void ClearMap()
        {
            allRuleTiles.Clear();
            tilemapGroupController.ClearAllTilemaps();
        }

        #endregion
    }
}