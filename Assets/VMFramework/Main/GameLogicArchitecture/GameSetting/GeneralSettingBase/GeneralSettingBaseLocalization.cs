#if UNITY_EDITOR
using Sirenix.OdinInspector;
using VMFramework.Localization;

namespace VMFramework.GameLogicArchitecture
{
    public partial class GeneralSettingBase : ILocalizedStringOwnerConfig
    {
        public virtual bool localizationEnabled => false;
        
        private void OnDefaultLocalizationTableNameChanged()
        {
            AutoConfigureLocalizedString(new()
            {
                defaultTableName = defaultLocalizationTableName,
                save = true,
            });
        }

        public virtual void AutoConfigureLocalizedString(LocalizedStringAutoConfigSettings settings)
        {
            
        }

        [Button("创建所有本地化字符串键"), TabGroup(TAB_GROUP_NAME, LOCALIZABLE_SETTING_CATEGORY)]
        [EnableIf(nameof(localizationEnabled))]
        public virtual void CreateLocalizedStringKeys()
        {
            
        }

        [Button("设置Key为默认值"), TabGroup(TAB_GROUP_NAME, LOCALIZABLE_SETTING_CATEGORY)]
        [EnableIf(nameof(localizationEnabled))]
        public virtual void SetKeyValueByDefault()
        {
            
        }
    }
}
#endif