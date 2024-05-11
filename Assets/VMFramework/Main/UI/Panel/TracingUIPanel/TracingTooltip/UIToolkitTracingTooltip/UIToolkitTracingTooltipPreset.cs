using System;
using Newtonsoft.Json;
using Sirenix.OdinInspector;
using UnityEngine.UIElements;
using VMFramework.OdinExtensions;

namespace VMFramework.UI
{
    public partial class UIToolkitTracingTooltipPreset : UIToolkitTracingUIPanelPreset, ITracingTooltipPreset
    {
        protected const string TOOLTIP_SETTING_CATEGORY = "提示框设置";

        public override Type controllerType => typeof(UIToolkitTracingTooltipController);

        [LabelText("提示框优先级"), TabGroup(TAB_GROUP_NAME, TOOLTIP_SETTING_CATEGORY)]
        [JsonProperty]
        public int tooltipPriority;

        [LabelText("标题Label名称"), TabGroup(TAB_GROUP_NAME, TOOLTIP_SETTING_CATEGORY)]
        [VisualElementName(typeof(Label))]
        [IsNotNullOrEmpty]
        [JsonProperty]
        public string titleLabelName;

        [LabelText("描述Label名称"), TabGroup(TAB_GROUP_NAME, TOOLTIP_SETTING_CATEGORY)]
        [VisualElementName(typeof(Label))]
        [IsNotNullOrEmpty]
        [JsonProperty]
        public string descriptionLabelName;

        [LabelText("属性容器名称"), TabGroup(TAB_GROUP_NAME, TOOLTIP_SETTING_CATEGORY)]
        [VisualElementName]
        [IsNotNullOrEmpty]
        [JsonProperty]
        public string propertyContainerName;
    }
}