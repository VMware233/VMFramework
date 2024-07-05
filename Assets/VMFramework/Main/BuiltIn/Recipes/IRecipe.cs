using System.Collections.Generic;
using VMFramework.GameLogicArchitecture;

namespace VMFramework.Recipes
{
    public interface IRecipe : IGameTypedGamePrefab
    {
        public IEnumerable<IRecipeInputQueryPattern> GetInputQueryPatterns();

        public IEnumerable<IRecipeOutputQueryPattern> GetOutputQueryPatterns();
    }
}