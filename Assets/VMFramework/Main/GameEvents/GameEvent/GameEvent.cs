using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.Core;
using VMFramework.GameLogicArchitecture;

namespace VMFramework.GameEvents
{
    public abstract partial class GameEvent<TGameEvent> : GameItem, IGameEvent<TGameEvent>
        where TGameEvent : GameEvent<TGameEvent>
    {
        [ShowInInspector]
        private readonly SortedDictionary<int, HashSet<Action<TGameEvent>>> callbacks = new();
        [ShowInInspector]
        private readonly Dictionary<Action<TGameEvent>, int> callbacksLookup = new();
        
        private readonly Action<TGameEvent> debugLogFunc = DebugLog;

        [ShowInInspector]
        private int disabledCount = 0;
        
        public bool isEnabled => disabledCount <= 0;

        public event IGameEvent.EnabledChangedEventHandler OnEnabledChangedEvent;

        protected override void OnCreate()
        {
            base.OnCreate();

            if (isDebugging)
            {
                AddCallback(debugLogFunc, GameEventPriority.SUPER);
            }
        }

        protected override void OnReturn()
        {
            base.OnReturn();

            bool hasExtraCallbacks = false;
            if (isDebugging)
            {
                if (callbacksLookup.Count > 1)
                {
                    hasExtraCallbacks = true;
                }
            }
            else
            {
                if (callbacksLookup.Count > 0)
                {
                    hasExtraCallbacks = true;
                }
            }

            if (hasExtraCallbacks)
            {
                Debugger.LogWarning($"{this} has extra callbacks. Callbacks Count : {callbacksLookup.Count}");
            }
        }

        private static void DebugLog(TGameEvent gameEvent)
        {
            Debugger.LogWarning($"{gameEvent} was triggered.");
        }

        public void Enable()
        {
            if (disabledCount > 0)
            {
                disabledCount--;

                if (disabledCount == 0)
                {
                    OnEnabledChangedEvent?.Invoke(false, true);
                }
            }
            else
            {
                Debug.LogError("Disabled count cannot be negative.");
            }
        }

        public void Disable()
        {
            disabledCount++;

            if (disabledCount == 1)
            {
                OnEnabledChangedEvent?.Invoke(true, false);
            }
        }
        
        public void AddCallback(Action<TGameEvent> callback, int priority)
        {
            if (callback == null)
            {
                Debug.LogError($"Cannot add null callback to {this}");
                return;
            }
            
            if (callbacks.TryGetValue(priority, out var set))
            {
                if (set.Add(callback))
                {
                    callbacksLookup.Add(callback, priority);
                    return;
                }

                var methodName = callback.Method.Name;
                Debugger.LogWarning($"Callback {methodName} already exists in {this} with priority {priority}.");
                
                return;
            }

            set = new() { callback };
            callbacks.Add(priority, set);
            callbacksLookup.Add(callback, priority);
        }
        
        public void RemoveCallback(Action<TGameEvent> callback)
        {
            if (callback == null)
            {
                Debug.LogError($"Cannot remove null callback from {this}");
                return;
            }

            if (callbacksLookup.Count == 0)
            {
                return;
            }
            
            if (callbacksLookup.TryGetValue(callback, out var priority) == false)
            {
                Debugger.LogWarning($"Callback {callback.Method.Name} does not exist in {this}");
                return;
            }
            
            callbacks[priority].Remove(callback);
            callbacksLookup.Remove(callback);
        }
    }
}