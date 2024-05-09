// using System;
// using System.Collections.Generic;
// using VMFramework.Configuration;
// using VMFramework.Core;
// using Sirenix.OdinInspector;
// using UnityEngine;
//
// namespace Basis.Core
// {
//     public class InputEventConfigOfNumberArray : InputEventConfig
//     {
//         [LabelText("数字范围"), FoldoutGroup(INPUT_MAPPING_SETTING_CATEGORY)]
//         public RangeIntegerConfig numberRange = new (1, 9);
//
//         [LabelText("是否使用小键盘"), FoldoutGroup(INPUT_MAPPING_SETTING_CATEGORY)]
//         public bool isKeypad = false;
//
//         [LabelText("数字动作"), FoldoutGroup(INPUT_MAPPING_SETTING_CATEGORY)]
//         public InputAction numberAction;
//
//         public event Action<int> intAction;
//
//         [FoldoutGroup(RUNTIME_DATA_CATEGORY)]
//         [ShowInInspector]
//         private Dictionary<int, InputAction> numberActionDictionary;
//
//         #region GUI
//
//         protected override void OnInspectorInit()
//         {
//             base.OnInspectorInit();
//
//             numberRange ??= new(1, 9);
//             numberAction.keyCode = KeyCode.None;
//         }
//
//         [Button("拆分为多个事件")]
//         private void SplitIntoBoolEvents()
//         {
//             foreach (var number in new RangeInteger(numberRange).GetAllPoints())
//             {
//                 GameCoreSettingBase.globalEventGeneralSetting.AddPrefab(
//                     new InputEventConfigOfBoolArg()
//                     {
//                         id = id + "_" + number,
//                         name = name + $" {number}",
//                         isDebugging = false,
//                         isActive = true,
//                         enableTriggerEvent = false,
//                         boolActionGroups = new List<InputActionGroup>()
//                         {
//                             new(number.ConvertToKeyCode(isKeypad),
//                                 numberAction.keyBoardTriggerType)
//                         }
//                     });
//             }
//         }
//
//         #endregion
//
//         protected override void OnInit()
//         {
//             base.OnInit();
//
//             numberActionDictionary = new();
//
//             foreach (var number in new RangeInteger(numberRange).GetAllPoints())
//             {
//                 numberActionDictionary[number] = new InputAction()
//                 {
//                     keyCode = number.ConvertToKeyCode(isKeypad),
//                     type = InputType.KeyBoardOrMouseOrJoyStick,
//                     keyBoardTriggerType = numberAction.keyBoardTriggerType,
//                     holdThreshold = numberAction.holdThreshold
//                 };
//             }
//
//             if (isDebugging)
//             {
//                 intAction += arg => Debug.Log($"{name}被触发:{arg}");
//             }
//         }
//
//         public void InvokeAction(int arg)
//         {
//             InvokeAction(arg != -1);
//
//             intAction?.Invoke(arg);
//         }
//
//         protected override void OnUpdate()
//         {
//             base.OnUpdate();
//
//             foreach (var (number, inputAction) in numberActionDictionary)
//             {
//                 if (CheckAction(inputAction))
//                 {
//                     InvokeAction(number);
//                 }
//             }
//         }
//     }
// }
