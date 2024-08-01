using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.Core;
using VMFramework.GameLogicArchitecture;

namespace VMFramework.Maps
{
    [RequireComponent(typeof(ExtendedTilemapPrefabController))]
    public sealed partial class IsometricTilemap
        : SerializedMonoBehaviour, IWritableMap3D<ExtendedRuleTile>, IWritableMap3D<string>,
            IReadableMap3D<ExtendedRuleTile>
    {
        [field: SerializeField]
        [field: MinValue(1)]
        public int maxZ { get; private set; }

        [ShowInInspector]
        private short[] baseOrders;

        private ExtendedTilemapPrefabController prefabController;

        [ShowInInspector]
        private readonly Dictionary<int, ExtendedTilemap> tilemaps = new();

        private void Awake()
        {
            prefabController = GetComponent<ExtendedTilemapPrefabController>();

            baseOrders = new short[maxZ];

            int count = 0;
            foreach (var baseOrderFloat in UniformlySpacedRangeFloat.ExcludeBoundaries(short.MinValue, short.MaxValue,
                         maxZ))
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

        #region Set Tiles

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetTile(Vector3Int point, ExtendedRuleTile tile)
        {
            if (tilemaps.TryGetValue(point.z, out var extendedTilemap) == false)
            {
                extendedTilemap = CreateTilemap(point.z);
            }

            extendedTilemap.SetTile(point.XY(), tile);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetTile(Vector3Int point, string tile)
        {
            var extendedTile = GamePrefabManager.GetGamePrefabStrictly<ExtendedRuleTile>(tile);

            SetTile(point, extendedTile);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetCubeTiles(Vector3Int start, Vector3Int end, ExtendedRuleTile tile)
        {
            var startXY = start.XY();
            var endXY = end.XY();

            for (var z = start.z; z <= end.z; z++)
            {
                if (tilemaps.TryGetValue(z, out var extendedTilemap) == false)
                {
                    extendedTilemap = CreateTilemap(z);
                }

                extendedTilemap.SetRectangleTiles(startXY, endXY, tile);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetCubeTiles(Vector3Int start, Vector3Int end, string tile)
        {
            var extendedTile = GamePrefabManager.GetGamePrefabStrictly<ExtendedRuleTile>(tile);

            SetCubeTiles(start, end, extendedTile);
        }

        #endregion

        #region Clear

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ClearTile(Vector3Int point)
        {
            if (tilemaps.TryGetValue(point.z, out var extendedTilemap) == false)
            {
                return;
            }

            extendedTilemap.ClearTile(point.XY());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ClearAll()
        {
            foreach (var tilemap in tilemaps.Values)
            {
                tilemap.ClearAll();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ClearCubeTiles(Vector3Int start, Vector3Int end)
        {
            var startXY = start.XY();
            var endXY = end.XY();

            for (var z = start.z; z <= end.z; z++)
            {
                if (tilemaps.TryGetValue(z, out var extendedTilemap) == false)
                {
                    continue;
                }

                extendedTilemap.ClearRectangleTiles(startXY, endXY);
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