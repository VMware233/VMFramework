using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Sirenix.OdinInspector;
using UnityEngine;

namespace VMFramework.GlobalEvent
{
    public sealed partial class InputEventConfigOfVector2Arg : InputEventConfig
    {
        [LabelText("参数模值不超过1"), TabGroup(TAB_GROUP_NAME, INPUT_MAPPING_SETTING_CATEGORY)]
        [JsonProperty]
        public bool argMagnitudeLessThan1 = true;

        [LabelText("是否输入传入Axis给X值"), TabGroup(TAB_GROUP_NAME, INPUT_MAPPING_SETTING_CATEGORY)]
        [JsonProperty]
        public bool isVector2XFromAxis = false;

        [HideLabel, TabGroup(TAB_GROUP_NAME, INPUT_MAPPING_SETTING_CATEGORY)]
        [ShowIf(nameof(isVector2XFromAxis))]
        [JsonProperty]
        public InputAxisType vector2XInputAxisType;

#if UNITY_EDITOR
        [LabelText("X正值动作组"), TabGroup(TAB_GROUP_NAME, INPUT_MAPPING_SETTING_CATEGORY)]
        [ListDrawerSettings(CustomAddFunction = nameof(AddActionGroupGUI))]
        [HideIf(nameof(isVector2XFromAxis))]
#endif
        [JsonProperty]
        public List<InputActionGroup> vector2XPositiveActionGroups = new();

#if UNITY_EDITOR
        [LabelText("X负值动作组"), TabGroup(TAB_GROUP_NAME, INPUT_MAPPING_SETTING_CATEGORY)]
        [ListDrawerSettings(CustomAddFunction = nameof(AddActionGroupGUI))]
        [HideIf(nameof(isVector2XFromAxis))]
#endif
        [JsonProperty]
        public List<InputActionGroup> vector2XNegativeActionGroups = new();

        [LabelText("是否输入传入Axis给Y值"), TabGroup(TAB_GROUP_NAME, INPUT_MAPPING_SETTING_CATEGORY)]
        [JsonProperty]
        public bool isVector2YFromAxis = false;

        [HideLabel, TabGroup(TAB_GROUP_NAME, INPUT_MAPPING_SETTING_CATEGORY)]
        [ShowIf(nameof(isVector2YFromAxis))]
        [JsonProperty]
        public InputAxisType vector2YInputAxisType;

#if UNITY_EDITOR
        [LabelText("Y正值动作组"), TabGroup(TAB_GROUP_NAME, INPUT_MAPPING_SETTING_CATEGORY)]
        [ListDrawerSettings(CustomAddFunction = nameof(AddActionGroupGUI))]
        [HideIf(nameof(isVector2YFromAxis))]
#endif
        [JsonProperty]
        public List<InputActionGroup> vector2YPositiveActionGroups = new();

#if UNITY_EDITOR
        [LabelText("Y负值动作组"), TabGroup(TAB_GROUP_NAME, INPUT_MAPPING_SETTING_CATEGORY)]
        [ListDrawerSettings(CustomAddFunction = nameof(AddActionGroupGUI))]
        [HideIf(nameof(isVector2YFromAxis))]
#endif
        [JsonProperty]
        public List<InputActionGroup> vector2YNegativeActionGroups = new();

        [TabGroup(TAB_GROUP_NAME, RUNTIME_DATA_CATEGORY)]
        [ReadOnly, EnableGUI, DisplayAsString, NonSerialized, ShowInInspector]
        public Vector2 vector2Value = Vector2.zero;

        public event Action<Vector2> vector2Action;

        protected override void OnInit()
        {
            base.OnInit();

            if (isDebugging)
            {
                vector2Action += arg => Debug.Log($"{name}被触发:{arg}");
            }
        }

        public void InvokeAction(Vector2 arg)
        {
            if (arg == Vector2.zero)
            {
                InvokeAction(false);
            }
            else
            {
                if (argMagnitudeLessThan1 && arg.sqrMagnitude > 1)
                {
                    arg = arg.normalized;
                }

                InvokeAction(true);
            }

            vector2Value = arg;

            vector2Action?.Invoke(arg);
        }

        protected override void OnUpdate()
        {
            base.OnUpdate();

            Vector2 argVector2 = Vector2.zero;

            if (isVector2XFromAxis)
            {
                argVector2.x = vector2XInputAxisType.GetAxisValue();
            }
            else
            {
                if (CheckGroups(vector2XPositiveActionGroups))
                {
                    argVector2.x += 1;
                }

                if (CheckGroups(vector2XNegativeActionGroups))
                {
                    argVector2.x -= 1;
                }
            }

            if (isVector2YFromAxis)
            {
                argVector2.y = vector2YInputAxisType.GetAxisValue();
            }
            else
            {
                if (CheckGroups(vector2YPositiveActionGroups))
                {
                    argVector2.y += 1;
                }

                if (CheckGroups(vector2YNegativeActionGroups))
                {
                    argVector2.y -= 1;
                }
            }

            InvokeAction(argVector2);
        }
    }
}
