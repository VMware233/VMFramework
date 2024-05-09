using System;
using VMFramework.GameLogicArchitecture;
using Newtonsoft.Json;
using Sirenix.OdinInspector;
using UnityEngine;

namespace VMFramework.GlobalEvent
{
    public class GlobalEventConfig : LocalizedGamePrefab
    {
        [LabelText("是否需要更新"), TabGroup(TAB_GROUP_NAME, BASIC_SETTING_CATEGORY)]
        [JsonProperty]
        public bool requireUpdate = false;

        [LabelText("开启触发器事件"), TabGroup(TAB_GROUP_NAME, BASIC_SETTING_CATEGORY)]
        [ShowIf(nameof(requireUpdate))]
        public bool enableTriggerEvent = false;

        [LabelText("触发器持续时间"), SuffixLabel("秒"),
         TabGroup(TAB_GROUP_NAME, BASIC_SETTING_CATEGORY)]
        [ShowIf("@" + nameof(requireUpdate) + "&&" + nameof(enableTriggerEvent))]
        [JsonProperty]
        [MinValue(0.0001)]
        public float triggerDuration = 0.2f;

        [LabelText("触发器事件不会重复触发"), TabGroup(TAB_GROUP_NAME, BASIC_SETTING_CATEGORY)]
        [ShowIf("@" + nameof(requireUpdate) + "&&" + nameof(enableTriggerEvent))]
        [JsonProperty]
        public bool triggerEventInvokeTrueOnce = false;

        [TabGroup(TAB_GROUP_NAME, RUNTIME_DATA_CATEGORY)]
        [ReadOnly, EnableGUI, DisplayAsString, ShowInInspector]
        public bool boolValue { get; private set; }

        [TabGroup(TAB_GROUP_NAME, RUNTIME_DATA_CATEGORY)]
        [ReadOnly, EnableGUI, DisplayAsString, ShowInInspector]
        public int disableCount { get; private set; }

        public bool enabled => disableCount <= 0;

        [TabGroup(TAB_GROUP_NAME, RUNTIME_DATA_CATEGORY)]
        [ReadOnly, EnableGUI, DisplayAsString, ShowInInspector]
        public bool isWaitingTriggerEnd { get; private set; }

        [TabGroup(TAB_GROUP_NAME, RUNTIME_DATA_CATEGORY)]
        [ReadOnly, EnableGUI, DisplayAsString, ShowInInspector]
        public float triggerEndTimeLeft { get; private set; }

        public event Action action;
        public event Action<bool> boolAction;
        public event Action<bool> boolTriggerAction;

        public event Action<bool> OnEnabledStateChangedEvent;

        protected override void OnInit()
        {
            base.OnInit();

            if (isDebugging)
            {
                boolAction += arg => Debug.Log($"{name}被触发:{arg}");
                boolTriggerAction += arg => Debug.Log($"{name}的Trigger事件被触发:{arg}");
            }
        }

        protected virtual bool CanUpdate()
        {
            if (isActive == false)
            {
                return false;
            }

            if (requireUpdate == false)
            {
                return false;
            }

            if (disableCount > 0)
            {
                return false;
            }

            return true;
        }

        protected virtual void OnUpdate()
        {
            if (enableTriggerEvent && isWaitingTriggerEnd)
            {
                triggerEndTimeLeft -= Time.deltaTime;

                if (triggerEndTimeLeft < 0f)
                {
                    isWaitingTriggerEnd = false;
                    InvokeTriggerAction(false);
                }
            }
        }

        public void Update()
        {
            if (CanUpdate())
            {
                OnUpdate();
            }
        }

        public void InvokeAction(bool arg)
        {
            boolValue = arg;
            boolAction?.Invoke(arg);

            if (arg)
            {
                action?.Invoke();

                if (enableTriggerEvent)
                {
                    InvokeTriggerAction(true);
                }
            }
        }

        public void InvokeTriggerAction(bool arg)
        {
            if (arg)
            {
                if (isWaitingTriggerEnd == false ||
                    triggerEventInvokeTrueOnce == false)
                {
                    boolTriggerAction?.Invoke(true);
                }

                triggerEndTimeLeft = triggerDuration;
                isWaitingTriggerEnd = true;
            }
            else
            {
                boolTriggerAction?.Invoke(false);
            }
        }

        public void EnableEvent()
        {
            if (disableCount > 0)
            {
                disableCount--;

                if (disableCount == 0)
                {
                    OnEnabledStateChangedEvent?.Invoke(true);
                }
            }
        }

        public void DisableEvent()
        {
            disableCount++;

            OnEnabledStateChangedEvent?.Invoke(false);
        }
    }
}
