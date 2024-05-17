using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using VMFramework.UI;
using VMFramework.Core;
using VMFramework.Containers;
using VMFramework.GameEvents;
using VMFramework.Procedure;
using VMFramework.Property;
using VMFramework.Recipe;
using VMFramework.ResourcesManagement;
using Debug = UnityEngine.Debug;

namespace VMFramework.GameLogicArchitecture
{
    public partial class GameCoreSettingBase : UniqueMonoBehaviour<GameCoreSettingBase>
    {
        [ShowInInspector, ReadOnly]
        protected static GameCoreSettingBaseFile _gameCoreSettingsFileBase;

        public static GameCoreSettingBaseFile gameCoreSettingsFileBase
        {
            get
            {
                if (_gameCoreSettingsFileBase == null)
                {
                    LoadGameSettingFile();
                }

                return _gameCoreSettingsFileBase;
            }
        }
        
        public static ManagerCreationGeneralSetting managerCreationGeneralSetting =>
            gameCoreSettingsFileBase == null ? null : gameCoreSettingsFileBase.managerCreationGeneralSetting;

        public static GameTypeGeneralSetting gameTypeGeneralSetting =>
            gameCoreSettingsFileBase == null ? null : gameCoreSettingsFileBase.gameTypeGeneralSetting;
        
        public static GameEventGeneralSetting gameEventGeneralSetting =>
            gameCoreSettingsFileBase == null ? null : gameCoreSettingsFileBase.gameEventGeneralSetting;

        public static ColliderMouseEventGeneralSetting colliderMouseEventGeneralSetting =>
            gameCoreSettingsFileBase == null ? null : gameCoreSettingsFileBase.colliderMouseEventGeneralSetting;


        // Resources Management
        
        public static ParticleGeneralSetting particleGeneralSetting =>
            gameCoreSettingsFileBase == null ? null : gameCoreSettingsFileBase.particleGeneralSetting;

        public static TrailGeneralSetting trailGeneralSetting =>
            gameCoreSettingsFileBase == null ? null : gameCoreSettingsFileBase.trailGeneralSetting;

        public static AudioGeneralSetting audioGeneralSetting =>
            gameCoreSettingsFileBase == null ? null : gameCoreSettingsFileBase.audioGeneralSetting;
        
        public static ModelGeneralSetting modelGeneralSetting =>
            gameCoreSettingsFileBase == null ? null : gameCoreSettingsFileBase.modelGeneralSetting;

        public static SpriteGeneralSetting spriteGeneralSetting =>
            gameCoreSettingsFileBase == null ? null : gameCoreSettingsFileBase.spriteGeneralSetting;


        // Built-In Modules

        public static PropertyGeneralSetting propertyGeneralSetting =>
            gameCoreSettingsFileBase == null ? null : gameCoreSettingsFileBase.propertyGeneralSetting;

        public static TooltipPropertyGeneralSetting tooltipPropertyGeneralSetting =>
            gameCoreSettingsFileBase == null ? null : gameCoreSettingsFileBase.tooltipPropertyGeneralSetting;

        public static CameraGeneralSetting cameraGeneralSetting =>
            gameCoreSettingsFileBase == null ? null : gameCoreSettingsFileBase.cameraGeneralSetting;

        public static ContainerGeneralSetting containerGeneralSetting =>
            gameCoreSettingsFileBase == null ? null : gameCoreSettingsFileBase.containerGeneralSetting;

        public static RecipeGeneralSetting recipeGeneralSetting =>
            gameCoreSettingsFileBase == null ? null : gameCoreSettingsFileBase.recipeGeneralSetting;
        
        // UI

        public static UIPanelGeneralSetting uiPanelGeneralSetting =>
            gameCoreSettingsFileBase == null ? null : gameCoreSettingsFileBase.uiPanelGeneralSetting;

        public static UIPanelProcedureGeneralSetting uiPanelProcedureGeneralSetting =>
            gameCoreSettingsFileBase == null ? null : gameCoreSettingsFileBase.uiPanelProcedureGeneralSetting;

        public static DebugUIPanelGeneralSetting debugUIPanelGeneralSetting =>
            gameCoreSettingsFileBase == null ? null : gameCoreSettingsFileBase.debugUIPanelGeneralSetting;

        public static TracingTooltipGeneralSetting tracingTooltipGeneralSetting =>
            gameCoreSettingsFileBase == null ? null : gameCoreSettingsFileBase.tracingTooltipGeneralSetting;

        public static ContextMenuGeneralSetting contextMenuGeneralSetting =>
            gameCoreSettingsFileBase == null ? null : gameCoreSettingsFileBase.contextMenuGeneralSetting;

        public static void Init()
        {
            gameCoreSettingsFileBase.Init();
        }

        public static GameCoreSettingBaseFile LoadGameSettingFile()
        {
            _gameCoreSettingsFileBase =
                UnityEngine.Resources.Load<GameCoreSettingBaseFile>("Configurations/GameSetting");

            if (_gameCoreSettingsFileBase == null)
            {
                Debug.LogError($"未在默认路径中找到游戏总设置");
                return null;
            }

            return _gameCoreSettingsFileBase;
        }

        public static Type GetExtendedCoreSettingType()
        {
            var extendedCoreSettingType =
                typeof(GameCoreSettingBase).GetDerivedClasses(false, false).FirstOrDefault();

            extendedCoreSettingType ??= typeof(GameCoreSettingBase);

            return extendedCoreSettingType;
        }

        [Button("获取所有通用设置", ButtonStyle.Box), FoldoutGroup("Debugging")]
        public static IReadOnlyList<GeneralSettingBase> GetAllGeneralSettings()
        {
            var allGeneralSettings = new List<GeneralSettingBase>();

            var extendedCoreSettingType = GetExtendedCoreSettingType();

            if (extendedCoreSettingType != null)
            {
                foreach (var generalSetting in extendedCoreSettingType.GetAllStaticPropertyValuesByReturnType(
                             typeof(GeneralSettingBase)))
                {
                    allGeneralSettings.Add((GeneralSettingBase)generalSetting);
                }
            }

            return allGeneralSettings;
        }

        [Button("获取通用设置", ButtonStyle.Box), FoldoutGroup("Debugging")]
        public static GeneralSettingBase FindGeneralSetting(Type generalSettingType)
        {
            var extendedCoreSettingType = GetExtendedCoreSettingType();

            return (GeneralSettingBase)extendedCoreSettingType
                .GetAllStaticPropertyValuesByReturnType(generalSettingType).FirstOrDefault();
        }

        public static T FindGeneralSetting<T>(Type generalSettingType)
        {
            var extendedCoreSettingType = GetExtendedCoreSettingType();

            return (T)extendedCoreSettingType.GetAllStaticPropertyValuesByReturnType(generalSettingType)
                .FirstOrDefault();
        }

        public static T FindGeneralSetting<T>() where T : GeneralSettingBase
        {
            return (T)FindGeneralSetting(typeof(T));
        }
    }
}
