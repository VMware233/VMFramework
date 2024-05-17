using Newtonsoft.Json;
using Sirenix.OdinInspector;

namespace VMFramework.GameEvents
{
    public abstract partial class InputGameEventConfig : GameEventConfig
    {
        protected const string INPUT_MAPPING_CATEGORY = "输入映射设置";

        [LabelText("需要鼠标在屏幕内才触发")]
        [TabGroup(TAB_GROUP_NAME, INPUT_MAPPING_CATEGORY)]
        [JsonProperty]
        public bool requireMouseInScreen;
    }
}