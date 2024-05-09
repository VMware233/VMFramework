using System;
using VMFramework.Configuration;
using VMFramework.GameLogicArchitecture;
using Sirenix.OdinInspector;
using VMFramework.Editor;
using VMFramework.Localization;

namespace VMFramework.ResourcesManagement
{
    public sealed partial class AudioGeneralSetting : GamePrefabGeneralSetting
    {
        #region Meta Data
        
        public override string prefabName => "Audio Preset";

        public override Type baseGamePrefabType => typeof(AudioPreset);

        #endregion

        [HideLabel, TabGroup(TAB_GROUP_NAME, MISCELLANEOUS_CATEGORY)]
        public ContainerChooser container = new();
    }
}
