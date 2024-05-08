using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace VMFramework.Core
{
    public interface IMultiStateFSM<TID, TOwner> : IFSM<TID, TOwner> where TOwner : class
    {
        public HashSet<IFSMState<TID, TOwner>> currentStates { get; protected set; }

        public IEnumerable<TID> currentStatesID
        {
            get
            {
                if (currentStates == null)
                {
                    return Enumerable.Empty<TID>();
                }

                return currentStates.Select(state => state.id);
            }
        }

        void IFSM<TID, TOwner>.Init(TOwner owner)
        {
            InitFSM(owner);
            InitMultiStateFSM(owner);
        }

        protected void InitMultiStateFSM(TOwner owner)
        {
            currentStates = new();
        }

        void IFSM<TID, TOwner>.Update()
        {
            if (initDone == false)
            {
                throw new Exception("FSM未初始化");
            }

            foreach (var state in states.Values)
            {
                if (currentStates.Contains(state))
                {
                    state.OnUpdate(true);
                }
                else
                {
                    state.OnUpdate(false);
                }
            }
        }

        void IFSM<TID, TOwner>.FixedUpdate()
        {
            if (initDone == false)
            {
                throw new Exception("FSM未初始化");
            }

            foreach (var state in states.Values)
            {
                if (currentStates.Contains(state))
                {
                    state.OnFixedUpdate(true);
                }
                else
                {
                    state.OnFixedUpdate(false);
                }
            }
        }

        bool IFSM<TID, TOwner>.EnterState(IFSMState<TID, TOwner> state)
        {
            if (state.CanEnter() == false)
            {
                return false;
            }

            if (currentStates.Add(state))
            {
                state.OnEnter();

                return true;
            }
            else
            {
                Debug.LogWarning("状态已经存在：" + state.id);

                return false;
            }
        }

        public bool ExitState(TID id)
        {
            if (initDone == false)
            {
                throw new Exception("FSM未初始化");
            }

            if (states.TryGetValue(id, out var currentState) == false)
            {
                throw new Exception("不存在的状态ID：" + id);
            }

            return ExitState(currentState);
        }

        public bool ExitState(IFSMState<TID, TOwner> state)
        {
            if (state.CanExit() == false)
            {
                return false;
            }

            if (currentStates.Remove(state))
            {
                state.OnExit();

                return true;
            }

            Debug.LogWarning("状态不存在：" + state.id);
            return false;
        }

        public bool EnterState(TID fromID, TID toID)
        {
            if (initDone == false)
            {
                throw new Exception("FSM未初始化");
            }

            if (states.TryGetValue(fromID, out var fromState) == false)
            {
                throw new Exception("不存在的状态ID：" + fromID);
            }

            if (states.TryGetValue(toID, out var toState) == false)
            {
                throw new Exception("不存在的状态ID：" + toID);
            }

            if (currentStates.Contains(fromState) == false)
            {
                Debug.LogWarning("不存在的状态ID：" + fromID);
                return false;
            }

            if (currentStates.Contains(toState))
            {
                Debug.LogWarning("状态已经存在：" + toID);
                return false;
            }

            if (ContainsConnection(fromState, toState) == false)
            {
                throw new Exception(
                    $"不存在的状态转换：{fromState.id} -> {toState.id}");
            }

            if (fromState.CanExit() == false)
            {
                return false;
            }

            if (toState.CanEnter() == false)
            {
                return false;
            }

            if (ExitState(fromState))
            {
                if (EnterState(toState))
                {
                    return true;
                }
            }

            return false;
        }

        bool IFSM<TID, TOwner>.HasCurrentState(TID id)
        {
            return currentStatesID.Contains(id);
        }
    }

    public class MultiStateFSM<TID, TOwner> : IMultiStateFSM<TID, TOwner> where TOwner : class
    {
        [LabelText("拥有者")]
        [ShowInInspector]
        TOwner IFSM<TID, TOwner>.owner { get; set; }

        [LabelText("初始化完成")]
        [ShowInInspector]
        bool IFSM<TID, TOwner>.initDone { get; set; }

        [LabelText("状态列表")]
        [ShowInInspector]
        Dictionary<TID, IFSMState<TID, TOwner>> IFSM<TID, TOwner>.states { get; set; }

        [LabelText("是否有所有状态的连接")]
        [ShowInInspector]
        bool IFSM<TID, TOwner>.hasAllConnections { get; set; }

        [LabelText("状态转换列表")]
        [ShowInInspector]
        Dictionary<IFSMState<TID, TOwner>, HashSet<IFSMState<TID, TOwner>>> IFSM<TID, TOwner>.connections
        {
            get;
            set;
        }

        [LabelText("当前状态")]
        [ShowInInspector]
        HashSet<IFSMState<TID, TOwner>> IMultiStateFSM<TID, TOwner>.currentStates { get; set; }
    }
}
