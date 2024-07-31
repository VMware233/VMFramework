using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.GameLogicArchitecture;

namespace VMFramework.ResourcesManagement
{
    public class TrailPreset : GameTypedGamePrefab
    {
        protected override string IDSuffix => "trail";

        [LabelText("拖尾预制体")]
        [AssetsOnly]
        [Required]
        public TrailRenderer trailPrefab;
    }
}