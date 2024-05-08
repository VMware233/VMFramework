using Newtonsoft.Json;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using VMFramework.Core;
using VMFramework.GameLogicArchitecture;

namespace VMFramework.GlobalEvent
{
    public sealed partial class InputEventConfigOfBoolArg : InputEventConfig
    {
#if UNITY_EDITOR
        [LabelText("输入动作组"), TabGroup(TAB_GROUP_NAME, INPUT_MAPPING_SETTING_CATEGORY)]
        [ListDrawerSettings(CustomAddFunction = nameof(AddActionGroupGUI))]
#endif
        [JsonProperty]
        public List<InputActionGroup> boolActionGroups = new();

        public override IEnumerable<string> GetInputMappingContent(
            KeyCodeUtility.KeyCodeToStringMode mode)
        {
            if (boolActionGroups.Count == 0)
            {
                yield return "";
                yield break;
            }

            foreach (var actionGroup in boolActionGroups)
            {
                var contentList = new List<string>();

                foreach (var action in actionGroup.actions)
                {
                    contentList.Add(
                        GameCoreSettingBase.globalEventGeneralSetting
                            .GetKeyCodeName(action.keyCode, mode));
                }

                yield return "+".Join(contentList);
            }
        }

        protected override void OnUpdate()
        {
            base.OnUpdate();

            InvokeAction(CheckGroups(boolActionGroups));
        }
    }
}
