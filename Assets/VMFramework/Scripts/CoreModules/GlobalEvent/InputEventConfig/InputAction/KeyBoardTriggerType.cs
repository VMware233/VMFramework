using Sirenix.OdinInspector;
using UnityEngine;

namespace VMFramework.GlobalEvent
{
    public enum KeyBoardTriggerType
    {
        [LabelText("正在按压"), Tooltip(nameof(KeyStay))]
        KeyStay,

        [LabelText("按下瞬间"), Tooltip(nameof(KeyDown))]
        KeyDown,

        [LabelText("松开瞬间"), Tooltip(nameof(KeyUp))]
        KeyUp,

        [LabelText("正在长按"), Tooltip(nameof(OnHolding))]
        OnHolding,

        [LabelText("长按瞬间"), Tooltip(nameof(HoldDown))]
        HoldDown,

        [LabelText("长按松开后触发"), Tooltip(nameof(HoldAndRelease))]
        HoldAndRelease
    }
}