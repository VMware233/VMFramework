#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.Core;

namespace VMFramework.GameLogicArchitecture
{
    public static class GamePrefabGeneralSettingUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GamePrefabGeneralSetting GetGamePrefabGeneralSetting(Type gamePrefabType)
        {
            foreach (var generalSetting in GlobalSettingCollector.GetAllGeneralSettings())
            {
                if (generalSetting is not GamePrefabGeneralSetting gamePrefabSetting)
                {
                    continue;
                }

                if (gamePrefabType.IsDerivedFrom(gamePrefabSetting.baseGamePrefabType, true))
                {
                    return gamePrefabSetting;
                }
            }
            
            return null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetGamePrefabGeneralSetting(Type gamePrefabType,
            out GamePrefabGeneralSetting gamePrefabSetting)
        {
            gamePrefabSetting = GetGamePrefabGeneralSetting(gamePrefabType);
            return gamePrefabSetting != null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GamePrefabGeneralSetting GetGamePrefabGeneralSetting(this IGamePrefab gamePrefab)
        {
            if (gamePrefab == null)
            {
                return null;
            }
            
            return GetGamePrefabGeneralSetting(gamePrefab.GetType());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetGamePrefabGeneralSetting(this IGamePrefab gamePrefab,
            out GamePrefabGeneralSetting gamePrefabSetting)
        {
            gamePrefabSetting = GetGamePrefabGeneralSetting(gamePrefab);
            return gamePrefabSetting != null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetGamePrefabGeneralSettingWithWarning(this IGamePrefab gamePrefab,
            out GamePrefabGeneralSetting gamePrefabSetting)
        {
            if (TryGetGamePrefabGeneralSetting(gamePrefab, out gamePrefabSetting))
            {
                return true;
            }
            
            Debug.LogError(
                $"Could not find {nameof(GamePrefabGeneralSetting)} for {gamePrefab?.GetType()}.");
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GamePrefabGeneralSetting> GetGamePrefabGeneralSettings(
            this IEnumerable<IGamePrefab> gamePrefabs)
        {
            int count = 0;
            
            foreach (var gamePrefab in gamePrefabs)
            {
                if (TryGetGamePrefabGeneralSettingWithWarning(gamePrefab, out var gamePrefabSetting))
                {
                    yield return gamePrefabSetting;
                }
                
                count++;
            }

            if (count == 0)
            {
                Debug.LogWarning($"{nameof(gamePrefabs)} is empty!");
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GamePrefabGeneralSetting> GetGamePrefabGeneralSettings(
            this GamePrefabWrapper gamePrefabWrapper)
        {
            if (gamePrefabWrapper == null)
            {
                return Enumerable.Empty<GamePrefabGeneralSetting>();
            }

            return gamePrefabWrapper.GetGamePrefabs().GetGamePrefabGeneralSettings();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<ValueDropdownItem> GetGamePrefabGeneralSettingNameList()
        {
            foreach (var generalSetting in GlobalSettingCollector.GetAllGeneralSettings())
            {
                if (generalSetting is not GamePrefabGeneralSetting gamePrefabSetting)
                {
                    continue;
                }

                yield return new ValueDropdownItem(gamePrefabSetting.gamePrefabName, gamePrefabSetting);
            }
        }
    }
}
#endif