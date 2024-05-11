using Newtonsoft.Json;
using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.OdinExtensions;

namespace VMFramework.GameLogicArchitecture
{
    public partial class GeneralSettingBase
    {
#if UNITY_EDITOR
        [field: LabelText("默认语言表名"), TabGroup(TAB_GROUP_NAME, LOCALIZABLE_SETTING_CATEGORY)]
        [field: InfoBox("本地化设置不可用", VisibleIf = "@!localizationEnabled")]
        [field: TableName]
        [field: OnValueChanged(nameof(OnDefaultLocalizationTableNameChanged))]
        [field: EnableIf(nameof(localizationEnabled))]
#endif
        [field: SerializeField]
        [JsonProperty]
        public string defaultLocalizationTableName { get; private set; }

        #region JSON

        public bool ShouldSerializedefaultLocalizationTableName()
        {
            return localizationEnabled;
        }

        #endregion
    }
}