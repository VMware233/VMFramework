using VMFramework.Core;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace VMFramework.Procedure
{
    public abstract class LoadingProcedure : ProcedureBase
    { 
        public abstract string nextProcedureID { get; }
        
        public sealed override void OnEnter()
        {
            base.OnEnter();

            _ = OnEnterLoading();
        }

        protected virtual async UniTask OnEnterLoading()
        {
            List<IGameInitializer> initializers = new();

            foreach (var derivedClass in typeof(IGameInitializer).GetDerivedClasses(
                         false, false))
            {
                if (derivedClass.TryGetAttribute<GameInitializerRegister>(false,
                        out var register) == false)
                {
                    continue;
                }

                if (register.ProcedureType != GetType())
                {
                    continue;
                }

                if (derivedClass.CreateInstance() is IGameInitializer initializer)
                {
                    initializers.Add(initializer);
                }
            }

            foreach (var initializer in initializers)
            {
                Debug.Log($"初始化器 {initializer} 开始初始化");
            }
            
            int beforeInitDoneCount = 0;
            foreach (var initializer in initializers)
            {
                initializer.OnBeforeInit(() => beforeInitDoneCount++);
            }

            await UniTask.WaitUntil(() => beforeInitDoneCount == initializers.Count);

            int preInitDoneCount = 0;
            foreach (var initializer in initializers)
            {
                initializer.OnPreInit(() => preInitDoneCount++);
            }

            await UniTask.WaitUntil(() => preInitDoneCount == initializers.Count);

            int initDoneCount = 0;
            foreach (var initializer in initializers)
            {
                initializer.OnInit(() => initDoneCount++);
            }

            await UniTask.WaitUntil(() => initDoneCount == initializers.Count);

            int postInitDoneCount = 0;
            foreach (var initializer in initializers)
            {
                initializer.OnPostInit(() => postInitDoneCount++);
            }

            await UniTask.WaitUntil(() => postInitDoneCount == initializers.Count);

            int initCompleteDoneCount = 0;
            foreach (var initializer in initializers)
            {
                initializer.OnInitComplete(() => initCompleteDoneCount++);
            }

            await UniTask.WaitUntil(() => initCompleteDoneCount == initializers.Count);
        }

        protected void EnterNextProcedure()
        {
            ProcedureManager.AddToSwitchQueue(id, nextProcedureID);
        }
    }
}
