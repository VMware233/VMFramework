#if UNITY_EDITOR
using VMFramework.Core;
using VMFramework.Localization;

namespace VMFramework.GameLogicArchitecture
{
    public partial class LocalizedGamePrefab : ILocalizedStringOwnerConfig
    {
        public virtual void AutoConfigureLocalizedString(LocalizedStringAutoConfigSettings settings)
        {
            name ??= new();

            name.AutoConfig(id.RemoveWordsSuffix(IDSuffix.GetWords()).ToPascalCase(" "),
                id.ToPascalCase() + "Name", settings.defaultTableName);
        }

        public virtual void SetKeyValueByDefault()
        {
            name.SetKeyValueByDefault();
        }
    }
}
#endif