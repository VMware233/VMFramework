using System;
using System.Collections.Generic;
using VMFramework.Core;
using Sirenix.OdinInspector;
using UnityEngine.Scripting;
using VMFramework.GameLogicArchitecture;
using VMFramework.Procedure;

namespace VMFramework.Recipes
{
    [ManagerCreationProvider(ManagerType.OtherCore)]
    public sealed class RecipeQueryManager : ManagerBehaviour<RecipeQueryManager>
    {
        [ShowInInspector]
        public static List<Func<object, IEnumerable<IRecipe>>> recipeInputQueryHandlers =
            new();

        [ShowInInspector]
        public static List<Func<object, IEnumerable<IRecipe>>> recipeOutputQueryHandlers =
            new();

        public static void RegisterRecipeInputQueryHandler(
            Func<object, IEnumerable<IRecipe>> handler)
        {
            recipeInputQueryHandlers.Add(handler);
        }

        public static void RegisterRecipeOutputQueryHandler(
            Func<object, IEnumerable<IRecipe>> handler)
        {
            recipeOutputQueryHandlers.Add(handler);
        }

        public static IEnumerable<IRecipe> GetRecipesByInput(object item)
        {
            foreach (var handler in recipeInputQueryHandlers)
            {
                foreach (var recipe in handler(item))
                {
                    yield return recipe;
                }
            }
        }

        public static IEnumerable<IRecipe> GetRecipesByOutput(object item)
        {
            foreach (var handler in recipeOutputQueryHandlers)
            {
                foreach (var recipe in handler(item))
                {
                    yield return recipe;
                }
            }
        }
    }
}
