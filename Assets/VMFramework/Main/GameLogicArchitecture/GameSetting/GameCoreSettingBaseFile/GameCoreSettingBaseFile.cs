using Sirenix.OdinInspector;
using UnityEngine.Serialization;
using VMFramework.UI;
using VMFramework.Core;
using VMFramework.Containers;
using VMFramework.Core.Linq;
using VMFramework.GameEvents;
using VMFramework.Procedure;
using VMFramework.Property;
using VMFramework.Recipe;
using VMFramework.ResourcesManagement;


namespace VMFramework.GameLogicArchitecture
{
    public partial class GameCoreSettingBaseFile : GameSettingBase, IManagerCreationProvider
    {
        public const string TOOLS_CATEGORY = "工具";
        public const string EDITOR_EXTENSION_CATEGORY = "编辑器扩展";
        public const string CORE_CATEGORY = "核心";
        public const string RESOURCES_MANAGEMENT_CATEGORY = "特效";
        public const string BUILTIN_MODULE_CATEGORY = "内置模块";
        public const string UI_CATEGORY = "UI";

        public const string defaultName = "GameSetting";

        public override bool isSettingUnmovable => true;

        public override string forcedFileName => "GameSetting";
        
        // Core

        [LabelText("管理器创建设置"), TabGroup(TAB_GROUP_NAME, CORE_CATEGORY)]
        [Required]
        public ManagerCreationGeneralSetting managerCreationGeneralSetting;

        [LabelText("游戏种类设置"), TabGroup(TAB_GROUP_NAME, CORE_CATEGORY)]
        [Required]
        public GameTypeGeneralSetting gameTypeGeneralSetting;
        
        [LabelText("游戏事件通用设置"), TabGroup(TAB_GROUP_NAME, CORE_CATEGORY)]
        [Required]
        public GameEventGeneralSetting gameEventGeneralSetting;

        [FormerlySerializedAs("mouseEventGeneralSetting")]
        [LabelText("鼠标事件通用设置"), TabGroup(TAB_GROUP_NAME, CORE_CATEGORY)]
        [Required]
        public ColliderMouseEventGeneralSetting colliderMouseEventGeneralSetting;
        
        // Resources Management

        [LabelText("粒子生成器设置"), TabGroup(TAB_GROUP_NAME, RESOURCES_MANAGEMENT_CATEGORY)]
        [Required]
        public ParticleGeneralSetting particleGeneralSetting;

        [LabelText("拖尾生成器设置"), TabGroup(TAB_GROUP_NAME, RESOURCES_MANAGEMENT_CATEGORY)]
        [Required]
        public TrailGeneralSetting trailGeneralSetting;

        [LabelText("音效设置"), TabGroup(TAB_GROUP_NAME, RESOURCES_MANAGEMENT_CATEGORY)]
        [Required]
        public AudioGeneralSetting audioGeneralSetting;
        
        [LabelText("模型通用设置"), TabGroup(TAB_GROUP_NAME, RESOURCES_MANAGEMENT_CATEGORY)]
        [Required]
        public ModelGeneralSetting modelGeneralSetting;

        [LabelText("精灵通用设置"), TabGroup(TAB_GROUP_NAME, RESOURCES_MANAGEMENT_CATEGORY)]
        [Required]
        public SpriteGeneralSetting spriteGeneralSetting;

        [LabelText("属性通用设置"), TabGroup(TAB_GROUP_NAME, BUILTIN_MODULE_CATEGORY)]
        [Required]
        public PropertyGeneralSetting propertyGeneralSetting;
        
        [LabelText("提示框属性通用设置"), TabGroup(TAB_GROUP_NAME, BUILTIN_MODULE_CATEGORY)]
        [Required]
        public TooltipPropertyGeneralSetting tooltipPropertyGeneralSetting;

        [LabelText("相机通用设置"), TabGroup(TAB_GROUP_NAME, BUILTIN_MODULE_CATEGORY)]
        [Required]
        public CameraGeneralSetting cameraGeneralSetting;

        [LabelText("容器通用设置"), TabGroup(TAB_GROUP_NAME, BUILTIN_MODULE_CATEGORY)]
        [Required]
        public ContainerGeneralSetting containerGeneralSetting;

        [LabelText("配方通用设置"), TabGroup(TAB_GROUP_NAME, BUILTIN_MODULE_CATEGORY)]
        [Required]
        public RecipeGeneralSetting recipeGeneralSetting;
        
        // UI

        [LabelText("UI通用设置"), TabGroup(TAB_GROUP_NAME, UI_CATEGORY)]
        [Required]
        public UIPanelGeneralSetting uiPanelGeneralSetting;
        
        [LabelText("UI面板流程通用设置"), TabGroup(TAB_GROUP_NAME, UI_CATEGORY)]
        [Required]
        public UIPanelProcedureGeneralSetting uiPanelProcedureGeneralSetting;

        [LabelText("Debug UI面板通用设置"), TabGroup(TAB_GROUP_NAME, UI_CATEGORY)]
        [Required]
        public DebugUIPanelGeneralSetting debugUIPanelGeneralSetting;

        [LabelText("追踪提示框通用设置"), TabGroup(TAB_GROUP_NAME, UI_CATEGORY)]
        [Required]
        public TracingTooltipGeneralSetting tracingTooltipGeneralSetting;

        [LabelText("上下文菜单通用设置"), TabGroup(TAB_GROUP_NAME, UI_CATEGORY)]
        [Required]
        public ContextMenuGeneralSetting contextMenuGeneralSetting;

        protected override void OnInit()
        {
            base.OnInit();

            CheckSettings();

            var generalSettings = GameCoreSettingBase.GetAllGeneralSettings();

            generalSettings.Examine(setting => setting.PreInit());
            generalSettings.Examine(setting => setting.Init());
            generalSettings.Examine(setting => setting.PostInit());
            generalSettings.Examine(setting => setting.FinishInit());
        }

        public override void CheckSettings()
        {
            foreach (var propertyInfo in GameCoreSettingBase.GetExtendedCoreSettingType()
                         .GetAllStaticPropertiesByReturnType(typeof(GeneralSettingBase)))
            {
                var generalSetting = propertyInfo.GetValue(null) as GeneralSettingBase;

                generalSetting.AssertIsNotNull(propertyInfo.Name);

                generalSetting.ResetInitializationState();

                generalSetting.CheckSettings();
            }
        }

        #region Manager Creation Provider

        void IManagerCreationProvider.HandleManagerCreation()
        {
            ManagerCreatorContainers.GetOrCreateManagerTypeContainer(ManagerType.SettingCore.ToString())
                .GetOrAddComponent(GameCoreSettingBase.GetExtendedCoreSettingType());
        }

        #endregion
    }
}
