using System;
using UnityEngine;
using VMFramework.Core;
using VMFramework.Procedure;
using VMFramework.Procedure.Editor;

namespace VMFramework.GameEvents
{
    internal sealed class IndependentGameEventEditorInitializer : IEditorInitializer
    {
        void IInitializer.OnPreInit(Action onDone)
        {
            foreach (var derivedClass in typeof(IndependentGameEvent<>).GetDerivedClasses(true, true))
            {
                if (derivedClass.IsGenericType)
                {
                    continue;
                }

                if (derivedClass.IsAbstract)
                {
                    continue;
                }

                if (derivedClass.IsSealed == false)
                {
                    Debug.LogError(
                        $"{derivedClass} is not marked as sealed. This cannot ensure it is a singleton.");
                }
            }
            
            onDone();
        }
    }
}