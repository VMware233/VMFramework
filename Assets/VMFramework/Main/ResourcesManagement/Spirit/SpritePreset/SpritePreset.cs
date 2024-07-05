using VMFramework.GameLogicArchitecture;
using VMFramework.Core;
using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.Procedure;

namespace VMFramework.ResourcesManagement
{
    public partial class SpritePreset : GameTypedGamePrefab, IInitializer
    {
        protected const string SPRITE_PREVIEW_GROUP =
            TAB_GROUP_NAME + "/" + BASIC_CATEGORY + "/Sprite Preview Group";

        protected const string FLIP_GROUP = TAB_GROUP_NAME + "/" + BASIC_CATEGORY + "/Flip Group";

        public bool enableInitializationDebugLog => false;

        [HideLabel, HorizontalGroup(SPRITE_PREVIEW_GROUP)]
        [PreviewField(40, ObjectFieldAlignment.Center)]
        public Sprite sprite;
        
        [HorizontalGroup(FLIP_GROUP)]
        public FlipType2D preloadFlipType = FlipType2D.None;

        [HorizontalGroup(FLIP_GROUP)]
        [SerializeField]
        private SpritePivotFlipType spritePivotFlipType = SpritePivotFlipType.NoChange;

        public Sprite GenerateSprite(FlipType2D flipType)
        {
            if (sprite is null)
            {
                return null;
            }

            if (flipType == FlipType2D.None)
            {
                return sprite;
            }

            var resultSprite = sprite.Flip(flipType, spritePivotFlipType);
            resultSprite.name = id;

            return resultSprite;
        }
    }
}
