using System;
using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.Core;
using VMFramework.GameLogicArchitecture;
using VMFramework.OdinExtensions;
using VMFramework.Procedure;

namespace VMFramework.UI
{
    [ManagerCreationProvider(ManagerType.UICore)]
    public sealed class UIPanelManager : ManagerBehaviour<UIPanelManager>
    {
        private static UIPanelGeneralSetting setting => UISetting.uiPanelGeneralSetting;

        public static Transform uiContainer { get; private set; }

        public static event Action<IUIPanelController> OnPanelCreatedEvent;

        #region Init

        protected override void OnBeforeInitStart()
        {
            base.OnBeforeInitStart();

            uiContainer = setting.container;
        }

        #endregion

        [Button]
        public static IUIPanelController RecreateUniquePanel(
            [GamePrefabID(typeof(IUIPanelPreset))] string presetID)
        {
            if (GamePrefabManager.GetGamePrefabStrictly<IUIPanelPreset>(presetID).isUnique == false)
            {
                throw new Exception($"{presetID}不是Unique的UIPanel");
            }

            if (UIPanelPool.TryGetUniquePanel(presetID, out var controller) == false)
            {
                return null;
            }

            var newPanel = CreatePanel(presetID);

            controller.OnRecreate(newPanel);

            controller.Destruct();

            return newPanel;
        }

        public static IUIPanelController CreatePanel(IUIPanelPreset preset)
        {
            if (preset == null)
            {
                Debug.LogError($"preset is null, cannot create panel!");
                return null;
            }
            
            Debug.Log($"Creating panel with preset:{preset}");
            
            var uiGameObject = new GameObject(preset.name);

            if (uiGameObject.AddComponent(preset.controllerType) is not IUIPanelController newUIPanel)
            {
                throw new Exception($"Failed to add component." +
                                    $"Because the controllerType of {preset} " +
                                    $"is not inherited from {nameof(IUIPanelController)}.");
            }

            uiGameObject.transform.SetParent(setting.container);

            newUIPanel.Init(preset);
            
            UIPanelPool.Register(newUIPanel);

            newUIPanel.CloseInstantly(null);
            newUIPanel.PostClose(null);

            newUIPanel.OnCreate();

            OnPanelCreatedEvent?.Invoke(newUIPanel);

            return newUIPanel;
        }

        [Button]
        public static IUIPanelController CreatePanel(
            [GamePrefabID(typeof(IUIPanelPreset))] string presetID)
        {
            presetID.AssertIsNotNullOrEmpty(nameof(presetID));

            var preset = GamePrefabManager.GetGamePrefabStrictly<IUIPanelPreset>(presetID);

            return CreatePanel(preset);
        }

        [Button]
        public static IUIPanelController GetClosedOrCreatePanel(
            [GamePrefabID(typeof(IUIPanelPreset))] string presetID)
        {
            if (UIPanelPool.TryGetUniquePanel(presetID, out var result))
            {
                return result;
            }

            if (UIPanelPool.TryGetClosedPanel(presetID, out result))
            {
                return result;
            }
            
            return CreatePanel(presetID);
        }
        
        public static T GetClosedOrCreatePanel<T>(string presetID) where T : IUIPanelController
        {
            return (T)GetClosedOrCreatePanel(presetID);
        }

        [Button]
        private static void GetClosedOrCreatePanelAndOpen(
            [GamePrefabID(typeof(IUIPanelPreset))] string presetID)
        {
            var newPanel = GetClosedOrCreatePanel(presetID);

            newPanel.Open();
        }
    }
}