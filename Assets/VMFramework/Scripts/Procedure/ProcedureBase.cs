using System;
using VMFramework.Core;

namespace VMFramework.Procedure
{
    public abstract class ProcedureBase : IProcedure
    {
        protected IFSM<string, ProcedureManager> fsm { get; private set; }

        public abstract string id { get; }
        
        public bool canEnterFromAnyState { get; private set; }
        
        public event Action OnEnterEvent;
        public event Action OnExitEvent;
        
        IFSM<string, ProcedureManager> IFSMState<String, ProcedureManager>.fsm
        {
            get => fsm;
            set => fsm = value;
        }

        public virtual void OnInit(IFSM<string, ProcedureManager> fsm)
        {

        }

        public virtual void OnEnter()
        {
            OnEnterEvent?.Invoke();
        }

        public virtual void OnExit()
        {
            OnExitEvent?.Invoke();
        }

        void IFSMState<string, ProcedureManager>.OnUpdate(bool isActive)
        {
            
        }

        void IFSMState<string, ProcedureManager>.OnFixedUpdate(bool isActive)
        {
            
        }

        public virtual void OnDestroy()
        {

        }
    }
}
