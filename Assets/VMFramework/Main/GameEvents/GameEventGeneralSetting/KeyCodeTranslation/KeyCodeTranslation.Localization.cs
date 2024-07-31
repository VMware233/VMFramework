#if UNITY_EDITOR
using VMFramework.Core;
using VMFramework.Localization;

namespace VMFramework.GameEvents
{
    public partial class KeyCodeTranslation : ILocalizedStringOwnerConfig
    {
        public void AutoConfigureLocalizedString(LocalizedStringAutoConfigSettings settings)
        {
            translation ??= new();

            translation.defaultValue ??= keyCode.ToString();

            if (translation.tableName.IsNullOrWhiteSpace())
            {
                translation.tableName = settings.defaultTableName;
            }

            if (translation.key.IsNullOrWhiteSpace())
            {
                translation.key = keyCode.ToString().ToPascalCase() + "Name";
            }
        }

        public void SetKeyValueByDefault()
        {
            translation.SetKeyValueByDefault();
        }
    }
}
#endif