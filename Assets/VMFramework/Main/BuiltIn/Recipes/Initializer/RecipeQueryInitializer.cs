using System;
using System.Collections.Generic;
using UnityEngine.Scripting;
using VMFramework.Core;
using VMFramework.GameLogicArchitecture;
using VMFramework.Procedure;

namespace VMFramework.Recipes
{
    [GameInitializerRegister(GameInitializationDoneProcedure.ID, ProcedureLoadingType.OnEnter)]
    [Preserve]
    public sealed class RecipeQueryInitializer : IGameInitializer
    {
        IEnumerable<InitializationAction> IInitializer.GetInitializationActions()
        {
            yield return new(InitializationOrder.Init, OnInit, this);
        }

        private static void OnInit(Action onDone)
        {
            foreach (var recipe in GamePrefabManager.GetAllGamePrefabs<IRecipe>())
            {
                foreach (var recipeInputQueryPattern in recipe.GetInputQueryPatterns())
                {
                    recipeInputQueryPattern.RegisterCache(recipe);
                }

                foreach (var recipeOutputQueryPattern in recipe
                             .GetOutputQueryPatterns())
                {
                    recipeOutputQueryPattern.RegisterCache(recipe);
                }
            }

            foreach (var recipeInputQueryPatternType in
                     typeof(IRecipeInputQueryPattern).GetDerivedClasses(false,
                         false))
            {
                var method = recipeInputQueryPatternType
                    .GetStaticMethodByAttribute<RecipeInputQueryHandlerAttribute>(false);

                if (method != null)
                {
                    var handler = (Func<object, IEnumerable<IRecipe>>)Delegate
                        .CreateDelegate(typeof(Func<object, IEnumerable<IRecipe>>), method);

                    RecipeQueryManager.RegisterRecipeInputQueryHandler(handler);
                }
                else
                {
                    throw new Exception(
                        $"{recipeInputQueryPatternType}没找到带有" +
                        $"{nameof(RecipeInputQueryHandlerAttribute)}的静态方法");
                }
            }

            foreach (var recipeOutputQueryPatternType in
                     typeof(IRecipeOutputQueryPattern).GetDerivedClasses(false,
                         false))
            {
                var method = recipeOutputQueryPatternType
                    .GetStaticMethodByAttribute<RecipeOutputQueryHandlerAttribute>(
                        false);

                if (method != null)
                {
                    var handler = (Func<object, IEnumerable<IRecipe>>)Delegate
                        .CreateDelegate(typeof(Func<object, IEnumerable<IRecipe>>),
                            method);

                    RecipeQueryManager.RegisterRecipeOutputQueryHandler(handler);
                }
                else
                {
                    throw new Exception(
                        $"{recipeOutputQueryPatternType}没找到带有" +
                        $"{nameof(RecipeOutputQueryHandlerAttribute)}的静态方法");
                }
            }

            onDone();
        }
    }
}