using System;
using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.Configuration;
using VMFramework.GameLogicArchitecture;

namespace VMFramework.GlobalEvent
{
    public sealed partial class GlobalEventGeneralSetting : GamePrefabGeneralSetting
    {
        #region Meta Data

        public override string prefabName => "Global Event";

        public override Type baseGamePrefabType => typeof(GlobalEventConfig);

        #endregion

        [field: LabelText("KeyCode翻译"), TabGroup(TAB_GROUP_NAME, LOCALIZABLE_SETTING_CATEGORY)]
        [field: SerializeField]
        public DictionaryConfigs<KeyCode, KeyCodeTranslation> keyCodeTranslations
        {
            get;
            private set;
        } = new();

        #region Init & CheckSettings

        public override void CheckSettings()
        {
            base.CheckSettings();

            keyCodeTranslations ??= new();
            keyCodeTranslations.CheckSettings();
        }

        protected override void OnPostInit()
        {
            base.OnPostInit();
            
            keyCodeTranslations.Init();
        }

        #endregion

        public string GetKeyCodeName(KeyCode keyCode,
            KeyCodeUtility.KeyCodeToStringMode mode)
        {
            if (keyCodeTranslations.TryGetConfig(keyCode, out var translation))
            {
                return translation.translation;
            }

            return keyCode.ConvertToString(mode);
        }
    }
}
