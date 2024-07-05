using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.Core;

namespace VMFramework.Procedure
{
    public sealed class InitializerManager : IReadOnlyInitializerManager
    {
        private readonly List<IInitializer> _initializers = new();

        private readonly Dictionary<IInitializer.InitAction, InitializationAction>
            _currentPriorityLeftActions = new();

        #region Properties

        [ShowInInspector]
        public IReadOnlyList<IInitializer> initializers => _initializers;

        [ShowInInspector]
        public IReadOnlyDictionary<IInitializer.InitAction, InitializationAction>
            currentPriorityLeftActions => _currentPriorityLeftActions;

        [ShowInInspector]
        public int currentPriority { get; private set; }

        [ShowInInspector]
        public bool isInitializing { get; private set; }

        [ShowInInspector]
        public bool isInitialized { get; private set; }

        #endregion

        public void Set(IEnumerable<IInitializer> initializers)
        {
            if (isInitializing)
            {
                throw new InvalidOperationException($"Cannot reset {nameof(InitializerManager)}, " +
                                                    $"while it is still initializing.");
            }

            _initializers.Clear();
            _initializers.AddRange(initializers);

            isInitialized = false;
        }

        public async UniTask Initialize()
        {
            if (isInitializing)
            {
                throw new InvalidOperationException($"Cannot initialize {nameof(InitializerManager)}, " +
                                                    $"while it is still initializing.");
            }

            isInitialized = false;
            isInitializing = true;

            var initializersName = _initializers.Select(initializer => initializer.GetType().ToString());
            
            var initializersNameWithTag = initializersName.Select(name => name.ColorTag("green")).ToList();

            if (initializersNameWithTag.Count > 0)
            {
                var names = initializersNameWithTag.Join(", ");
                
                Debug.Log($"Initializer: {names} Started!");
            }
            
            foreach (var (priority, listOfActions) in _initializers.GetInitializationActions())
            {
                currentPriority = priority;
                _currentPriorityLeftActions.Clear();

                foreach (var actionInfo in listOfActions)
                {
                    if (_currentPriorityLeftActions.TryAdd(actionInfo.action, actionInfo))
                    {
                        continue;
                    }

                    Debug.LogError($"Duplicate initialization action: {actionInfo.action} detected in " +
                                   $"priority:{priority} which is provided by {actionInfo.initializer.GetType()}" +
                                   $"while it's already provided by " +
                                   $"{_currentPriorityLeftActions[actionInfo.action].initializer.GetType()}");
                }

                foreach (var actionInfo in listOfActions)
                {
                    if (actionInfo.initializer.enableInitializationDebugLog)
                    {
                        Debug.Log($"Initializing {actionInfo.action.Method.Name} " +
                                  $"of {actionInfo.initializer.GetType()}");
                    }
                    
                    actionInfo.action(() => _currentPriorityLeftActions.Remove(actionInfo.action));
                }

                await UniTask.WaitUntil(() => _currentPriorityLeftActions.Count == 0);
            }

            isInitializing = false;
            isInitialized = true;
        }
    }
}