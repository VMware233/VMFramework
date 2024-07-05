#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEngine;
using VMFramework.Procedure;
using VMFramework.Procedure.Editor;

namespace VMFramework.GameEvents
{
    internal sealed class IndependentGameEventEditorInitializer : IEditorInitializer
    {
        IEnumerable<InitializationAction> IInitializer.GetInitializationActions()
        {
            yield return new(InitializationOrder.BeforeInitStart, OnBeforeInitStart, this);
        }

        private static void OnBeforeInitStart(Action onDone)
        {
            foreach (var type in IndependentGameEventCollector.Collect())
            {
                if (type.IsSealed == false)
                {
                    Debug.LogError($"{type} is not marked as sealed. This cannot ensure it is a singleton.");
                }
            }

            onDone();
        }
    }
}
#endif