using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace VMFramework.GameEvents
{
    public class SingletonGameEvent<TGameEvent> where TGameEvent : SingletonGameEvent<TGameEvent>, new()
    {
        protected static readonly TGameEvent instance;
        
        [ShowInInspector]
        private static readonly SortedDictionary<int, HashSet<Action<TGameEvent>>> callbacks = new();
        [ShowInInspector]
        private static readonly Dictionary<Delegate, int> callbacksLookup = new();

        [ShowInInspector]
        private static int disabledCount = 0;
        
        public static bool isEnabled => disabledCount <= 0;

        public static event IGameEvent.EnabledChangedEventHandler OnEnabledChangedEvent;
        
        static SingletonGameEvent()
        {
            instance = new TGameEvent();
        }

        #region Enabled

        public static void Enable()
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

        public static void Disable()
        {
            disabledCount++;

            if (disabledCount == 1)
            {
                OnEnabledChangedEvent?.Invoke(true, false);
            }
        }

        #endregion

        #region Callback

        public static void AddCallback(Action<TGameEvent> callback, int priority = GameEventPriority.TINY)
        {
            if (callback == null)
            {
                Debug.LogError($"Cannot add null callback to {typeof(TGameEvent)}");
                return;
            }
            
            if (callbacks.TryGetValue(priority, out var set))
            {
                if (set.Add(callback) == false)
                {
                    callbacksLookup.Add(callback, priority);
                    return;
                }

                var methodName = callback.Method.Name;
                Debug.LogWarning($"Callback {methodName} already exists in {typeof(TGameEvent)} with priority {priority}.");
                
                return;
            }

            set = new() { callback };
            callbacks.Add(priority, set);
            callbacksLookup.Add(callback, priority);
        }
        
        public static void RemoveCallback(Action<TGameEvent> callback)
        {
            if (callback == null)
            {
                Debug.LogError($"Cannot remove null callback from {typeof(TGameEvent)}");
                return;
            }
            
            if (callbacksLookup.TryGetValue(callback, out var priority) == false)
            {
                Debug.LogWarning($"Callback {callback.Method.Name} does not exist in {typeof(TGameEvent)}");
                return;
            }
            
            callbacks[priority].Remove(callback);
            callbacksLookup.Remove(callback);
        }

        #endregion

        #region Propagate

        private static bool isPropagationStopped = false;
        
        public static void StopPropagation()
        {
            isPropagationStopped = true;
        }

        protected virtual bool CanPropagate()
        {
            if (isEnabled == false)
            {
                Debug.LogWarning($"{typeof(TGameEvent)} is disabled. Cannot propagate.");
                return false;
            }
            
            return true;
        }

        [Button]
        public static void Propagate()
        {
            if (instance.CanPropagate() == false)
            {
                return;
            }
            
            isPropagationStopped = false;

            foreach (var set in callbacks.Values)
            {
                foreach (var callback in set)
                {
                    callback(instance);
                }
                
                if (isPropagationStopped)
                {
                    break;
                }
            }
            
            instance.OnPropagationStopped();
        }

        protected virtual void OnPropagationStopped()
        {
            
        }

        #endregion
    }
}