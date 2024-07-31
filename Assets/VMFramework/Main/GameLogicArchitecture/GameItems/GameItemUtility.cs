using System.Runtime.CompilerServices;

namespace VMFramework.GameLogicArchitecture
{
    public static class GameItemUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TGameItem GetGameItem<TGameItem>(this IGamePrefab gamePrefab)
            where TGameItem : IGameItem
        {
            return GameItemManager.Get<TGameItem>(gamePrefab.id);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGameItem GetGameItem(this IGamePrefab gamePrefab)
        {
            return GameItemManager.Get(gamePrefab.id);
        }
    }
}