using System.Collections.Generic;
using Newtonsoft.Json;
using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.Configuration;

namespace VMFramework.GlobalEvent
{
    public class InputActionGroup : BaseConfigClass
    {
        [LabelText("输入动作")]
        [ListDrawerSettings(ShowFoldout = false, DefaultExpandedState = true,
            CustomAddFunction = nameof(AddInputActionToListGUI))]
        [JsonProperty]
        public List<InputAction> actions = new();

        #region GUI

        private InputAction AddInputActionToListGUI()
        {
            return new InputAction()
            {
                holdThreshold = 0.5f,
                runtimeData = new()
            };
        }

        #endregion

        public InputActionGroup()
        {
            actions.Add(new());
        }

        public InputActionGroup(KeyCode keyCode,
            KeyBoardTriggerType keyBoardTriggerType)
        {
            InputAction action;
            action.type = InputType.KeyBoardOrMouseOrJoyStick;
            action.keyCode = keyCode;
            action.keyBoardTriggerType = keyBoardTriggerType;
            action.holdThreshold = 0;
            action.runtimeData = new();

            actions.Add(action);
        }
    }
}