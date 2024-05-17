using Sirenix.OdinInspector;
using UnityEngine;

namespace VMFramework.GameEvents
{
    public enum KeyBoardTriggerType
    {
        /// <summary>
        /// 正在按压
        /// </summary>
        [LabelText("正在按压"), Tooltip(nameof(KeyStay))]
        KeyStay,

        /// <summary>
        /// 按下瞬间
        /// </summary>
        [LabelText("按下瞬间"), Tooltip(nameof(KeyDown))]
        KeyDown,

        /// <summary>
        /// 松开瞬间
        /// </summary>
        [LabelText("松开瞬间"), Tooltip(nameof(KeyUp))]
        KeyUp,
        
        /// <summary>
        /// 正在长按
        /// </summary>
        [LabelText("正在长按"), Tooltip(nameof(OnHolding))]
        OnHolding,

        /// <summary>
        /// 长按瞬间
        /// </summary>
        [LabelText("长按瞬间"), Tooltip(nameof(HoldDown))]
        HoldDown,

        /// <summary>
        /// 长按松开后触发
        /// </summary>
        [LabelText("长按松开后触发"), Tooltip(nameof(HoldAndRelease))]
        HoldAndRelease
    }
}