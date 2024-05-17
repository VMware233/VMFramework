using System;
using System.Collections.Generic;
using VMFramework.GameLogicArchitecture;
using VMFramework.Core;
using Newtonsoft.Json;
using Sirenix.OdinInspector;
using VMFramework.GameEvents;
using VMFramework.OdinExtensions;

namespace VMFramework.UI
{
    public partial class UIPanelPreset : LocalizedGamePrefab, IUIPanelPreset
    {
        protected override string idSuffix => "ui";

        [LabelText("控制器类别"), TabGroup(TAB_GROUP_NAME, BASIC_SETTING_CATEGORY)]
        [ShowInInspector]
        public virtual Type controllerType => typeof(UIPanelController);

        [LabelText("显示优先级"), SuffixLabel("大显示优先级的UI会覆盖小的"),
         TabGroup(TAB_GROUP_NAME, BASIC_SETTING_CATEGORY)]
        [JsonProperty]
        public int sortingOrder = 0;

        [LabelText("唯一"), TabGroup(TAB_GROUP_NAME, BASIC_SETTING_CATEGORY)]
        [JsonProperty]
        public bool isUnique = true;
        
        [LabelText("预热次数"), TabGroup(TAB_GROUP_NAME, BASIC_SETTING_CATEGORY)]
        [HideIf(nameof(isUnique))]
        [MinValue(0)]
        [JsonProperty]
        public int prewarmCount = 0;

        [LabelText("创建时自动打开"), TabGroup(TAB_GROUP_NAME, BASIC_SETTING_CATEGORY)]
        [JsonProperty]
        public bool autoOpenOnCreation = false;

        [LabelText("开启此面板时关闭的游戏事件"), TabGroup(TAB_GROUP_NAME, BASIC_SETTING_CATEGORY)]
        [GamePrefabID(typeof(IGameEventConfig))]
        [ListDrawerSettings(ShowFoldout = false)]
        [DisallowDuplicateElements]
        [JsonProperty]
        public List<string> gameEventDisabledOnOpen = new();

        [LabelText("启用关闭此UI的输入映射"), TabGroup(TAB_GROUP_NAME, BASIC_SETTING_CATEGORY)]
        [JsonProperty]
        public bool enableUICloseGameEvent = false;

        [LabelText("关闭此UI的游戏事件"), TabGroup(TAB_GROUP_NAME, BASIC_SETTING_CATEGORY)]
        [GamePrefabID(typeof(IGameEventConfig))]
        [IsNotNullOrEmpty]
        [ShowIf(nameof(enableUICloseGameEvent))]
        [JsonProperty]
        public string uiCloseGameEventID;

        [LabelText("启用切换开关此UI的输入映射"), TabGroup(TAB_GROUP_NAME, BASIC_SETTING_CATEGORY)]
        [JsonProperty]
        public bool enableUIGameEvent = false;

        [LabelText("切换开关此UI的游戏事件"), TabGroup(TAB_GROUP_NAME, BASIC_SETTING_CATEGORY)]
        [GamePrefabID(typeof(IGameEventConfig))]
        [IsNotNullOrEmpty]
        [ShowIf(nameof(enableUIGameEvent))]
        [JsonProperty]
        public string uiToggleGameEventID;

        #region Interface Implementation

        bool IUIPanelPreset.isUnique => isUnique;

        int IUIPanelPreset.prewarmCount => prewarmCount;

        #endregion

        public override void CheckSettings()
        {
            base.CheckSettings();

            controllerType.AssertIsDerivedFrom(typeof(IUIPanelController), true, false);
        }
    }
}
