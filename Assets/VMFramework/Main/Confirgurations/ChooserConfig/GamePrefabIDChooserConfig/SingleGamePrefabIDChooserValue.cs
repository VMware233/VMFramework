using VMFramework.GameLogicArchitecture;

namespace VMFramework.Configuration
{
    public sealed class SingleGamePrefabIDChooserValue<TGamePrefab>
        : SingleValueChooserConfig<GamePrefabIDConfig<TGamePrefab>, string>, IGamePrefabIDChooserConfig<TGamePrefab>
        where TGamePrefab : IGamePrefab
    {
        protected override string UnboxWrapper(GamePrefabIDConfig<TGamePrefab> wrapper)
        {
            return wrapper.id;
        }
    }
}