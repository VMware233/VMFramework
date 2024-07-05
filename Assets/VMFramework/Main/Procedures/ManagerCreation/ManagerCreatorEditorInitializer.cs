#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using VMFramework.Procedure.Editor;

namespace VMFramework.Procedure
{
    internal sealed class ManagerCreatorEditorInitializer : IEditorInitializer
    {
        IEnumerable<InitializationAction> IInitializer.GetInitializationActions()
        {
            yield return new(InitializationOrder.BeforeInitStart, OnBeforeInitStart, this);
        }
        
        private static void OnBeforeInitStart(Action onDone)
        {
            ManagerCreator.CreateManagers();
            onDone();
        }
    }
}
#endif