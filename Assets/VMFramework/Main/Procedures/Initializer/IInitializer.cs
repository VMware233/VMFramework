using System;
using System.Collections.Generic;

namespace VMFramework.Procedure
{
    public interface IInitializer
    {
        public bool enableInitializationDebugLog => true;
        
        public delegate void InitAction(Action onDone);

        public IEnumerable<InitializationAction> GetInitializationActions();
    }
}
