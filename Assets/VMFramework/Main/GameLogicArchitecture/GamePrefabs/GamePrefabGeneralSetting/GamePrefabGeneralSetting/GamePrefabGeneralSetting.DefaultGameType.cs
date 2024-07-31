using Newtonsoft.Json;
using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.Core;
using VMFramework.OdinExtensions;

namespace VMFramework.GameLogicArchitecture
{
    public partial class GamePrefabGeneralSetting
    {
        [field: TabGroup(TAB_GROUP_NAME, GAME_TYPE_CATEGORY)]
        [field: InfoBox("GamePrefab's Game Type is disabled", VisibleIf = "@!GamePrefabGameTypeEnabled")]
        [field: GameTypeID]
        [field: EnableIf(nameof(GamePrefabGameTypeEnabled))]
        [field: SerializeField]
        [JsonProperty]
        public string DefaultGameType { get; private set; }

        private bool GamePrefabGameTypeEnabled =>
            BaseGamePrefabType.IsDerivedFrom<IGameTypedGamePrefab>(false);

        #region JSON

        public bool ShouldSerializedefaultGameType()
        {
            return GamePrefabGameTypeEnabled;
        }

        #endregion
    }
}