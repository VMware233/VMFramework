using Sirenix.OdinInspector;
using UnityEngine;

namespace VMFramework.GameEvents
{
    public struct InputAction
    {
        [LabelText("输入种类")]
        [EnumToggleButtons]
        public InputType type;

        [LabelText("键盘码")]
        [ShowIf(nameof(type), InputType.KeyBoardOrMouseOrJoyStick)]
        public KeyCode keyCode;

        [LabelText("键盘触发类型")]
        [ShowIf(nameof(type), InputType.KeyBoardOrMouseOrJoyStick)]
        [EnumToggleButtons]
        public KeyBoardTriggerType keyBoardTriggerType;

        [LabelText("长按时间阈值")]
        [ShowIf(nameof(DisplayHoldThresholdGUI))]
        [MinValue(0)]
        public float holdThreshold;

        private bool DisplayHoldThresholdGUI()
        {
            return type == InputType.KeyBoardOrMouseOrJoyStick &&
                   keyBoardTriggerType is KeyBoardTriggerType.OnHolding
                       or KeyBoardTriggerType.HoldDown
                       or KeyBoardTriggerType.HoldAndRelease;
        }

        public InputAction Copy()
        {
            return new InputAction()
            {
                type = type,
                keyCode = keyCode,
                keyBoardTriggerType = keyBoardTriggerType,
                holdThreshold = holdThreshold
            };
        }
    }
}