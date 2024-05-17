#if UNITY_EDITOR
using Sirenix.OdinInspector;
using UnityEngine;

namespace VMFramework.GameEvents
{
    public partial class Vector2InputGameEventConfig
    {
        protected override void OnInspectorInit()
        {
            base.OnInspectorInit();

            xPositiveActionGroups ??= new();
            xNegativeActionGroups ??= new();
            yPositiveActionGroups??= new();
            yNegativeActionGroups??= new();
        }
        
        [Button("快速添加WASD输入动作组", ButtonSizes.Medium), TabGroup(TAB_GROUP_NAME, INPUT_MAPPING_CATEGORY)]
        private void AddWASDActionGroupGUI()
        {
            isXFromAxis = false;
            isYFromAxis = false;

            xPositiveActionGroups.Clear();
            xPositiveActionGroups.Add(new(KeyCode.D, KeyBoardTriggerType.KeyStay));
            xPositiveActionGroups.Add(new(KeyCode.RightArrow, KeyBoardTriggerType.KeyStay));

            xNegativeActionGroups.Clear();
            xNegativeActionGroups.Add(new(KeyCode.A, KeyBoardTriggerType.KeyStay));
            xNegativeActionGroups.Add(new(KeyCode.LeftArrow, KeyBoardTriggerType.KeyStay));

            yPositiveActionGroups.Clear();
            yPositiveActionGroups.Add(new(KeyCode.W, KeyBoardTriggerType.KeyStay));
            yPositiveActionGroups.Add(new(KeyCode.UpArrow, KeyBoardTriggerType.KeyStay));

            yNegativeActionGroups.Clear();
            yNegativeActionGroups.Add(new(KeyCode.S, KeyBoardTriggerType.KeyStay));
            yNegativeActionGroups.Add(new(KeyCode.DownArrow, KeyBoardTriggerType.KeyStay));
        }
    }
}
#endif