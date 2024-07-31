using System;
using System.Collections.Generic;
using VMFramework.Core;
using Sirenix.OdinInspector;
using VMFramework.Procedure;

namespace VMFramework.Timers
{
    [ManagerCreationProvider(ManagerType.TimerCore)]
    public sealed class UpdateDelegateManager : ManagerBehaviour<UpdateDelegateManager>
    {
        [ShowInInspector]
        public static int fixedUpdateEventCount = 0;

        [ShowInInspector]
        public static int updateEventCount = 0;

        [ShowInInspector]
        public static int lateUpdateEventCount = 0;

        [ShowInInspector]
        public static int onGUIEventCount = 0;

        public static event Action OnFixedUpdateEvent;
        public static event Action OnUpdateEvent;
        public static event Action OnLateUpdateEvent;
        public static event Action OnGUIEvent;

        [ShowInInspector]
        private static HashSet<Action> _allFixedUpdateDelegates = new();

        [ShowInInspector]
        private static HashSet<Action> _allUpdateDelegates = new();

        [ShowInInspector]
        private static HashSet<Action> _allLateUpdateDelegates = new();

        [ShowInInspector]
        private static HashSet<Action> _allOnGUIDelegates = new();

        public static bool HasUpdateDelegate(UpdateType type, Action action)
        {
            switch (type)
            {
                case UpdateType.FixedUpdate:
                    return _allFixedUpdateDelegates.Contains(action);
                case UpdateType.Update:
                    return _allUpdateDelegates.Contains(action);
                case UpdateType.LateUpdate:
                    return _allLateUpdateDelegates.Contains(action);
                case UpdateType.OnGUI:
                    return _allOnGUIDelegates.Contains(action);
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        public static void AddUpdateDelegate(UpdateType type, Action action)
        {
            switch (type)
            {
                case UpdateType.FixedUpdate:
                    OnFixedUpdateEvent += action;
                    _allFixedUpdateDelegates.Add(action);
                    fixedUpdateEventCount++;
                    break;
                case UpdateType.Update:
                    OnUpdateEvent += action;
                    _allUpdateDelegates.Add(action);
                    updateEventCount++;
                    break;
                case UpdateType.LateUpdate:
                    OnLateUpdateEvent += action;
                    _allLateUpdateDelegates.Add(action);
                    lateUpdateEventCount++;
                    break;
                case UpdateType.OnGUI:
                    OnGUIEvent += action;
                    _allOnGUIDelegates.Add(action);
                    onGUIEventCount++;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        public static void RemoveUpdateDelegate(UpdateType type, Action action)
        {
            switch (type)
            {
                case UpdateType.FixedUpdate:
                    OnFixedUpdateEvent -= action;
                    _allFixedUpdateDelegates.Remove(action);
                    fixedUpdateEventCount++;
                    break;
                case UpdateType.Update:
                    OnUpdateEvent -= action;
                    _allUpdateDelegates.Remove(action);
                    updateEventCount++;
                    break;
                case UpdateType.LateUpdate:
                    OnLateUpdateEvent -= action;
                    _allLateUpdateDelegates.Remove(action);
                    lateUpdateEventCount++;
                    break;
                case UpdateType.OnGUI:
                    OnGUIEvent -= action;
                    _allOnGUIDelegates.Remove(action);
                    onGUIEventCount++;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        private void FixedUpdate()
        {
            OnFixedUpdateEvent?.Invoke();
        }

        private void Update()
        {
            OnUpdateEvent?.Invoke();
        }

        private void LateUpdate()
        {
            OnLateUpdateEvent?.Invoke();
        }

        private void OnGUI()
        {
            OnGUIEvent?.Invoke();
        }
    }
}
