using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using VMFramework.Core;

namespace VMFramework.Procedure
{
    public static class InitializerUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<(int order, IList<InitializationAction>)> GetInitializationActions(
            this IList<IInitializer> initializers)
        {
            foreach (var initializer in initializers)
            {
                foreach (var actionInfo in initializer.GetInitializationActions())
                {
                    if (actionInfo.action == null)
                    {
                        Debug.LogError($"The action with order : {actionInfo.order} is null." +
                                       $"It's provided by {initializer.GetType()}.");
                    }
                }
            }
            
            var dict = initializers.SelectMany(initializer => initializer.GetInitializationActions())
                .BuildSortedDictionary(initializer => (initializer.order, initializer),
                    Comparer<int>.Create((x, y) => x.CompareTo(y)));

            foreach (var (order, listOfActions) in dict)
            {
                yield return (order, listOfActions.ToList());
            }
        }
    }
}