using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Scripting;
using Object = UnityEngine.Object;

namespace VMFramework.Procedure
{
    [GameInitializerRegister(typeof(CoreInitializationProcedure))]
    [Preserve]
    public sealed class ManagerInitializer : IGameInitializer
    {
        private static readonly List<IManagerBehaviour> _managerBehaviours = new();

        public static IReadOnlyList<IManagerBehaviour> managerBehaviours =>
            _managerBehaviours;

        async void IInitializer.OnBeforeInit(Action onDone)
        {
            ManagerCreator.CreateManagers();
            
            var allGameObjects =
                Object.FindObjectsByType<GameObject>(FindObjectsInactive.Include, FindObjectsSortMode.None);

            foreach (GameObject go in allGameObjects)
            {
                var behaviours = go.GetComponents<IManagerBehaviour>();

                if (behaviours.Length > 0)
                {
                    _managerBehaviours.AddRange(behaviours);
                }
            }
            
            int beforeInitDoneCount = 0;
            foreach (var managerBehaviour in _managerBehaviours)
            {
                managerBehaviour.OnBeforeInit(() => beforeInitDoneCount++);
            }

            await UniTask.WaitUntil(() => beforeInitDoneCount == _managerBehaviours.Count);
            
            onDone();
        }

        async void IInitializer.OnPreInit(Action onDone)
        {
            int preInitDoneCount = 0;
            foreach (var managerBehaviour in _managerBehaviours)
            {
                managerBehaviour.OnPreInit(() => preInitDoneCount++);
            }

            await UniTask.WaitUntil(() => preInitDoneCount == _managerBehaviours.Count);
            
            onDone();
        }

        async void IInitializer.OnInit(Action onDone)
        {
            int initDoneCount = 0;
            foreach (var managerBehaviour in _managerBehaviours)
            {
                managerBehaviour.OnInit(() => initDoneCount++);
            }

            await UniTask.WaitUntil(() => initDoneCount == _managerBehaviours.Count);
            
            onDone();
        }

        async void IInitializer.OnPostInit(Action onDone)
        {
            int postInitDoneCount = 0;
            foreach (var managerBehaviour in _managerBehaviours)
            {
                managerBehaviour.OnPostInit(() => postInitDoneCount++);
            }

            await UniTask.WaitUntil(() => postInitDoneCount == _managerBehaviours.Count);
            
            onDone();
        }

        async void IInitializer.OnInitComplete(Action onDone)
        {
            int initCompleteDoneCount = 0;
            foreach (var managerBehaviour in _managerBehaviours)
            {
                managerBehaviour.OnInitComplete(() => initCompleteDoneCount++);
            }

            await UniTask.WaitUntil(() => initCompleteDoneCount == _managerBehaviours.Count);
            
            onDone();
        }
    }
}
