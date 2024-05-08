using Sirenix.OdinInspector;
using VMFramework.Configuration;

namespace VMFramework.GlobalEvent
{
    public class InputActionRuntimeData : BaseConfigClass
    {
        [LabelText("已经按下的时间")]
        public float heldTime = 0;

        [LabelText("是否已经触发了按压瞬间")]
        public bool hasTriggeredHoldDown = false;
    }
}