using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using VMFramework.Core.Linq;

namespace VMFramework.GameLogicArchitecture.Editor
{
    public static class GamePrefabWrapperGameItemUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasAnyGameItem(this GamePrefabWrapper gamePrefabWrapper)
        {
            if (gamePrefabWrapper == null)
            {
                return false;
            }
            
            return gamePrefabWrapper.GetGamePrefabs().Any(gamePrefab => gamePrefab.gameItemType != null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasAnyGameItem(this IEnumerable<GamePrefabWrapper> gamePrefabWrappers)
        {
            if (gamePrefabWrappers == null)
            {
                return false;
            }
            
            return gamePrefabWrappers.Any(gamePrefabWrapper => gamePrefabWrapper.HasAnyGameItem());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Type> GetGameItemTypes(this GamePrefabWrapper gamePrefabWrapper)
        {
            if (gamePrefabWrapper == null)
            {
                return Enumerable.Empty<Type>();
            }
            
            return gamePrefabWrapper.GetGamePrefabs().Select(gamePrefab => gamePrefab?.gameItemType).WhereNotNull();
        }
    }
}