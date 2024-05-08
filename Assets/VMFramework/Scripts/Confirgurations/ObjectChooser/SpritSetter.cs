using System.Linq;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Object = UnityEngine.Object;
using VMFramework.Core.Linq;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace VMFramework.Configuration
{
    public class SpriteItem : BaseConfigClass
    {
        [HideLabel, HorizontalGroup]
        [Required]
        public Sprite sprite;

        [HideLabel, HorizontalGroup]
        [ShowInInspector]
        [PreviewField(40, ObjectFieldAlignment.Center)]
        private Sprite spritePreview => sprite;

        public override bool Equals(object obj) =>
            obj switch
            {
                null => false,
                SpriteItem spriteItem => sprite.Equals(spriteItem.sprite),
                _ => false
            };

        public override int GetHashCode()
        {
            if (sprite == null) return base.GetHashCode();

            return sprite.GetHashCode();
        }

        public static implicit operator Sprite(SpriteItem item)
        {
            return item.sprite;
        }

        public static implicit operator SpriteItem(Sprite sprite)
        {
            return new()
            {
                sprite = sprite
            };
        }
    }

    public class SpriteSetter : ObjectChooser<SpriteItem>
    {
        //[ShowInInspector]
        //[HideLabel]
        //[AssetList(Path = "Resources"), PreviewField(70, ObjectFieldAlignment.Center)]
        //[AssetSelector(Paths = "Assets/Resources")]
        //[ShowIf(@"@isRandomValue == false && fixedType == ""Single Value""")]
        //public Sprite fixedSpritePreview
        //{
        //    get => value;
        //    set => this.value = value;
        //}

        #region GUI

#if UNITY_EDITOR
        private IEnumerable<Sprite> selectedSprites => Selection.objects.Select<Object, Sprite>();

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
                            value = selectedSprite,
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
                            value = selectedSprite,
                            times = 1
                        });
                    }
                }
            }
        }

#endif

        #endregion

        public static implicit operator Sprite(SpriteSetter setter)
        {
            return setter.GetValue();
        }
    }
}
