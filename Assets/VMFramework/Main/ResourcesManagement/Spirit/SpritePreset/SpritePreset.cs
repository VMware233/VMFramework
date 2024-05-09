using VMFramework.GameLogicArchitecture;
using VMFramework.Core;
using Sirenix.OdinInspector;
using UnityEngine;

namespace VMFramework.ResourcesManagement
{
    public partial class SpritePreset : GameTypedGamePrefab
    {
        protected const string SPRITE_PREVIEW_GROUP =
            TAB_GROUP_NAME + "/" + BASIC_SETTING_CATEGORY + "/Sprite Preview Group";

        protected const string FLIP_GROUP = TAB_GROUP_NAME + "/" + BASIC_SETTING_CATEGORY + "/Flip Group";

        [HideLabel, HorizontalGroup(SPRITE_PREVIEW_GROUP)]
        [PreviewField(40, ObjectFieldAlignment.Center)]
        public Sprite sprite;

        [LabelText("是否翻转X轴"), HorizontalGroup(FLIP_GROUP)]
        [SerializeField]
        [OnValueChanged(nameof(OnValueChanged))]
        public bool flipX = false;

        [LabelText("是否翻转Y轴"), HorizontalGroup(FLIP_GROUP)]
        [SerializeField]
        [OnValueChanged(nameof(OnValueChanged))]
        public bool flipY = false;

        [LabelText("Pivot操作")]
        [ShowIf(nameof(hasFlipOperation))]
        [SerializeField]
        [OnValueChanged(nameof(OnValueChanged))]
        private SpriteUtility.SpritePivotOperationType spritePivotOperationType =
            SpriteUtility.SpritePivotOperationType.Reversed;

        [HideLabel, HorizontalGroup(SPRITE_PREVIEW_GROUP)]
        [ShowInInspector]
        [ShowIf(nameof(hasFlipOperation))]
        [PreviewField(40, ObjectFieldAlignment.Center)]
        private Sprite spritePreview => SpriteManager.GetSprite(id);

        private bool hasFlipOperation => flipX || flipY;

        #region GUI

        private void OnValueChanged()
        {
            SpriteManager.ResetSprite(id);
        }

        #endregion

        #region Init

        public override bool isPreInitializationRequired => true;

        public override bool isInitializationRequired => false;

        public override bool isPostInitializationRequired => false;

        public override bool isInitializationCompleteRequired => false;

        protected override void OnPreInit()
        {
            base.OnPreInit();

            SpriteManager.GetSprite(id);
        }

        #endregion

        public Sprite GenerateSprite()
        {
            if (sprite is null)
            {
                return null;
            }

            if (flipX == false && flipY == false)
            {
                return sprite;
            }

            var resultSprite = sprite.Flip(flipX, flipY, spritePivotOperationType);
            resultSprite.name = id;

            return resultSprite;
        }
    }
}
