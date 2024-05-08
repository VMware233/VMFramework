using VMFramework.GameLogicArchitecture;

namespace VMFramework.Core
{
    public interface IFSMState<TID, TOwner> : IIDOwner<TID> where TOwner : class
    {
        public bool canEnterFromAnyState { get; }

        public IFSM<TID, TOwner> fsm { get; protected set; }

        public void OnInit(IFSM<TID, TOwner> fsm);

        public void OnEnter();

        public bool CanEnter()
        {
            return true;
        }

        public void OnExit();

        public bool CanExit()
        {
            return true;
        }

        public void OnUpdate(bool isActive);

        public void OnFixedUpdate(bool isActive);

        public void OnDestroy();
    }
}
