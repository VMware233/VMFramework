using System;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using Sirenix.OdinInspector;
using UnityEngine.UIElements;
using VMFramework.Core;
using VMFramework.GameLogicArchitecture;
using VMFramework.OdinExtensions;

namespace VMFramework.UI
{
    public class UIToolkitPanelPreset : UIPanelPreset, IUIToolkitUIPanelPreset
    {
        public const string UI_TOOLKIT_PANEL_CATEGORY = "UI Toolkit面板设置";

        public override Type controllerType => typeof(UIToolkitPanelController);

        public PanelSettings panelSettings
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                if (useDefaultPanelSettings)
                {
                    return GameCoreSettingBase.uiPanelGeneralSetting.GetPanelSetting(sortingOrder);
                }

                return customPanelSettings;
            }
        }

        [LabelText("UI模板")]
        [SuffixLabel("UXML文件")]
        [TabGroup(TAB_GROUP_NAME, UI_TOOLKIT_PANEL_CATEGORY, SdfIconType.ColumnsGap, TextColor = "red")]
        [Required]
        public VisualTreeAsset visualTree;

        [LabelText("使用默认的面板设置")]
        [TabGroup(TAB_GROUP_NAME, UI_TOOLKIT_PANEL_CATEGORY)]
        [JsonProperty]
        public bool useDefaultPanelSettings = true;

        [LabelText("自定义的面板设置")]
        [TabGroup(TAB_GROUP_NAME, UI_TOOLKIT_PANEL_CATEGORY)]
        [HideIf(nameof(useDefaultPanelSettings))]
        [Required]
        public PanelSettings customPanelSettings;

        [LabelText("所有节点忽略鼠标事件")]
        [TabGroup(TAB_GROUP_NAME, UI_TOOLKIT_PANEL_CATEGORY)]
        [JsonProperty]
        public bool ignoreMouseEvents;

        [LabelText("关闭按钮名称")]
        [TabGroup(TAB_GROUP_NAME, UI_TOOLKIT_PANEL_CATEGORY)]
        [VisualElementName(typeof(Button), typeof(BasicButton))]
        [JsonProperty]
        public string closeUIButtonName;

        public override void CheckSettings()
        {
            base.CheckSettings();

            visualTree.AssertIsNotNull(nameof(visualTree));

            if (useDefaultPanelSettings == false)
            {
                customPanelSettings.AssertIsNotNull(nameof(customPanelSettings));
            }
        }

        #region Interface Implementation

        VisualTreeAsset IUIToolkitUIPanelPreset.visualTree => visualTree;

        #endregion
    }
}