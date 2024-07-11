using System;
using System.Collections.Generic;
using VMFramework.GameLogicArchitecture;

namespace VMFramework.Recipes
{
    public abstract class Recipe : GameTypedGamePrefab, IRecipe
    {
        protected const string RECIPE_CATEGORY = "Recipe";

        protected override string idSuffix => "recipe";

        public abstract IEnumerable<IRecipeInputQueryPattern> GetInputQueryPatterns();

        public abstract IEnumerable<IRecipeOutputQueryPattern> GetOutputQueryPatterns();
    }
}