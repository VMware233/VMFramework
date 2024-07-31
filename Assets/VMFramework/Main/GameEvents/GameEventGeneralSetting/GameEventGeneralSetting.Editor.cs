#if UNITY_EDITOR
using UnityEngine;
using VMFramework.Configuration;
using VMFramework.Localization;

namespace VMFramework.GameEvents
{
    public partial class GameEventGeneralSetting
    {
        protected override void OnInspectorInit()
        {
            base.OnInspectorInit();

            AddDefaultKeyCodeTranslations();
        }
        
        private void AddDefaultKeyCodeTranslations()
        {
            keyCodeTranslations ??= new();

            keyCodeTranslations.TryAddConfig(new KeyCodeTranslation(KeyCode.Mouse0,
                new LocalizedStringReference()
                {
                    defaultValue = "Left Mouse",
                    key = "LeftMouseName",
                    tableName = DefaultLocalizationTableName
                }));

            keyCodeTranslations.TryAddConfig(new KeyCodeTranslation(KeyCode.Mouse1,
                new LocalizedStringReference()
                {
                    defaultValue = "Right Mouse",
                    key = "RightMouseName",
                    tableName = DefaultLocalizationTableName
                }));

            keyCodeTranslations.TryAddConfig(new KeyCodeTranslation(KeyCode.Mouse2,
                new LocalizedStringReference()
                {
                    defaultValue = "Middle Mouse",
                    key = "MiddleMouseName",
                    tableName = DefaultLocalizationTableName
                }));
        }
    }
}
#endif