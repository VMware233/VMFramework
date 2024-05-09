#if UNITY_EDITOR
using Sirenix.OdinInspector;
using UnityEngine;

namespace VMFramework.GlobalEvent
{
    public partial class InputEventConfigOfFloatArg
    {
        protected override void OnInspectorInit()
        {
            base.OnInspectorInit();

            floatPositiveActionGroups ??= new();
            floatNegativeActionGroups ??= new();
        }

        [Button("快速添加AD左右输入动作组", ButtonSizes.Medium)]
        private void AddADActionGroupGUI()
        {
            isFloatFromAxis = false;

            floatPositiveActionGroups.Clear();
            floatPositiveActionGroups.Add(new(KeyCode.D, KeyBoardTriggerType.KeyStay));
            floatPositiveActionGroups.Add(new(KeyCode.RightArrow, KeyBoardTriggerType.KeyStay));

            floatNegativeActionGroups.Clear();
            floatNegativeActionGroups.Add(new(KeyCode.A, KeyBoardTriggerType.KeyStay));
            floatNegativeActionGroups.Add(new(KeyCode.LeftArrow, KeyBoardTriggerType.KeyStay));
        }
    }
}
#endif