#if UNITY_EDITOR
using VMFramework.Core;
using VMFramework.GameLogicArchitecture;
using VMFramework.GameLogicArchitecture.Editor;
using VMFramework.ResourcesManagement;

namespace VMFramework.Configuration
{
    public partial class SpritePresetItem
    {
        private void OnIDChanged()
        {
            if (spritePresetID.IsNullOrEmpty())
            {
                flipX = false;
                flipY = false;
                return;
            }

            var currentSpritePreset = GamePrefabManager.GetGamePrefab<SpritePreset>(spritePresetID);

            if (currentSpritePreset == null)
            {
                flipX = false;
                flipY = false;
                return;
            }

            flipX = currentSpritePreset.flipX;
            flipY = currentSpritePreset.flipY;
        }

        private void OnFlipChanged()
        {
            if (spritePresetID.IsNullOrEmpty())
            {
                return;
            }

            var currentSpritePreset = GamePrefabManager.GetGamePrefabStrictly<SpritePreset>(spritePresetID);

            if (currentSpritePreset == null)
            {
                return;
            }

            var originSprite = currentSpritePreset.sprite;

            var allSpritePresets = GamePrefabManager.GetAllGamePrefabs<SpritePreset>();

            foreach (var spritePreset in allSpritePresets)
            {
                if (spritePreset.sprite != originSprite)
                {
                    continue;
                }

                if (spritePreset.flipX != flipX)
                {
                    continue;
                }

                if (spritePreset.flipY != flipY)
                {
                    continue;
                }

                spritePresetID = spritePreset.id;

                return;
            }

            var newID = currentSpritePreset.id;

            if (flipX)
            {
                newID += "_xFlip";
            }

            if (flipY)
            {
                newID += "_yFlip";
            }

            var newSpritePreset = new SpritePreset()
            {
                id = newID,
                sprite = originSprite,
                flipX = flipX,
                flipY = flipY
            };

            GamePrefabWrapperCreator.CreateGamePrefabWrapper(newSpritePreset);

            spritePresetID = newSpritePreset.id;
        }

        protected override void OnInspectorInit()
        {
            base.OnInspectorInit();

            OnIDChanged();
        }
    }
}
#endif