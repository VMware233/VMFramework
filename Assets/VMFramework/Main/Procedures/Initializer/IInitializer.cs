using System;
using System.Collections.Generic;

namespace VMFramework.Procedure
{
    public delegate void InitActionHandler(Action onDone);
    
    public interface IInitializer
    {
        public bool EnableInitializationDebugLog => true;

        public IEnumerable<InitializationAction> GetInitializationActions();
    }
}
