using System.Collections.Generic;
using System.Linq;
using VMFramework.Containers;

namespace VMFramework.Recipes
{
    public readonly struct RecipeInputQueryIDPattern : IRecipeInputQueryPattern
    {
        public readonly string id;

        public RecipeInputQueryIDPattern(string id)
        {
            this.id = id;
        }

        #region Cache

        private static readonly Dictionary<string, HashSet<IRecipe>> cache = new();

        void IRecipeInputQueryPattern.RegisterCache(IRecipe recipe)
        {
            cache.TryAdd(id, new());
            cache[id].Add(recipe);
        }

        [RecipeInputQueryHandler]
        private static IEnumerable<IRecipe> GetRecipes(object item)
        {
            if (item is IContainerItem containerItem)
            {
                if (cache.TryGetValue(containerItem.id, out var recipes))
                {
                    return recipes;
                }
            }

            return Enumerable.Empty<IRecipe>();
        }

        #endregion
    }
}
