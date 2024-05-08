using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using VMFramework.Core;
using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.GameLogicArchitecture;
using VMFramework.Procedure;

namespace VMFramework.ResourcesManagement
{
    [ManagerCreationProvider(ManagerType.ResourcesCore)]
    public class SpriteManager : SerializedMonoBehaviour
    {
        [LabelText("Sprite缓存")]
        [ShowInInspector]
        private static readonly Dictionary<string, Sprite> spriteCache = new();
        
        [LabelText("SpriteID缓存")]
        [ShowInInspector]
        private static readonly Dictionary<Sprite, string> spriteIDCache = new();

        #region Get & Reset Sprite

        public static Sprite GetSprite(string spritePresetID)
        {
            if (spritePresetID.IsNullOrEmpty())
            {
                return null;
            }

            if (spriteCache.TryGetValue(spritePresetID, out var existedSprite))
            {
                return existedSprite;
            }

            var spritePreset = GamePrefabManager.GetGamePrefab<SpritePreset>(spritePresetID);

            if (spritePreset == null)
            {
                return null;
            }

            var sprite = spritePreset.GenerateSprite();

            spriteCache.Add(spritePresetID, sprite);

            return sprite;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ResetSprite(string spritePresetID)
        {
            spriteCache.Remove(spritePresetID);

            GetSprite(spritePresetID);
        }

        #endregion

        #region Sprite Preset

        public static bool HasSpritePreset(Sprite sprite)
        {
            if (sprite == null)
            {
                return false;
            }
            
            if (spriteIDCache.TryGetValue(sprite, out var spritePresetID))
            {
                if (GamePrefabManager.ContainsGamePrefab(spritePresetID))
                {
                    return true;
                }
                
                spriteIDCache.Remove(sprite);
                    
                return false;
            }

            var spritePreset = GamePrefabManager.GetAllGamePrefabs<SpritePreset>()
                .FirstOrDefault(prefab => prefab.sprite == sprite);

            if (spritePreset == null)
            {
                return false;
            }
            
            spriteIDCache.Add(sprite, spritePreset.id);

            return true;
        }
        
        public static SpritePreset GetSpritePreset(Sprite sprite)
        {
            if (sprite == null)
            {
                return null;
            }
            
            if (spriteIDCache.TryGetValue(sprite, out var spritePresetID))
            {
                var existedSpritePreset = GamePrefabManager.GetGamePrefab<SpritePreset>(spritePresetID);
                
                if (existedSpritePreset == null)
                {
                    spriteIDCache.Remove(sprite);
                }
                
                return existedSpritePreset;
            }

            var spritePreset = GamePrefabManager.GetAllGamePrefabs<SpritePreset>()
                .FirstOrDefault(prefab => prefab.sprite == sprite);
            
            if (spritePreset == null)
            {
                return null;
            }
            
            spriteIDCache.Add(sprite, spritePreset.id);

            return spritePreset;
        }

        #endregion
    }
}
