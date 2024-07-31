#if UNITY_EDITOR
using Sirenix.OdinInspector;
using VMFramework.Core;
using VMFramework.Core.Editor;

namespace VMFramework.GameLogicArchitecture
{
    public partial class GamePrefabGeneralSetting
    {
        public void AddDefaultGameTypeToGamePrefabWrapper(GamePrefabWrapper wrapper)
        {
            if (DefaultGameType.IsNullOrWhiteSpace())
            {
                return;
            }
            
            bool isDirty = false;
            foreach (var gamePrefab in wrapper.GetGamePrefabs())
            {
                if (gamePrefab is IGameTypedGamePrefab gameTypedGamePrefab)
                {
                    if (gameTypedGamePrefab.InitialGameTypesID.Contains(DefaultGameType) == false)
                    {
                        gameTypedGamePrefab.InitialGameTypesID.Add(DefaultGameType);
                        
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
        [EnableIf(nameof(GamePrefabGameTypeEnabled))]
        private void AddDefaultGameTypeToInitialGamePrefabWrappers()
        {
            foreach (var wrapper in initialGamePrefabWrappers)
            {
                AddDefaultGameTypeToGamePrefabWrapper(wrapper);
            }
        }
    }
}
#endif