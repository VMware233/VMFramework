using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.Core;
using VMFramework.Core.FSM;

namespace VMFramework.Procedure
{
    [ManagerCreationProvider(ManagerType.ProcedureCore)]
    public sealed class ProcedureManager : UniqueMonoBehaviour<ProcedureManager>
    {
        [ShowInInspector]
        private static IMultiFSM<string, ProcedureManager> fsm = new MultiFSM<string, ProcedureManager>();

        [ShowInInspector]
        private static List<IManagerBehaviour> managerBehaviours = new();

        [ShowInInspector]
        private static Dictionary<string, IProcedure> procedures = new();

        [ShowInInspector]
        [ListDrawerSettings(ShowFoldout = false)]
        public static IReadOnlyList<string> currentProcedureIDs =>
            fsm?.currentStatesID.ToList();

        [ShowInInspector]
        private static readonly Queue<(string fromProcedureID, string toProcedureID)> procedureSwitchQueue =
            new();

        private static event Action<string> OnEnterProcedureEvent;
        private static event Action<string> OnExitProcedureEvent;

        #region Awake & Start & Update

        protected override void Awake()
        {
            base.Awake();

            Debug.Log("启动游戏！");
        }

        private void Start()
        {
            foreach (var derivedClass in typeof(IProcedure).GetDerivedClasses(false, false))
            {
                if (derivedClass.IsAbstract || derivedClass.IsInterface)
                {
                    continue;
                }

                var procedure = (IProcedure)derivedClass.CreateInstance();

                procedures.Add(procedure.id, procedure);

                procedure.OnEnterEvent += () =>
                {
                    Debug.Log($"进入流程:{procedure.id}");

                    OnEnterProcedureEvent?.Invoke(procedure.id);
                };

                procedure.OnExitEvent += () =>
                {
                    Debug.Log($"离开流程:{procedure.id}");

                    OnExitProcedureEvent?.Invoke(procedure.id);
                };

                fsm.AddState(procedure);
            }

            fsm.Init(this);

            EnterProcedure(CoreInitializationProcedure.ID);
        }

        private void Update()
        {
            if (procedureSwitchQueue.Count == 0)
            {
                return;
            }

            var (fromProcedureID, toProcedureID) = procedureSwitchQueue.Dequeue();

            if (fsm.HasCurrentState(fromProcedureID))
            {
                ExitProcedure(fromProcedureID);
            }
            
            EnterProcedure(toProcedureID);
        }

        #endregion

        #region Enter & Exit Procedure

        public static void EnterProcedure(string procedureID)
        {
            fsm.EnterState(procedureID);
        }

        public static void ExitProcedure(string procedureID)
        {
            fsm.ExitState(procedureID);
        }

        public static void AddToSwitchQueue(string fromProcedureID, string toProcedureID)
        {
            if (procedures.ContainsKey(fromProcedureID) == false)
            {
                throw new ArgumentException($"不存在的流程ID:{fromProcedureID}");
            }

            if (procedures.ContainsKey(toProcedureID) == false)
            {
                throw new ArgumentException($"不存在的流程ID:{toProcedureID}");
            }

            procedureSwitchQueue.Enqueue((fromProcedureID, toProcedureID));
        }

        #endregion

        #region Get Procedure

        public static IProcedure GetProcedure(string procedureID)
        {
            if (procedures.ContainsKey(procedureID) == false)
            {
                throw new ArgumentException($"不存在的流程ID:{procedureID}");
            }

            return procedures[procedureID];
        }

        #endregion

        #region Add Enter & Exit Events

        public static void AddOnEnterEvent(string procedureID, Action action)
        {
            GetProcedure(procedureID).OnEnterEvent += action;
        }

        public static void AddOnExitEvent(string procedureID, Action action)
        {
            GetProcedure(procedureID).OnExitEvent += action;
        }

        public static void AddOnEnterEvent(Action<string> action)
        {
            OnEnterProcedureEvent += action;
        }

        public static void AddOnExitEvent(Action<string> action)
        {
            OnExitProcedureEvent += action;
        }

        #endregion

        #region Get Name List

        private static readonly TypeCollector<IProcedure> procedureCollector = new()
        {
            includingSelf = false,
            includingAbstract = false,
            includingInterface = false,
            includingGenericDefinition = false
        };

        public static IEnumerable<ValueDropdownItem> GetNameList()
        {
            if (procedureCollector.count == 0)
            {
                procedureCollector.Collect();
            }

            foreach (var procedureType in procedureCollector.GetCollectedTypes())
            {
                var id = procedureType.GetStaticFieldValueByName<string>("ID");

                if (id.IsNullOrEmpty() == false)
                {
                    yield return new ValueDropdownItem(procedureType.Name, id);
                }
            }
        }

        #endregion
    }
}