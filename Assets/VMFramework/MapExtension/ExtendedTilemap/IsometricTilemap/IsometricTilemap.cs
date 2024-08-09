using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.Core;
using VMFramework.GameLogicArchitecture;

namespace VMFramework.Maps
{
    [RequireComponent(typeof(ExtendedTilemapPrefabController))]
    public sealed partial class IsometricTilemap
        : SerializedMonoBehaviour, IReadableMap<Vector3Int, ExtendedRuleTile>, ITileFillableMap<Vector3Int, ExtendedRuleTile>,
            ITileReplaceableMap<Vector3Int, ExtendedRuleTile>, ITileDestructibleMap<Vector3Int, ExtendedRuleTile>,
            ITilesCubeFillableGridMap<ExtendedRuleTile>, ITilesCubeReplaceableGridMap<ExtendedRuleTile>,
            ITilesCubeDestructibleGridMap, IClearableMap
    {
        [field: SerializeField]
        [field: MinValue(1)]
        public int MaxZ { get; private set; }

        [ShowInInspector]
        private short[] baseOrders;

        private ExtendedTilemapPrefabController prefabController;

        [ShowInInspector]
        private readonly Dictionary<int, ExtendedTilemap> tilemaps = new();

        private void Awake()
        {
            prefabController = GetComponent<ExtendedTilemapPrefabController>();

            baseOrders = new short[MaxZ];

            int count = 0;
            foreach (var baseOrderFloat in UniformlySpacedRangeFloat.ExcludeBoundaries(short.MinValue, short.MaxValue,
                         MaxZ))
            {
                baseOrders[count] = (short)baseOrderFloat.Round();
                count++;
            }
        }

        #region Tilemap

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private ExtendedTilemap CreateTilemap(int z)
        {
            var go = Instantiate(prefabController.prefabObject, transform);
            var tilemap = go.GetComponent<ExtendedTilemap>();
            var tilemapGroup = tilemap.GetComponent<TilemapGroupController>();
            var grid = tilemapGroup.tilemapPrefabController.prefabObject.transform
                .QueryFirstComponentInChildren<Grid>(true);

            go.name = $"z={z}";
            go.transform.localPosition = go.transform.localPosition.AddY(grid.cellSize.y * z);

            tilemapGroup.SetBaseOrder(baseOrders[z]);

            tilemaps.Add(z, tilemap);

            return tilemap;
        }

        #endregion

        #region Fill

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool FillTile(Vector3Int position, [NotNull] ExtendedRuleTile extendedRuleTile)
        {
            if (tilemaps.TryGetValue(position.z, out var extendedTilemap) == false)
            {
                extendedTilemap = CreateTilemap(position.z);
            }

            return extendedTilemap.FillTile(position.XY(), extendedRuleTile);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void FillCubeTiles(CubeInteger cube, [NotNull] ExtendedRuleTile extendedRuleTile)
        {
            var xyRectangle = cube.xyRectangle;

            foreach (var z in cube.zRange)
            {
                if (tilemaps.TryGetValue(z, out var extendedTilemap) == false)
                {
                    extendedTilemap = CreateTilemap(z);
                }

                extendedTilemap.FillRectangleTiles(xyRectangle, extendedRuleTile);
            }
        }

        #endregion

        #region Replace

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ReplaceTile(Vector3Int position, ExtendedRuleTile extendedRuleTile)
        {
            if (tilemaps.TryGetValue(position.z, out var extendedTilemap) == false)
            {
                extendedTilemap = CreateTilemap(position.z);
            }

            extendedTilemap.ReplaceTile(position.XY(), extendedRuleTile);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ReplaceCubeTiles(CubeInteger cube, ExtendedRuleTile extendedRuleTile)
        {
            var xyRectangle = cube.xyRectangle;

            foreach (var z in cube.zRange)
            {
                if (tilemaps.TryGetValue(z, out var extendedTilemap) == false)
                {
                    extendedTilemap = CreateTilemap(z);
                }

                extendedTilemap.ReplaceRectangleTiles(xyRectangle, extendedRuleTile);
            }
        }

        #endregion

        #region Destruct

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool DestructTile(Vector3Int position, out ExtendedRuleTile tile)
        {
            if (tilemaps.TryGetValue(position.z, out var extendedTilemap) == false)
            {
                tile = null;
                return false;
            }

            return extendedTilemap.DestructTile(position.XY(), out tile);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void DestructCubeTiles(CubeInteger cube)
        {
            var xyRectangle = cube.xyRectangle;

            foreach (var z in cube.zRange)
            {
                if (tilemaps.TryGetValue(z, out var extendedTilemap) == false)
                {
                    extendedTilemap = CreateTilemap(z);
                }

                extendedTilemap.DestructRectangleTiles(xyRectangle);
            }
        }

        #endregion

        #region Clear

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ClearMap()
        {
            foreach (var tilemap in tilemaps.Values)
            {
                tilemap.ClearMap();
            }
        }

        #endregion

        #region Update

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void RefreshMap()
        {
            foreach (var tilemap in tilemaps.Values)
            {
                tilemap.RefreshMap();
            }
        }

        #endregion

        ExtendedRuleTile IMapping<Vector3Int, ExtendedRuleTile>.MapTo(Vector3Int point) => GetTile(point);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<Vector3Int> GetAllPoints()
        {
            foreach (var (z, tilemap) in tilemaps)
            {
                foreach (var point in tilemap.GetAllPoints())
                {
                    yield return point.InsertAsZ(z);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<ExtendedRuleTile> GetAllTiles()
        {
            foreach (var tilemap in tilemaps.Values)
            {
                foreach (var tile in tilemap.GetAllTiles())
                {
                    yield return tile;
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ExtendedRuleTile GetTile(Vector3Int point)
        {
            if (tilemaps.TryGetValue(point.z, out var extendedTilemap) == false)
            {
                return null;
            }

            return extendedTilemap.GetTile(point.XY());
        }
    }
}