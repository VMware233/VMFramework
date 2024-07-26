#if UNITY_EDITOR
using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.OdinExtensions;

namespace VMFramework.Maps
{
    public partial class IsometricTilemap
    {
        [Button(ButtonStyle.Box)]
        private void _RefreshMap()
        {
            RefreshMap();
        }

        [Button]
        private void _ClearMap()
        {
            ClearMap();
        }

        [Button(ButtonStyle.Box)]
        private void _SetTile(Vector3Int pos, [GamePrefabID(typeof(ExtendedRuleTile))] [HideLabel] string id)
        {
            SetTile(pos, id);
        }

        [Button(ButtonStyle.Box)]
        private void _SetCubeTiles(Vector3Int start, Vector3Int end,
            [GamePrefabID(typeof(ExtendedRuleTile))] [HideLabel] string tile)
        {
            SetCubeTiles(start, end, tile);
        }

        [Button(ButtonStyle.Box)]
        private void _ClearTile(Vector3Int pos)
        {
            ClearTile(pos);
        }

        [Button(ButtonStyle.Box)]
        private void _ClearCubeTiles(Vector3Int start, Vector3Int end)
        {
            ClearCubeTiles(start, end);
        }
    }
}
#endif