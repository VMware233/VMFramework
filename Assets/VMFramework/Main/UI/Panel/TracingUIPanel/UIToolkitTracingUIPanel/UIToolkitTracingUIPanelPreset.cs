using Newtonsoft.Json;
using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.OdinExtensions;

namespace VMFramework.UI
{
    public class UIToolkitTracingUIPanelPreset : UIToolkitPanelPreset, ITracingUIPanelPreset
    {
        protected const string TRACING_UI_SETTING_CATEGORY = "Tracing UI";

        [SuffixLabel("左下角为(0, 0)"), 
         TabGroup(TAB_GROUP_NAME, TRACING_UI_SETTING_CATEGORY, SdfIconType.Mouse, TextColor = "purple")]
        [MinValue(0), MaxValue(1)]
        [JsonProperty]
        public Vector2 defaultPivot = new(0, 1);

        [TabGroup(TAB_GROUP_NAME, TRACING_UI_SETTING_CATEGORY)]
        [JsonProperty]
        public bool enableScreenOverflow;

        [TabGroup(TAB_GROUP_NAME, TRACING_UI_SETTING_CATEGORY)]
        [HideIf(nameof(enableScreenOverflow))]
        [JsonProperty]
        public bool autoPivotCorrection = true;
        
        [TabGroup(TAB_GROUP_NAME, TRACING_UI_SETTING_CATEGORY)]
        [JsonProperty]
        public bool enableAutoMouseTracing = false;

        [TabGroup(TAB_GROUP_NAME, TRACING_UI_SETTING_CATEGORY)]
        [ToggleButtons("持续跟随", "仅跟随一次")]
        [JsonProperty]
        public bool persistentTracing = true;

        [LabelWidth(200), 
         TabGroup(TAB_GROUP_NAME, TRACING_UI_SETTING_CATEGORY)]
        [JsonProperty]
        public bool useRightPosition;

        [TabGroup(TAB_GROUP_NAME, TRACING_UI_SETTING_CATEGORY)]
        [LabelWidth(200)]
        [JsonProperty]
        public bool useTopPosition;

        [TabGroup(TAB_GROUP_NAME, TRACING_UI_SETTING_CATEGORY)]
        [VisualElementName]
        [IsNotNullOrEmpty]
        [JsonProperty]
        public string containerVisualElementName;
    }
}