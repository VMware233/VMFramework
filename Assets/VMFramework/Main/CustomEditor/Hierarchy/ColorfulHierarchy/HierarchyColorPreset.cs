#if UNITY_EDITOR
using Newtonsoft.Json;
using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.Configuration;
using VMFramework.OdinExtensions;

namespace VMFramework.Editor
{
    public class HierarchyColorPreset : BaseConfig
    {
        [IsNotNullOrEmpty]
        [JsonProperty]
        public string keyChar;

        [JsonProperty]
        public Color textColor = Color.white;

        [JsonProperty]
        public Color backgroundColor = Color.black;

        [EnumToggleButtons]
        [JsonProperty]
        public TextAnchor textAlignment = TextAnchor.MiddleCenter;

        [EnumToggleButtons]
        [JsonProperty]
        public FontStyle fontStyle = FontStyle.Bold;

        [JsonProperty]
        public bool autoUpperLetters = true;
    }
}
#endif