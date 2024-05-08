using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace VMFramework.Core
{
    public interface IFSM<TID, TOwner> where TOwner : class
    {
        public TOwner owner { get; protected set; }

        public bool initDone { get; protected set; }

        protected Dictionary<TID, IFSMState<TID, TOwner>> states { get; set; }

        public bool hasAllConnections { get; protected set; }

        protected Dictionary<IFSMState<TID, TOwner>, HashSet<IFSMState<TID, TOwner>>> connections
        {
            get;
            set;
        }

        public void Init(TOwner owner)
        {
            InitFSM(owner);
        }

        protected void InitFSM(TOwner owner)
        {
            if (initDone)
            {
                throw new System.Exception("FSM已经初始化");
            }

            this.owner = owner;

            foreach (var state in states.Values)
            {
                state.OnInit(this);
            }

            initDone = true;
        }

        public void Update();

        public void FixedUpdate();

        public void AddState(IFSMState<TID, TOwner> fsmState)
        {
            if (initDone)
            {
                throw new System.Exception("FSM已经初始化");
            }

            states ??= new();

            if (states.ContainsKey(fsmState.id))
            {
                throw new System.Exception("重复的状态ID：" + fsmState.id);
            }

            states.Add(fsmState.id, fsmState);
        }

        public void AddConnection(TID fromID, TID toID)
        {
            if (hasAllConnections)
            {
                return;
            }

            connections ??= new();

            if (states.ContainsKey(fromID) == false)
            {
                throw new System.Exception("不存在的状态ID：" + fromID);
            }

            if (states.ContainsKey(toID) == false)
            {
                throw new System.Exception("不存在的状态ID：" + toID);
            }

            var fromState = states[fromID];
            var toState = states[toID];

            if (connections.ContainsKey(fromState) == false)
            {
                connections.Add(fromState, new());
            }

            connections[fromState].Add(toState);
        }

        public void AddAllConnections()
        {
            hasAllConnections = true;
        }

        public bool ContainsConnection(TID fromID, TID toID)
        {
            if (hasAllConnections)
            {
                return true;
            }

            if (states.TryGetValue(fromID, out var fromState) == false)
            {
                Debug.LogWarning("不存在的状态ID：" + fromID);
                return false;
            }

            if (states.TryGetValue(toID, out var toState) == false)
            {
                Debug.LogWarning("不存在的状态ID：" + toID);
                return false;
            }

            return ContainsConnection(fromState, toState);
        }

        public bool ContainsConnection(IFSMState<TID, TOwner> fromState, IFSMState<TID, TOwner> toState)
        {
            if (hasAllConnections)
            {
                return true;
            }

            if (connections == null)
            {
                return false;
            }

            if (toState.canEnterFromAnyState)
            {
                return true;
            }

            if (connections.TryGetValue(fromState, out var toStateSet) == false)
            {
                return false;
            }

            return toStateSet.Contains(toState);
        }

        public bool EnterState(TID id)
        {
            if (initDone == false)
            {
                throw new System.Exception("FSM未初始化");
            }

            if (states.TryGetValue(id, out var nextState) == false)
            {
                throw new System.Exception("不存在的状态ID：" + id);
            }

            return EnterState(nextState);
        }

        public bool EnterState(IFSMState<TID, TOwner> state);

        public bool HasCurrentState(IFSMState<TID, TOwner> state)
        {
            return HasCurrentState(state.id);
        }

        public bool HasCurrentState(TID id);

        public TState GetState<TState>(TID id) where TState : IFSMState<TID, TOwner>
        {
            if (states.TryGetValue(id, out var state))
            {
                return (TState)state;
            }

            return default;
        }

        public IEnumerable<TState> GetAllStates<TState>() where TState : IFSMState<TID, TOwner>
        {
            return states.Values.Cast<TState>();
        }
    }
}
