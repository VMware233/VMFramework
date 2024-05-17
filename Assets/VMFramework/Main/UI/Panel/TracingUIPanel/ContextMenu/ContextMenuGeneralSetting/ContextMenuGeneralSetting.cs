using Newtonsoft.Json;
using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.Core;
using VMFramework.GameLogicArchitecture;
using VMFramework.OdinExtensions;

namespace VMFramework.UI
{
    public sealed partial class ContextMenuGeneralSetting : GeneralSettingBase
    {
        public const string CONTEXT_MENU_CATEGORY = "上下文菜单设置";

        #region Default Context Menu

        [LabelText("默认上下文菜单"), TabGroup(TAB_GROUP_NAME, CONTEXT_MENU_CATEGORY)]
        [GamePrefabID(typeof(IContextMenuPreset))]
        [IsNotNullOrEmpty]
        [JsonProperty, SerializeField]
        public string defaultContextMenuID;

        #endregion

        public override void CheckSettings()
        {
            base.CheckSettings();
            
            defaultContextMenuID.AssertIsNotNullOrEmpty(nameof(defaultContextMenuID));
        }
    }
}
