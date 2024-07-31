#if UNITY_EDITOR
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using VMFramework.Core.Editor;
using VMFramework.Core.Linq;

namespace VMFramework.GameLogicArchitecture.Editor
{
    public static class GamePrefabWrapperScriptOpenUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void OpenGamePrefabScripts(this GamePrefabWrapper gamePrefabWrapper)
        {
            if (gamePrefabWrapper == null)
            {
                Debug.LogError($"{nameof(gamePrefabWrapper)} is null! Cannot open {nameof(GamePrefab)} scripts!");
                return;
            }

            gamePrefabWrapper.GetGamePrefabs().OpenScriptOfObjects();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void OpenGameItemScripts(this GamePrefabWrapper gamePrefabWrapper)
        {
            if (gamePrefabWrapper == null)
            {
                Debug.LogError($"{nameof(gamePrefabWrapper)} is null! Cannot open {nameof(GameItem)} scripts!");
                return;
            }

            gamePrefabWrapper.GetGameItemTypes().Examine(gameItemType => gameItemType.OpenScriptOfType());
        }
    }
}
#endif