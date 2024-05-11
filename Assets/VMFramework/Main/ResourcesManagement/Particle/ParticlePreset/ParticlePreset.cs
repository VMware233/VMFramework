using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.Configuration;
using VMFramework.GameLogicArchitecture;
using VMFramework.OdinExtensions;

namespace VMFramework.ResourcesManagement
{
    public partial class ParticlePreset : GameTypedGamePrefab
    {
        protected override string idSuffix => "particle";

        [LabelText("粒子预制体"), TabGroup(TAB_GROUP_NAME, BASIC_SETTING_CATEGORY)]
        [AssetList]
        [AssetSelector(Paths = "Assets")]
        [AssetsOnly]
        [Required]
        public ParticleSystem particlePrefab;

        [LabelText("持续时间限制"), TabGroup(TAB_GROUP_NAME, BASIC_SETTING_CATEGORY)]
        [ToggleButtons("是", "否")]
        public bool enableDurationLimitation = false;

        [LabelText("持续时间"), TabGroup(TAB_GROUP_NAME, BASIC_SETTING_CATEGORY)]
        [ShowIf(nameof(enableDurationLimitation))]
        public IChooserConfig<float> duration = new SingleValueChooserConfig<float>();
    }
}
