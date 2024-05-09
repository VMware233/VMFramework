using System;
using Newtonsoft.Json;
using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.GameLogicArchitecture;
using VMFramework.OdinExtensions;

namespace VMFramework.UI
{
    public sealed partial class ContextMenuGeneralSetting : GeneralSettingBase
    {
        public const string CONTEXT_MENU_SETTING_CATEGORY = "上下文菜单设置";

        #region Default Context Menu

        [LabelText("默认上下文菜单UI的框架"), TitleGroup(CONTEXT_MENU_SETTING_CATEGORY)]
        [JsonProperty, SerializeField]
        private UIFrameworkType defaultContextMenuFrameworkType =
            UIFrameworkType.UIToolkit;

        [LabelText("默认UIToolkit版本的上下文菜单"), TitleGroup(CONTEXT_MENU_SETTING_CATEGORY)]
        [ShowIf(nameof(defaultContextMenuFrameworkType), UIFrameworkType.UIToolkit)]
        [GamePrefabIDValueDropdown(typeof(UIToolkitContextMenuUIPreset))]
        [IsNotNullOrEmpty]
        [JsonProperty, SerializeField]
        private string defaultUIToolkitContextMenuID;

        public string defaultContextMenuID => defaultContextMenuFrameworkType switch
        {
            //UIFrameworkType.UGUI => defaultUGUITooltipID,
            UIFrameworkType.UIToolkit => defaultUIToolkitContextMenuID,
            _ => throw new ArgumentOutOfRangeException()
        };

        #endregion
    }
}
