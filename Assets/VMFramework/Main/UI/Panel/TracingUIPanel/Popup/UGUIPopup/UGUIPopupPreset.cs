using System;
using Sirenix.OdinInspector;
using VMFramework.Configuration.Animation;

namespace VMFramework.UI
{
    public partial class UGUIPopupPreset : UGUITracingUIPanelPreset, IPopupPreset
    {
        protected const string POPUP_SETTING_CATEGORY = "弹出UI设置";

        public override Type controllerType => typeof(UGUIPopupController);

        [LabelText("弹出UI的容器名称"), TabGroup(TAB_GROUP_NAME, POPUP_SETTING_CATEGORY)]
        [ValueDropdown(nameof(GetPrefabChildrenNames))]
        public string popupContainerName;

        [LabelText("是否启用容器动画"), TabGroup(TAB_GROUP_NAME, POPUP_SETTING_CATEGORY)]
        public bool enableContainerAnimation = false;

        [LabelText("拆分容器动画"), TabGroup(TAB_GROUP_NAME, POPUP_SETTING_CATEGORY)]
        [EnableIf(nameof(enableContainerAnimation))]
        [Indent]
        public bool splitContainerAnimation = false;

        [LabelText("容器动画"), TabGroup(TAB_GROUP_NAME, POPUP_SETTING_CATEGORY)]
        [EnableIf(nameof(enableContainerAnimation))]
        [HideIf(nameof(splitContainerAnimation))]
        [Indent]
        public GameObjectAnimation containerAnimation = new();

        [LabelText("是否容器动画结束后自动关闭"), TabGroup(TAB_GROUP_NAME, POPUP_SETTING_CATEGORY)]
        [EnableIf(nameof(enableContainerAnimation))]
        [HideIf(nameof(splitContainerAnimation))]
        [Indent]
        public bool autoCloseAfterContainerAnimation = true;

        [LabelText("开始容器动画"), TabGroup(TAB_GROUP_NAME, POPUP_SETTING_CATEGORY)]
        [EnableIf(nameof(enableContainerAnimation))]
        [ShowIf(nameof(splitContainerAnimation))]
        [Indent]
        public GameObjectAnimation startContainerAnimation = new();

        [LabelText("结束容器动画"), TabGroup(TAB_GROUP_NAME, POPUP_SETTING_CATEGORY)]
        [EnableIf(nameof(enableContainerAnimation))]
        [ShowIf(nameof(splitContainerAnimation))]
        [Indent]
        public GameObjectAnimation endContainerAnimation = new();

        public override void CheckSettings()
        {
            base.CheckSettings();
            
            containerAnimation.CheckSettings();
            startContainerAnimation.CheckSettings();
            endContainerAnimation.CheckSettings();
        }

        protected override void OnInit()
        {
            base.OnInit();

            containerAnimation.Init();
            startContainerAnimation.Init();
            endContainerAnimation.Init();
        }
    }
}
