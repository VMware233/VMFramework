using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;
using EnumsNET;
using VMFramework.Core;

namespace VMFramework.MouseEvent
{
    internal class MouseEventConfig
    {
        [HideLabel]
        [GUIColor(nameof(GetMouseEventTypeColorGUI))]
        [EnumToggleButtons]
        public MouseEventType type;

        [LabelText("触发")]
        public event Action mouseEvent;

        [HideInInspector]
        public Action<MouseEventType> mouseEventWithType;

        public void Invoke(MouseEventType eventType)
        {
            mouseEvent?.Invoke();
            mouseEventWithType?.Invoke(eventType);
        }

        #region GUI

        private Color GetMouseEventTypeColorGUI()
        {
            switch (type)
            {
                case MouseEventType.PointerEnter:
                case MouseEventType.PointerLeave:
                case MouseEventType.PointerHover:
                    return new(1f, 0.7f, 0.7f);
                case MouseEventType.AnyMouseButtonDown:
                case MouseEventType.AnyMouseButtonUp:
                case MouseEventType.AnyMouseButtonStay:
                    return new(0.7f, 0.7f, 1);
                case MouseEventType.LeftMouseButtonDown:
                case MouseEventType.LeftMouseButtonUp:
                case MouseEventType.LeftMouseButtonClick:
                case MouseEventType.LeftMouseButtonStay:
                    return new(0.5f, 1, 1);
                case MouseEventType.RightMouseButtonDown:
                case MouseEventType.RightMouseButtonUp:
                case MouseEventType.RightMouseButtonClick:
                case MouseEventType.RightMouseButtonStay:
                    return new(0.7f, 1, 0.7f);
                case MouseEventType.MiddleMouseButtonDown:
                case MouseEventType.MiddleMouseButtonUp:
                case MouseEventType.MiddleMouseButtonClick:
                case MouseEventType.MiddleMouseButtonStay:
                    return new(1, 1, 0.5f);
                case MouseEventType.DragBegin:
                case MouseEventType.DragStay:
                case MouseEventType.DragEnd:
                    return new(1, 0.5f, 1);
                case MouseEventType.None:
                default:
                    return Color.white;
            }
        }

        #endregion
    }

    [DisallowMultipleComponent]
    public class MouseEventTrigger : MonoBehaviour
    {
        [LabelText("组名称")]
        public string groupName;

        [LabelText("检测种类")]
        [EnumToggleButtons]
        public ObjectDimensions objectDim = ObjectDimensions.THREE_D;

        [LabelText("是否开启绑定模式")]
        [PropertyTooltip("若此Trigger中的任意事件触发，会触发绑定Trigger中的对应事件")]
        public bool isBindingMode = false;

        [LabelText("要绑定的Trigger")]
        [ShowIf(nameof(isBindingMode))]
        [Required]
        public MouseEventTrigger bindTrigger;

        [LabelText("是否触发此触发器")]
        [ShowIf(nameof(isBindingMode))]
        public bool enableTriggerThis = true;

        [LabelText("是否允许拖拽")]
        public bool draggable = false;

        [LabelText("触发拖拽的键"), ShowIf(nameof(draggable))]
        public MouseButtonType dragButton = MouseButtonType.LeftButton;

        [LabelText("Debugging模式")]
        [FoldoutGroup("Only For Debugging")]
        public bool isDebugging = false;

        [LabelText("事件设置字典")]
        [ShowInInspector]
        [FoldoutGroup("Only For Debugging")]
        [ReadOnly, EnableGUI]
        private Dictionary<MouseEventType, MouseEventConfig> eventConfigsDict = new();

        public void Invoke(MouseEventType eventType)
        {
            if (isDebugging)
            {
                Debug.Log($"{name}触发了MouseEvent:{eventType}");
            }

            if (isBindingMode == false || enableTriggerThis)
            {
                if (eventConfigsDict.TryGetValue(eventType, out var config))
                {
                    config.Invoke(eventType);
                }
            }

            if (isBindingMode && bindTrigger != null)
            {
                bindTrigger.Invoke(eventType);
            }
        }

        public void AddEvent(MouseEventType eventType, Action action)
        {
            eventType.AssertIsNot(MouseEventType.None, nameof(eventType));

            foreach (var eventTypeFlag in eventType.GetFlags())
            {
                if (eventConfigsDict.ContainsKey(eventTypeFlag) == false)
                {
                    eventConfigsDict[eventTypeFlag] = new()
                    {
                        type = eventTypeFlag
                    };
                }

                var config = eventConfigsDict[eventTypeFlag];

                config.mouseEvent += action;
            }
        }

        public void AddEvent(MouseEventType eventType, Action<MouseEventType> action)
        {
            eventType.AssertIsNot(MouseEventType.None, nameof(eventType));

            foreach (var eventTypeFlag in eventType.GetFlags())
            {
                if (eventConfigsDict.ContainsKey(eventTypeFlag) == false)
                {
                    eventConfigsDict[eventTypeFlag] = new()
                    {
                        type = eventTypeFlag
                    };
                }

                var config = eventConfigsDict[eventTypeFlag];

                config.mouseEventWithType += action;
            }
        }

        public void ClearEvents()
        {
            eventConfigsDict.Clear();
        }
    }
}
