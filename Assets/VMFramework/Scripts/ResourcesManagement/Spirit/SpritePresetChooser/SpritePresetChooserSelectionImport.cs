#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using VMFramework.Core.Linq;

namespace VMFramework.Configuration
{
    public partial class SpritePresetChooser
    {
        private IEnumerable<Sprite> selectedSprites =>
            Selection.objects.Select<Object, Sprite>();

        private int selectedSpritesCount => selectedSprites.Count();

        private bool showAddSelectedSpritesButton =>
            isRandomValue && selectedSpritesCount > 0;

        [Button("添加选中的Sprites")]
        [ShowIf(nameof(showAddSelectedSpritesButton))]
        private void AddSelectedSprites()
        {
            if (isRandomValue == false)
            {
                return;
            }

            foreach (var selectedSprite in selectedSprites)
            {
                if (selectedSprite == null)
                {
                    continue;
                }

                if (randomType == WEIGHTED_SELECT)
                {
                    var values = weightedSelectItems.Select(item => item.value.sprite)
                        .ToList();

                    if (values.Contains(selectedSprite) == false)
                    {
                        weightedSelectItems.Add(new()
                        {
                            value = new(selectedSprite),
                            ratio = 1
                        });
                    }

                    OnWeightedSelectItemsChanged();
                }

                if (randomType == CIRCULAR_SELECT)
                {
                    var values = circularSelectItems.Select(item => item.value.sprite)
                        .ToList();

                    if (values.Contains(selectedSprite) == false)
                    {
                        circularSelectItems.Add(new()
                        {
                            value = new(selectedSprite),
                            times = 1
                        });
                    }
                }
            }
        }
    }
}
#endif