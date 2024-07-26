#if UNITY_EDITOR
using Newtonsoft.Json;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using VMFramework.Configuration;

namespace VMFramework.Editor
{
    public sealed class TextureImporterConfiguration : BaseConfig
    {
        public const string SPRITE_CATEGORY = "Sprite";

        [JsonProperty]
        public bool isOn = true;

        [FolderPath]
        [JsonProperty]
        public string textureFolder = "Assets/Resources/Images";

        [FoldoutGroup(SPRITE_CATEGORY)]
        [JsonProperty]
        public bool ignoreSpriteImportMode = true;

        [FoldoutGroup(SPRITE_CATEGORY)]
        [HideIf(nameof(ignoreSpriteImportMode))]
        [JsonProperty]
        public SpriteImportMode spriteImportMode = SpriteImportMode.Single;

        [FoldoutGroup(SPRITE_CATEGORY)]
        [JsonProperty]
        public bool ignoreSpritePivot = true;
        
        [FoldoutGroup(SPRITE_CATEGORY)]
        [HideIf(nameof(ignoreSpritePivot))]
        [JsonProperty]
        public IVectorChooserConfig<Vector2> spritePivot =
            new SingleVectorChooserConfig<Vector2>(new Vector2(0.5f, 0.5f));

        [JsonProperty]
        public FilterMode filterMode = FilterMode.Point;

        [JsonProperty]
        public bool isReadable = true;

        [JsonProperty]
        public TextureImporterFormat textureFormat = TextureImporterFormat.RGBA32;

        [JsonProperty]
        public TextureImporterCompression compression = TextureImporterCompression.Uncompressed;
    }
}
#endif