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
        private void _FillTile([HideLabel] Vector3Int position,
            [GamePrefabID(typeof(ExtendedRuleTile))] [HideLabel] string id)
        {
            this.FillTile(position, id);
        }

        [Button(ButtonStyle.Box)]
        private void _SetCubeTiles(Vector3Int start, Vector3Int end,
            [GamePrefabID(typeof(ExtendedRuleTile))] [HideLabel] string tile)
        {
            this.FillCubeTiles(new(start, end), tile);
        }

        [Button(ButtonStyle.Box)]
        private void _DestructTile([HideLabel] Vector3Int position)
        {
            this.DestructTile(position);
        }

        [Button(ButtonStyle.Box)]
        private void _DestructCubeTiles(Vector3Int start, Vector3Int end)
        {
            DestructCubeTiles(new(start, end));
        }
    }
}
#endif