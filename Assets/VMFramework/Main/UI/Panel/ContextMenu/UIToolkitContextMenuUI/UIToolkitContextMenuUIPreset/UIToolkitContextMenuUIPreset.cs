using System;
using Newtonsoft.Json;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using VMFramework.GameEvents;
using VMFramework.OdinExtensions;

namespace VMFramework.UI
{
    public partial class UIToolkitContextMenuUIPreset : UIToolkitTracingUIPanelPreset, IContextMenuPreset
    {
        public const string CONTEXT_MENU_UI_CATEGORY = "上下文菜单界面设置";
        
        public const string ENTRY_SELECTED_ICON_ASSET_NAME = "Entry Selecting";

        public override Type controllerType =>
            typeof(UIToolkitContextMenuUIController);

        [TabGroup(TAB_GROUP_NAME, CONTEXT_MENU_UI_CATEGORY)]
        [VisualElementName]
        [JsonProperty]
        public string contextMenuEntryContainerName;

        [TabGroup(TAB_GROUP_NAME, CONTEXT_MENU_UI_CATEGORY)]
        public Sprite entrySelectedIcon;

        [SuffixLabel("If Only One Entry"),
         TabGroup(TAB_GROUP_NAME, CONTEXT_MENU_UI_CATEGORY)]
        [JsonProperty]
        public bool autoExecuteIfOnlyOneEntry = true;

        [TabGroup(TAB_GROUP_NAME, CONTEXT_MENU_UI_CATEGORY)]
        [JsonProperty]
        public MouseButtonType clickMouseButtonType = MouseButtonType.LeftButton;

        [TabGroup(TAB_GROUP_NAME, CONTEXT_MENU_UI_CATEGORY)]
        [GamePrefabID(typeof(IGameEventConfig))]
        [JsonProperty]
        public List<string> gameEventIDsToClose = new();

        public override void CheckSettings()
        {
            base.CheckSettings();

            isUnique = true;
        }
    }
}
