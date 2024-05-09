#if UNITY_EDITOR
using VMFramework.Localization;

namespace VMFramework.GlobalEvent
{
    public partial class GlobalEventGeneralSetting
    {
        public override void AutoConfigureLocalizedString(LocalizedStringAutoConfigSettings settings)
        {
            base.AutoConfigureLocalizedString(settings);

            foreach (var config in keyCodeTranslations.GetAllConfigs())
            {
                config.AutoConfigureLocalizedString(settings);
            }
        }

        public override void CreateLocalizedStringKeys()
        {
            base.CreateLocalizedStringKeys();

            foreach (var config in keyCodeTranslations.GetAllConfigs())
            {
                config.CreateLocalizedStringKeys();
            }
        }

        public override void SetKeyValueByDefault()
        {
            base.SetKeyValueByDefault();
            
            foreach (var config in keyCodeTranslations.GetAllConfigs())
            {
                config.SetKeyValueByDefault();
            }
        }
    }
}
#endif