using Sirenix.OdinInspector;
using VMFramework.ExtendedTilemap;

namespace VMFramework.GameLogicArchitecture
{
    public partial class BuiltInModulesSettingFile
    {
        [TabGroup(TAB_GROUP_NAME, BUILTIN_MODULE_CATEGORY)]
        [Required]
        public ExtendedRuleTileGeneralSetting extendedRuleTileGeneralSetting;
    }
}