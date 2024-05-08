using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.Core;
using VMFramework.GameLogicArchitecture;
using VMFramework.Containers;
using VMFramework.Procedure;

namespace VMFramework.UI
{
    [ManagerCreationProvider(ManagerType.UICore)]
    public class ContainerUIManager : ManagerBehaviour<ContainerUIManager>
    {
        #region Init

        protected override void OnBeforeInit()
        {
            base.OnBeforeInit();

            UIPanelManager.OnPanelCreatedEvent += uiPanelController =>
            {
                if (uiPanelController is IContainerUIPanel)
                {
                    uiPanelController.OnOpenInstantlyEvent += OnOpenContainerUIPriorityRegistered;
                    uiPanelController.OnCloseInstantlyEvent += OnCloseContainerUIPriorityUnregistered;
                }
            };
        }

        #endregion

        #region Binder

        public static readonly Dictionary<string, string> containerBinder = new();

        public static void BindContainerUITo(string containerUIPanelID, string containerID)
        {
            if (GamePrefabManager.TryGetGamePrefab<UIPanelPreset>(containerUIPanelID, out var panelPreset) ==
                false)
            {
                Debug.LogWarning($"未找到ID为{containerUIPanelID}的{nameof(UIPanelPreset)}");
                return;
            }

            if (containerBinder.ContainsKey(containerID))
            {
                Debug.LogWarning($"已经存在ID为容器UI:{containerUIPanelID}" + $"到容器:{containerID}的绑定，将覆盖旧的绑定");
            }

            containerBinder[containerID] = containerUIPanelID;
        }

        public static bool TryGetContainerUI(string containerID, out string containerUIPanelID)
        {
            return containerBinder.TryGetValue(containerID, out containerUIPanelID);
        }

        #endregion

        #region Open Container UI & Close

        public static IUIPanelController OpenUI(IContainer container)
        {
            container.AssertIsNotNull(nameof(container));

            if (containerBinder.TryGetValue(container.id, out var containerUIPanelID) == false)
            {
                Debug.LogWarning($"未找到ID为{container.id}的容器绑定的UI");
                return null;
            }

            var containerUIPanel = UIPanelPool.GetUniquePanelStrictly<IContainerUIPanel>(containerUIPanelID);

            containerUIPanel.Open();
            containerUIPanel.SetBindContainer(container);

            return containerUIPanel;
        }

        public static void CloseUI(IContainer container)
        {
            container.AssertIsNotNull(nameof(container));

            if (containerBinder.TryGetValue(container.id, out var containerUIPanelID) == false)
            {
                Debug.LogWarning($"未找到ID为{container.id}的容器绑定的UI");
                return;
            }

            var containerUIPanel = UIPanelPool.GetUniquePanelStrictly<IContainerUIPanel>(containerUIPanelID);

            containerUIPanel.Close();
        }

        #endregion

        #region Open Container Owner & Close

        public static void OpenContainerOwner(IContainerOwner owner)
        {
            foreach (var container in owner.GetContainers())
            {
                OpenUI(container);
            }
        }

        public static void CloseContainerOwner(IContainerOwner owner)
        {
            foreach (var container in owner.GetContainers())
            {
                CloseUI(container);
            }
        }

        #endregion

        #region Container UI Priority

        [ShowInInspector]
        private static Dictionary<int, IContainerUIPanel> containerUIPriorityDict = new();

        public static void OnOpenContainerUIPriorityRegistered(IUIPanelController uiPanelController)
        {
            if (uiPanelController is not IContainerUIPanel containerUIPanel)
            {
                return;
            }

            containerUIPriorityDict[containerUIPanel.containerUIPriority] = containerUIPanel;
        }

        public static void OnCloseContainerUIPriorityUnregistered(IUIPanelController uiPanelController)
        {
            if (uiPanelController is not IContainerUIPanel containerUIPanel)
            {
                return;
            }

            if (containerUIPriorityDict.TryGetValue(containerUIPanel.containerUIPriority,
                    out var existedContainerUIPanel))
            {
                if (existedContainerUIPanel == containerUIPanel)
                {
                    containerUIPriorityDict.Remove(containerUIPanel.containerUIPriority);
                }
            }
        }

        [Button]
        public static IContainerUIPanel GetHighestPriorityContainerUIPanel()
        {
            if (containerUIPriorityDict.Count == 0)
            {
                return null;
            }

            var highestPriority = containerUIPriorityDict.Keys.Max();

            return containerUIPriorityDict[highestPriority];
        }

        [Button]
        public static IContainerUIPanel GetSecondHighestPriorityContainerUIPanel()
        {
            if (containerUIPriorityDict.Count == 0)
            {
                return null;
            }

            var (highestPriority, secondHighestPriority) = containerUIPriorityDict.Keys.TwoMaxValues();

            return containerUIPriorityDict[secondHighestPriority];
        }

        #endregion
    }
}