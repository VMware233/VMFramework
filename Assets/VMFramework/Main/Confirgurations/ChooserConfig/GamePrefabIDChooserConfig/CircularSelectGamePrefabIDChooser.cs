using VMFramework.GameLogicArchitecture;

namespace VMFramework.Configuration
{
    public class CircularSelectGamePrefabIDChooser<TGamePrefab>
        : CircularSelectChooserConfig<GamePrefabIDConfig<TGamePrefab>, string>,
            IGamePrefabIDChooserConfig<TGamePrefab>
        where TGamePrefab : IGamePrefab
    {
        protected override string UnboxWrapper(GamePrefabIDConfig<TGamePrefab> wrapper)
        {
            return wrapper.id;
        }
    }
}