using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using VMFramework.Core;

namespace VMFramework.GameLogicArchitecture
{
    public static class EmptyGamePrefabs
    {
        private static readonly Dictionary<Type, IGamePrefab> cacheByType = new();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGamePrefab Get(Type type)
        {
            if (type == null)
            {
                return null;
            }
            
            if (cacheByType.TryGetValue(type, out var prefab))
            {
                return prefab;
            }

            prefab = type.CreateInstance() as IGamePrefab;

            if (prefab == null)
            {
                return null;
            }
            
            cacheByType.Add(type, prefab);
            return prefab;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGet(Type type, out IGamePrefab prefab)
        {
            if (type == null)
            {
                prefab = null;
                return false;
            }
            
            if (cacheByType.TryGetValue(type, out prefab))
            {
                return true;
            }

            prefab = type.CreateInstance() as IGamePrefab;
            
            if (prefab == null)
            {
                return false;
            }
            
            cacheByType.Add(type, prefab);
            return true;
        }
    }
}