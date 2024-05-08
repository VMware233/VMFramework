using System;
using VMFramework.Core;

namespace VMFramework.Procedure
{
    public interface IProcedure : IFSMState<string, ProcedureManager>
    {
        public event Action OnEnterEvent, OnExitEvent;
    }
}
