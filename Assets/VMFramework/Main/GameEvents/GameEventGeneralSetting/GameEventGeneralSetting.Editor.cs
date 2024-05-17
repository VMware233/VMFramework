#if UNITY_EDITOR
using UnityEngine;
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

            keyCodeTranslations.AddConfig(new KeyCodeTranslation(KeyCode.Mouse0,
                new LocalizedStringReference()
                {
                    defaultValue = "Left Mouse",
                    key = "LeftMouseName",
                    tableName = defaultLocalizationTableName
                }));

            keyCodeTranslations.AddConfig(new KeyCodeTranslation(KeyCode.Mouse1,
                new LocalizedStringReference()
                {
                    defaultValue = "Right Mouse",
                    key = "RightMouseName",
                    tableName = defaultLocalizationTableName
                }));

            keyCodeTranslations.AddConfig(new KeyCodeTranslation(KeyCode.Mouse2,
                new LocalizedStringReference()
                {
                    defaultValue = "Middle Mouse",
                    key = "MiddleMouseName",
                    tableName = defaultLocalizationTableName
                }));
        }
    }
}
#endif