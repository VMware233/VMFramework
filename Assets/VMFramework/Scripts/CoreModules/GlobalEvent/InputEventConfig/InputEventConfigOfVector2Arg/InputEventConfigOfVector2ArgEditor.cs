#if UNITY_EDITOR
using Sirenix.OdinInspector;
using UnityEngine;

namespace VMFramework.GlobalEvent
{
    public partial class InputEventConfigOfVector2Arg
    {
        protected override void OnInspectorInit()
        {
            base.OnInspectorInit();

            vector2XPositiveActionGroups ??= new();
            vector2XNegativeActionGroups ??= new();
            vector2YPositiveActionGroups ??= new();
            vector2YNegativeActionGroups ??= new();
        }

        [Button("快速添加WASD输入动作组", ButtonSizes.Medium)]
        private void AddWASDActionGroupGUI()
        {
            isVector2XFromAxis = false;
            isVector2YFromAxis = false;

            vector2XPositiveActionGroups.Clear();
            vector2XPositiveActionGroups.Add(new(KeyCode.D, KeyBoardTriggerType.KeyStay));
            vector2XPositiveActionGroups.Add(new(KeyCode.RightArrow, KeyBoardTriggerType.KeyStay));

            vector2XNegativeActionGroups.Clear();
            vector2XNegativeActionGroups.Add(new(KeyCode.A, KeyBoardTriggerType.KeyStay));
            vector2XNegativeActionGroups.Add(new(KeyCode.LeftArrow, KeyBoardTriggerType.KeyStay));

            vector2YPositiveActionGroups.Clear();
            vector2YPositiveActionGroups.Add(new(KeyCode.W, KeyBoardTriggerType.KeyStay));
            vector2YPositiveActionGroups.Add(new(KeyCode.UpArrow, KeyBoardTriggerType.KeyStay));

            vector2YNegativeActionGroups.Clear();
            vector2YNegativeActionGroups.Add(new(KeyCode.S, KeyBoardTriggerType.KeyStay));
            vector2YNegativeActionGroups.Add(new(KeyCode.DownArrow, KeyBoardTriggerType.KeyStay));
        }
    }
}
#endif