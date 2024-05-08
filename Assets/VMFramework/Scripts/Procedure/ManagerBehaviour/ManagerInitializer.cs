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

        async void IInitializer.OnPreInit(Action onDone)
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
            foreach (var initializer in _managerBehaviours)
            {
                initializer.OnBeforeInit(() => beforeInitDoneCount++);
            }

            await UniTask.WaitUntil(() => beforeInitDoneCount == _managerBehaviours.Count);
            
            int preInitDoneCount = 0;
            foreach (var initializer in _managerBehaviours)
            {
                initializer.OnPreInit(() => preInitDoneCount++);
            }

            await UniTask.WaitUntil(() => preInitDoneCount == _managerBehaviours.Count);

            int initDoneCount = 0;
            foreach (var initializer in _managerBehaviours)
            {
                initializer.OnInit(() => initDoneCount++);
            }

            await UniTask.WaitUntil(() => initDoneCount == _managerBehaviours.Count);

            int postInitDoneCount = 0;
            foreach (var initializer in _managerBehaviours)
            {
                initializer.OnPostInit(() => postInitDoneCount++);
            }

            await UniTask.WaitUntil(() => postInitDoneCount == _managerBehaviours.Count);

            int initCompleteDoneCount = 0;
            foreach (var initializer in _managerBehaviours)
            {
                initializer.OnInitComplete(() => initCompleteDoneCount++);
            }

            await UniTask.WaitUntil(() => initCompleteDoneCount == _managerBehaviours.Count);
            
            onDone();
        }
    }
}
