using System;
using System.Collections.Generic;
using VMFramework.Core;
using Newtonsoft.Json;
using Sirenix.OdinInspector;
using VMFramework.Containers;
using VMFramework.OdinExtensions;

namespace VMFramework.UI
{
    public partial class ContainerUIBasePreset : UIToolkitPanelPreset
    {
        protected const string CONTAINER_SETTING_CATEGORY = "Container UI";

        public override Type controllerType => typeof(ContainerUIBaseController);

        [TabGroup(TAB_GROUP_NAME, CONTAINER_SETTING_CATEGORY, SdfIconType.Box,
             TextColor = "magenta")]
        [GamePrefabID(typeof(ContainerPreset))]
        [IsNotNullOrEmpty]
        [JsonProperty]
        public string bindContainerID;

        [TabGroup(TAB_GROUP_NAME, CONTAINER_SETTING_CATEGORY)]
        [JsonProperty]
        public bool useCustomSlotSourceContainer = false;

        [TabGroup(TAB_GROUP_NAME, CONTAINER_SETTING_CATEGORY)]
        [VisualElementName]
        [ShowIf(nameof(useCustomSlotSourceContainer))]
        [JsonProperty]
        public List<string> customSlotSourceContainerNames = new();

        [TabGroup(TAB_GROUP_NAME, CONTAINER_SETTING_CATEGORY)]
        [JsonProperty]
        public bool autoAllocateSlotIndex = false;

        [TabGroup(TAB_GROUP_NAME, CONTAINER_SETTING_CATEGORY)]
        [JsonProperty]
        [ShowIf(nameof(autoAllocateSlotIndex))]
        public bool ignorePreallocateSlotIndex = false;

        [TabGroup(TAB_GROUP_NAME, CONTAINER_SETTING_CATEGORY)]
        [JsonProperty]
        public bool autoFillSlot = false;

#if UNITY_EDITOR
        [TabGroup(TAB_GROUP_NAME, CONTAINER_SETTING_CATEGORY)]
        [ListDrawerSettings(CustomAddFunction = nameof(AddAutoFillContainerConfigGUI))]
        [ShowIf(nameof(autoFillSlot))]
#endif
        public List<AutoFillContainerConfig> autoFillContainerConfigs = new();

        [TabGroup(TAB_GROUP_NAME, CONTAINER_SETTING_CATEGORY)]
        [JsonProperty]
        public int containerUIPriority = 0;

        public override void CheckSettings()
        {
            base.CheckSettings();

            bindContainerID.AssertIsNotNull(nameof(bindContainerID));
        }

        protected override void OnInit()
        {
            base.OnInit();

            ContainerUIManager.BindContainerUITo(id, bindContainerID);
        }
    }
}
