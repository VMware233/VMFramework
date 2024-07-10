using VMFramework.GameLogicArchitecture;

namespace VMFramework.Configuration
{
    public class WeightedSelectGamePrefabIDChooserConfig<TGamePrefab>
        : WeightedSelectChooserConfig<GamePrefabIDConfig<TGamePrefab>, string>,
            IGamePrefabIDChooserConfig<TGamePrefab>
        where TGamePrefab : IGamePrefab
    {
        protected override string UnboxWrapper(GamePrefabIDConfig<TGamePrefab> wrapper)
        {
            return wrapper.id;
        }
    }
}