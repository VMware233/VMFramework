using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.GameLogicArchitecture;
using VMFramework.OdinExtensions;
using VMFramework.ResourcesManagement;

namespace VMFramework.Configuration
{
    public sealed partial class SpritePresetItem : BaseConfigClass
    {
        private const string SPRITE_PREVIEW_GROUP = "SpritePreview";
        
        private const string SPRITE_PREVIEW_LEFT_GROUP = "SpritePreview/SpritePreviewLeft";
        
        private const string SPRITE_PREVIEW_FLIP_GROUP = "SpritePreview/SpritePreviewLeft/Flip";

#if UNITY_EDITOR
        [HideLabel, VerticalGroup(SPRITE_PREVIEW_LEFT_GROUP)]
        [GamePrefabID(typeof(SpritePreset))]
        [OnValueChanged(nameof(OnIDChanged))]
#endif
        public string spritePresetID;

#if UNITY_EDITOR
        [LabelText("X轴翻转"), HorizontalGroup(SPRITE_PREVIEW_FLIP_GROUP)]
        [LabelWidth(50)]
        [OnValueChanged(nameof(OnFlipChanged))]
        [ShowInInspector]
#endif
        private bool flipX = false;

#if UNITY_EDITOR
        [LabelText("Y轴翻转"), HorizontalGroup(SPRITE_PREVIEW_FLIP_GROUP)]
        [LabelWidth(50)]
        [OnValueChanged(nameof(OnFlipChanged))]
        [ShowInInspector]
#endif
        private bool flipY = false;

        [HideLabel, HorizontalGroup(SPRITE_PREVIEW_GROUP)]
        [PreviewField(40, ObjectFieldAlignment.Right)]
        [ShowInInspector]
        public Sprite sprite
        {
            get => SpriteManager.GetSprite(spritePresetID);
            private set
            {
                if (value == null)
                {
                    spritePresetID = null;
                }

                if (SpriteManager.HasSpritePreset(value) == false)
                {
                    GameCoreSettingBase.spriteGeneralSetting.AddSpritePreset(value);
                }

                spritePresetID = SpriteManager.GetSpritePreset(value)?.id;
            }
        }

        #region Constructor

        public SpritePresetItem() : this(null) { }

        public SpritePresetItem(Sprite sprite)
        {
            if (sprite == null)
            {
                return;
            }

            if (SpriteManager.HasSpritePreset(sprite) == false)
            {
                GameCoreSettingBase.spriteGeneralSetting.AddSpritePreset(sprite);
            }

            var spritePreset = SpriteManager.GetSpritePreset(sprite);

            spritePresetID = spritePreset?.id;
        }

        #endregion

        #region Conversion

        public static implicit operator Sprite(SpritePresetItem item)
        {
            return item.sprite;
        }

        #endregion
    }
}