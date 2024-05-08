using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace VMFramework.Core
{
    public interface ISingleStateFSM<TID, TOwner> : IFSM<TID, TOwner> where TOwner : class
    {
        public IFSMState<TID, TOwner> currentState { get; protected set; }

        void IFSM<TID, TOwner>.Update()
        {
            if (initDone == false)
            {
                throw new System.Exception("FSM未初始化");
            }

            foreach (var state in states.Values)
            {
                if (state != currentState)
                {
                    state.OnUpdate(false);
                }
                else
                {
                    state.OnUpdate(true);
                }
            }
        }

        void IFSM<TID, TOwner>.FixedUpdate()
        {
            if (initDone == false)
            {
                throw new System.Exception("FSM未初始化");
            }

            foreach (var state in states.Values)
            {
                if (state != currentState)
                {
                    state.OnFixedUpdate(false);
                }
                else
                {
                    state.OnFixedUpdate(true);
                }
            }
        }

        bool IFSM<TID, TOwner>.EnterState(IFSMState<TID, TOwner> state)
        {
            if (currentState != null)
            {
                if (ContainsConnection(currentState, state) == false)
                {
                    throw new System.Exception(
                        $"不存在的状态转换：{currentState.id} -> {state.id}");
                }

                if (currentState.CanExit() == false)
                {
                    return false;
                }

                if (state.CanEnter() == false)
                {
                    return false;
                }

                currentState.OnExit();
            }

            currentState = state;

            currentState.OnEnter();

            return true;
        }

        bool IFSM<TID, TOwner>.HasCurrentState(TID id)
        {
            return currentState.id.Equals(id);
        }
    }

    [HideDuplicateReferenceBox]
    [HideReferenceObjectPicker]
    public class SingleStateFSM<TID, TOwner> : ISingleStateFSM<TID, TOwner> where TOwner : class
    {
        [LabelText("Owner")]
        [ShowInInspector]
        TOwner IFSM<TID, TOwner>.owner { get; set; }

        [LabelText("Init Done")]
        [ShowInInspector]
        bool IFSM<TID, TOwner>.initDone { get; set; }

        [LabelText("States")]
        [ShowInInspector]
        Dictionary<TID, IFSMState<TID, TOwner>> IFSM<TID, TOwner>.states { get; set; }

        [LabelText("Has All Connections")]
        [ShowInInspector]
        bool IFSM<TID, TOwner>.hasAllConnections { get; set; }

        [LabelText("Connections")]
        [ShowInInspector]
        Dictionary<IFSMState<TID, TOwner>, HashSet<IFSMState<TID, TOwner>>> IFSM<TID, TOwner>.connections
        {
            get;
            set;
        }

        [LabelText("Current State")]
        [ShowInInspector]
        IFSMState<TID, TOwner> ISingleStateFSM<TID, TOwner>.currentState { get; set; }
    }
}
