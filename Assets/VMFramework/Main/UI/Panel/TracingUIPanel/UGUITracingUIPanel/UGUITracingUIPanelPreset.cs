using System;
using Newtonsoft.Json;
using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.OdinExtensions;

namespace VMFramework.UI
{
    public class UGUITracingUIPanelPreset : UGUIPanelPreset, ITracingUIPanelPreset
    {
        protected const string TRACING_UI_SETTING_CATEGORY = "Tracing UI";

        public override Type controllerType => typeof(UGUITracingUIPanelController);

        [TabGroup(TAB_GROUP_NAME, TRACING_UI_SETTING_CATEGORY)]
        [SuffixLabel("左下角为(0, 0)")]
        [MinValue(0), MaxValue(1)]
        [JsonProperty]
        public Vector2 defaultPivot = new(0, 1);

        [TabGroup(TAB_GROUP_NAME, TRACING_UI_SETTING_CATEGORY)]
        [JsonProperty]
        public bool enableScreenOverflow = false;

        [TabGroup(TAB_GROUP_NAME, TRACING_UI_SETTING_CATEGORY)]
        [HideIf(nameof(enableScreenOverflow))]
        [JsonProperty]
        public bool autoPivotCorrection = true;

        [TabGroup(TAB_GROUP_NAME, TRACING_UI_SETTING_CATEGORY)]
        [JsonProperty]
        public bool enableAutoMouseTracing = false;
        
        [TabGroup(TAB_GROUP_NAME, TRACING_UI_SETTING_CATEGORY)]
        [JsonProperty]
        [ToggleButtons("持续跟随", "仅跟随一次")]
        [EnableIf(nameof(enableAutoMouseTracing))]
        public bool persistentTracing = true;
    }
}
