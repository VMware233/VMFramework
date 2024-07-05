#if UNITY_EDITOR
using System;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;
using VMFramework.Core;
using VMFramework.Core.Editor;
using VMFramework.Core.Linq;
using VMFramework.Localization;

namespace VMFramework.GameLogicArchitecture.Editor
{
    public static class GamePrefabWrapperCreator
    {
        public static event Action<GamePrefabWrapper> OnGamePrefabWrapperCreated;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static IGamePrefab CreateDefaultGamePrefab(string id, Type gamePrefabType)
        {
            if (gamePrefabType == null)
            {
                throw new ArgumentNullException(nameof(gamePrefabType));
            }

            if (gamePrefabType.TryCreateInstance() is not IGamePrefab gamePrefab)
            {
                throw new Exception($"Could not create instance of {gamePrefabType.Name}.");
            }

            gamePrefab.id = id;

            return gamePrefab;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GamePrefabWrapper CreateGamePrefabWrapper(string id, Type gamePrefabType,
            GamePrefabWrapperType wrapperType)
        {
            var gamePrefab = CreateDefaultGamePrefab(id, gamePrefabType);

            return CreateGamePrefabWrapper(gamePrefab, wrapperType);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GamePrefabWrapper CreateGamePrefabWrapper(IGamePrefab gamePrefab,
            GamePrefabWrapperType wrapperType)
        {
            if (gamePrefab.TryGetGamePrefabGeneralSettingWithWarning(out var gamePrefabSetting) == false)
            {
                return null;
            }

            string path = gamePrefabSetting.gamePrefabFolderPath;

            if (path.EndsWith("/") == false)
            {
                path += "/";
            }

            path += gamePrefab.id.ToPascalCase();

            return CreateGamePrefabWrapper(path, gamePrefab, wrapperType);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GamePrefabWrapper CreateGamePrefabWrapper(string path, string id, Type gamePrefabType,
            GamePrefabWrapperType wrapperType)
        {
            var gamePrefab = CreateDefaultGamePrefab(id, gamePrefabType);

            return CreateGamePrefabWrapper(path, gamePrefab, wrapperType);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GamePrefabWrapper CreateGamePrefabWrapper(string path, IGamePrefab gamePrefab,
            GamePrefabWrapperType wrapperType)
        {
            gamePrefab.id.AssertIsNotNull(nameof(gamePrefab.id));

            if (gamePrefab.id.IsEmptyAfterTrim())
            {
                throw new ArgumentException($"{nameof(gamePrefab.id)} ID cannot be empty or whitespace.");
            }

            if (gamePrefab is ILocalizedStringOwnerConfig localizedStringOwner)
            {
                localizedStringOwner.AutoConfigureLocalizedString(default);
            }

            return wrapperType switch
            {
                GamePrefabWrapperType.Single => CreateGamePrefabWrapper<GamePrefabSingleWrapper>(path,
                    gamePrefab),
                GamePrefabWrapperType.Multiple => CreateGamePrefabWrapper<GamePrefabMultipleWrapper>(path,
                    gamePrefab),
                _ => throw new ArgumentOutOfRangeException(nameof(wrapperType), wrapperType, null)
            };
        }

        private static TWrapper CreateGamePrefabWrapper<TWrapper>(string path,
            params IGamePrefab[] gamePrefabs)
            where TWrapper : GamePrefabWrapper
        {
            if (PreCheckPath(path) == false)
            {
                return null;
            }

            if (gamePrefabs.Length == 0)
            {
                Debug.LogError($"No {nameof(IGamePrefab)} provided to create {typeof(TWrapper).Name}.");
                return null;
            }

            if (gamePrefabs.IsAllNull())
            {
                Debug.LogError(
                    $"All {nameof(IGamePrefab)} provided to create {typeof(TWrapper).Name} are null.");
                return null;
            }

            var gamePrefabWrapper = path.CreateScriptableObjectAsset<TWrapper>();

            if (gamePrefabWrapper == null)
            {
                Debug.LogError($"Could not create {typeof(TWrapper).Name} " +
                               $"of {gamePrefabs.Join(", ")} on Path : {path}");
                return null;
            }

            gamePrefabWrapper.InitGamePrefabs(gamePrefabs);

            foreach (var gamePrefab in gamePrefabWrapper.GetGamePrefabs())
            {
                if (gamePrefab.TryGetGamePrefabGeneralSettingWithWarning(out var gamePrefabSetting))
                {
                    gamePrefabSetting.AddDefaultGameTypeToGamePrefabWrapper(gamePrefabWrapper);
                }
            }

            gamePrefabWrapper.EnforceSave();
            OnGamePrefabWrapperCreated?.Invoke(gamePrefabWrapper);
            return gamePrefabWrapper;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool PreCheckPath(string path)
        {
            if (path.EndsWith(".asset") == false)
            {
                path += ".asset";
            }

            AssetDatabase.Refresh();

            if (path.ExistsAsset())
            {
                Debug.LogWarning($"{nameof(GamePrefabWrapper)} already exists at {path}.");
                return false;
            }

            path.CreateFolderByAssetPath();

            return true;
        }
    }
}
#endif