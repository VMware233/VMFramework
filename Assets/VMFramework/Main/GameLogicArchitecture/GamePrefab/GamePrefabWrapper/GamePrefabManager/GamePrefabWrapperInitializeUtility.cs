#if UNITY_EDITOR
using System;
using System.Linq;
using UnityEngine;
using VMFramework.Core;
using VMFramework.Core.Editor;
using VMFramework.Core.Linq;

namespace VMFramework.GameLogicArchitecture.Editor
{
    public static class GamePrefabWrapperInitializeUtility
    {
        public static event Action OnGamePrefabWrappersRefresh;

        public static void Refresh()
        {
            GamePrefabManager.Clear();
            
            foreach (var wrapper in GamePrefabWrapperQuery.GetAllGamePrefabWrappers())
            {
                foreach (var gamePrefab in wrapper.GetGamePrefabs())
                {
                    if (gamePrefab == null)
                    {
                        continue;
                    }

                    if (gamePrefab.id.IsNullOrEmpty())
                    {
                        Debug.LogWarning($"{wrapper.name}中存在未设置ID的GamePrefab，请检查。" +
                                         $"路径为:{wrapper.GetAssetPath()}", wrapper);
                        continue;
                    }
                    
                    GamePrefabManager.RegisterGamePrefab(gamePrefab);
                    
                    gamePrefab.OnIDChangedEvent += OnGamePrefabIDChanged;
                }
            }
            
            RemoveEmptyGamePrefabWrappers();
            
            OnGamePrefabWrappersRefresh?.Invoke();
        }

        private static void RemoveEmptyGamePrefabWrappers()
        {
            foreach (var wrapper in GamePrefabWrapperQuery.GetAllGamePrefabWrappers())
            {
                var gamePrefabs = wrapper.GetGamePrefabs().ToList();

                if (gamePrefabs.IsNullOrEmptyOrAllNull())
                {
                    wrapper.DeleteAsset();
                }
            }
        }

        public static void CreateAutoRegisterGamePrefabs()
        {
            foreach (var gamePrefabType in typeof(IGamePrefab).GetDerivedClasses(false, false))
            {
                if (gamePrefabType.IsAbstract || gamePrefabType.IsInterface)
                {
                    continue;
                }

                foreach (var gamePrefabAutoRegisterAttribute in gamePrefabType
                             .GetAttributes<GamePrefabAutoRegisterAttribute>(false))
                {
                    var id = gamePrefabAutoRegisterAttribute.ID;

                    if (GamePrefabManager.TryGetGamePrefab(id, out var existedGamePrefab))
                    {
                        if (existedGamePrefab.GetType() != gamePrefabType)
                        {
                            Debug.LogWarning($"ID为{id}的GamePrefab已经存在，但类型不匹配，请检查。" +
                                             $"需要自动注册的类型为{gamePrefabType}，" +
                                             $"但已存在的类型为{existedGamePrefab.GetType()}。");
                        }
                        
                        continue;
                    }
                    
                    var wrapper = GamePrefabWrapperCreator.CreateGamePrefabWrapper(id, gamePrefabType);

                    if (wrapper == null)
                    {
                        continue;
                    }
                    
                    foreach (var gamePrefab in wrapper.GetGamePrefabs())
                    {
                        if (gamePrefab is IGamePrefabAutoRegisterProvider autoRegisterProvider)
                        {
                            autoRegisterProvider.OnGamePrefabAutoRegister();
                        }
                    }
                    
                    wrapper.EnforceSave();
                }
                
                Refresh();
            }
        }

        private static void OnGamePrefabIDChanged(IGamePrefab gamePrefab, string oldID, string newID)
        {
            GamePrefabManager.UnregisterGamePrefab(gamePrefab);
            GamePrefabManager.RegisterGamePrefab(gamePrefab);
        }
    }
}

#endif