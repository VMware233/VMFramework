using Newtonsoft.Json;
using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.Core;
using VMFramework.Core.Editor;
using VMFramework.OdinExtensions;

namespace VMFramework.GameLogicArchitecture
{
    public partial class GamePrefabGeneralSetting
    {
        [field: LabelText("默认游戏种类"), TabGroup(TAB_GROUP_NAME, GAME_TYPE_CATEGORY)]
        [field: InfoBox("GamePrefab游戏种类不可用", VisibleIf = "@!gamePrefabGameTypeEnabled")]
        [field: GameType]
        [field: EnableIf(nameof(gamePrefabGameTypeEnabled))]
        [field: SerializeField]
        [JsonProperty]
        public string defaultGameType { get; private set; }

        private bool gamePrefabGameTypeEnabled =>
            baseGamePrefabType.IsDerivedFrom<IGameTypedGamePrefab>(false);

        #region JSON

        public bool ShouldSerializedefaultGameType()
        {
            return gamePrefabGameTypeEnabled;
        }

        #endregion

        #region Tools

#if UNITY_EDITOR
        public void AddDefaultGameTypeToGamePrefabWrapper(GamePrefabWrapper wrapper)
        {
            if (defaultGameType.IsNullOrWhiteSpace())
            {
                return;
            }
            
            bool isDirty = false;
            foreach (var gamePrefab in wrapper.GetGamePrefabs())
            {
                if (gamePrefab is IGameTypedGamePrefab gameTypedGamePrefab)
                {
                    if (gameTypedGamePrefab.initialGameTypesID.Contains(defaultGameType) == false)
                    {
                        gameTypedGamePrefab.initialGameTypesID.Add(defaultGameType);
                        
                        isDirty = true;
                    }
                }
            }

            if (isDirty)
            {
                wrapper.EnforceSave();
            }
        }

        [Button, TabGroup(TAB_GROUP_NAME, GAME_TYPE_CATEGORY)]
        [EnableIf(nameof(gamePrefabGameTypeEnabled))]
        private void AddDefaultGameTypeToInitialGamePrefabWrappers()
        {
            foreach (var wrapper in initialGamePrefabWrappers)
            {
                AddDefaultGameTypeToGamePrefabWrapper(wrapper);
            }
        }
#endif

        #endregion
    }
}