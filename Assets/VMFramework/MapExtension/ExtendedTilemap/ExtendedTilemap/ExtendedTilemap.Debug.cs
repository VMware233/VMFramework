#if UNITY_EDITOR
using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.Core;
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
        private void _SetTile(Vector2Int pos, [GamePrefabID(typeof(ExtendedRuleTile))] [HideLabel] string id)
        {
            SetTile(pos, id);
        }

        [Button(ButtonStyle.Box)]
        private void _SetRectangleTiles(Vector2Int start, Vector2Int end,
            [GamePrefabID(typeof(ExtendedRuleTile))] [HideLabel] string tile)
        {
            SetRectangleTiles(start, end, tile);
        }

        [Button(ButtonStyle.Box)]
        private void _ClearTile(Vector2Int pos)
        {
            ClearTile(pos);
        }

        [Button(ButtonStyle.Box)]
        private void _ClearRectangleTiles(Vector2Int start, Vector2Int end)
        {
            ClearRectangleTiles(start, end);
        }

        [Button("在矩形区域内放置随机数量的特定ID的瓦片", ButtonStyle.Box)]
        private void SetRandomTiles([HideLabel] RectangleInteger rectangle,
            [GamePrefabID(typeof(ExtendedRuleTile))] [HideLabel] string id, [MinValue(1)] int number)
        {
            this.SetRandomRectangleTiles(rectangle, id, number);
        }

        [Button("在矩形区域内随机放置特定面积占比数量的特定ID的瓦片", ButtonStyle.Box)]
        private void SetRandomTiles([HideLabel] RectangleInteger rectangle,
            [GamePrefabID(typeof(ExtendedRuleTile))] [HideLabel] string id, [PropertyRange(0, 1)] float ratio)
        {
            this.SetRandomRectangleTiles(rectangle, id, ratio);
        }
    }
}
#endif