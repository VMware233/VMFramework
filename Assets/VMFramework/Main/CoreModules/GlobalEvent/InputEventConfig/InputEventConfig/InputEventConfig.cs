using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.Core;

namespace VMFramework.GlobalEvent
{
    public partial class InputEventConfig : GlobalEventConfig
    {
        protected const string INPUT_MAPPING_SETTING_CATEGORY = "输入映射设置";

        [LabelText("需要鼠标在屏幕内才触发")]
        [TabGroup(TAB_GROUP_NAME, INPUT_MAPPING_SETTING_CATEGORY)]
        [JsonProperty]
        public bool requireMouseInScreen;

        public override void CheckSettings()
        {
            base.CheckSettings();

            requireUpdate = true;
        }

        public virtual IEnumerable<string> GetInputMappingContent(KeyCodeUtility.KeyCodeToStringMode mode)
        {
            yield return "";
        }

        protected override bool CanUpdate()
        {
            if (base.CanUpdate() == false)
            {
                return false;
            }

            if (requireMouseInScreen)
            {
                if (Input.mousePosition.x.BetweenInclusive(0, Screen.width) == false ||
                    Input.mousePosition.y.BetweenInclusive(0, Screen.height) == false)
                {
                    return false;
                }
            }

            return true;
        }

        #region Check Group

        protected static bool CheckGroups(List<InputActionGroup> groups)
        {
            if (groups == null || groups.Count == 0)
            {
                return false;
            }

            foreach (var group in groups)
            {
                if (CheckGroup(group))
                {
                    return true;
                }
            }

            return false;
        }

        protected static bool CheckGroup(InputActionGroup group)
        {
            if (group.actions == null || group.actions.Count == 0)
            {
                return false;
            }

            foreach (var action in group.actions)
            {
                if (CheckAction(action) == false)
                {
                    return false;
                }
            }

            return true;
        }

        #endregion

        #region Check Action

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected static bool CheckAction(InputAction action)
        {
            switch (action.type)
            {
                case InputType.KeyBoardOrMouseOrJoyStick:

                    switch (action.keyBoardTriggerType)
                    {
                        case KeyBoardTriggerType.KeyStay:

                            if (Input.GetKey(action.keyCode) == false)
                            {
                                return false;
                            }

                            break;
                        case KeyBoardTriggerType.KeyDown:

                            if (Input.GetKeyDown(action.keyCode) == false)
                            {
                                return false;
                            }

                            break;
                        case KeyBoardTriggerType.KeyUp:

                            if (Input.GetKeyUp(action.keyCode) == false)
                            {
                                return false;
                            }

                            break;
                        case KeyBoardTriggerType.OnHolding:
                            if (Input.GetKey(action.keyCode) == false)
                            {
                                action.runtimeData.heldTime = 0;
                                return false;
                            }

                            action.runtimeData.heldTime += Time.deltaTime;

                            if (action.runtimeData.heldTime < action.holdThreshold)
                            {
                                return false;
                            }

                            break;
                        case KeyBoardTriggerType.HoldDown:
                            if (Input.GetKey(action.keyCode) == false)
                            {
                                action.runtimeData.heldTime = 0;
                                action.runtimeData.hasTriggeredHoldDown = false;

                                return false;
                            }

                            action.runtimeData.heldTime += Time.deltaTime;

                            if (action.runtimeData.heldTime < action.holdThreshold)
                            {
                                return false;
                            }

                            if (action.runtimeData.hasTriggeredHoldDown)
                            {
                                return false;
                            }

                            action.runtimeData.hasTriggeredHoldDown = true;
                            break;
                        case KeyBoardTriggerType.HoldAndRelease:
                            if (Input.GetKey(action.keyCode))
                            {
                                action.runtimeData.heldTime += Time.deltaTime;
                            }

                            if (Input.GetKeyUp(action.keyCode))
                            {
                                if (action.runtimeData.heldTime > action.holdThreshold)
                                {
                                    action.runtimeData.heldTime = 0;
                                }
                                else
                                {
                                    action.runtimeData.heldTime = 0;
                                    return false;
                                }
                            }
                            else
                            {
                                return false;
                            }

                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }

                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return true;
        }

        #endregion
    }
}