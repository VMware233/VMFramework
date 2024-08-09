#if UNITY_EDITOR
using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.Core;
using VMFramework.GameLogicArchitecture;
using VMFramework.OdinExtensions;

namespace VMFramework.Maps
{
    public partial class ExtendedTilemap
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
        private void _FillTile([HideLabel] Vector2Int position, [GamePrefabID(typeof(ExtendedRuleTile))] [HideLabel] string id)
        {
            this.FillTile(position, id);
        }

        [Button(ButtonStyle.Box)]
        private void _SetRectangleTiles(Vector2Int start, Vector2Int end,
            [GamePrefabID(typeof(ExtendedRuleTile))] [HideLabel] string id)
        {
            this.FillRectangleTiles(new RectangleInteger(start, end), id);
        }

        [Button(ButtonStyle.Box)]
        private void _DestructTile([HideLabel] Vector2Int position)
        {
            this.DestructTile(position);
        }

        [Button(ButtonStyle.Box)]
        private void _DestructRectangleTiles(Vector2Int start, Vector2Int end)
        {
            DestructRectangleTiles(new RectangleInteger(start, end));
        }

        [Button(ButtonStyle.Box)]
        private void FillRandomTiles([HideLabel] RectangleInteger rectangle,
            [GamePrefabID(typeof(ExtendedRuleTile))] [HideLabel] string id, [PropertyRange(0, 1)] float density)
        {
            var extendedRuleTile = GamePrefabManager.GetGamePrefabStrictly<ExtendedRuleTile>(id);
            this.FillRandomRectangleTiles(rectangle, extendedRuleTile, density, GlobalRandom.Default);
        }
    }
}
#endif