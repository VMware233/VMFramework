using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Sirenix.OdinInspector;
using UnityEngine;

namespace VMFramework.GlobalEvent
{
    public sealed partial class InputEventConfigOfFloatArg : InputEventConfig
    {
        [LabelText("是否输入传入Axis"), TabGroup(TAB_GROUP_NAME, INPUT_MAPPING_SETTING_CATEGORY)]
        [JsonProperty]
        public bool isFloatFromAxis = false;

        [HideLabel, TabGroup(TAB_GROUP_NAME, INPUT_MAPPING_SETTING_CATEGORY)]
        [ShowIf(nameof(isFloatFromAxis))]
        [JsonProperty]
        public InputAxisType floatInputAxisType;

#if UNITY_EDITOR
        [LabelText("正值动作组"), TabGroup(TAB_GROUP_NAME, INPUT_MAPPING_SETTING_CATEGORY)]
        [HideIf(nameof(isFloatFromAxis))]
        [ListDrawerSettings(CustomAddFunction = nameof(AddActionGroupGUI))]
#endif
        [JsonProperty]
        public List<InputActionGroup> floatPositiveActionGroups = new();

#if UNITY_EDITOR
        [LabelText("负值动作组"), TabGroup(TAB_GROUP_NAME, INPUT_MAPPING_SETTING_CATEGORY)]
        [HideIf(nameof(isFloatFromAxis))]
        [ListDrawerSettings(CustomAddFunction = nameof(AddActionGroupGUI))]
#endif
        [JsonProperty]
        public List<InputActionGroup> floatNegativeActionGroups = new();

        [TabGroup(TAB_GROUP_NAME, RUNTIME_DATA_CATEGORY)]
        [ReadOnly, EnableGUI, DisplayAsString, NonSerialized, ShowInInspector]
        public float floatValue = 0;

        public event Action<float> floatAction;

        protected override void OnInit()
        {
            base.OnInit();

            if (isDebugging)
            {
                floatAction += arg => Debug.Log($"{name}被触发:{arg}");
            }
        }

        public void InvokeAction(float arg)
        {
            floatValue = arg;

            InvokeAction(arg != 0);

            floatAction?.Invoke(arg);
        }

        protected override void OnUpdate()
        {
            base.OnUpdate();

            float argFloat;

            if (isFloatFromAxis)
            {
                argFloat = floatInputAxisType.GetAxisValue();
            }
            else
            {
                argFloat = 0;

                if (CheckGroups(floatPositiveActionGroups))
                {
                    argFloat += 1;
                }

                if (CheckGroups(floatNegativeActionGroups))
                {
                    argFloat -= 1;
                }
            }

            InvokeAction(argFloat);
        }
    }
}
