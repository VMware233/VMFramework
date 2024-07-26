using Sirenix.OdinInspector;
using VMFramework.Maps;

namespace VMFramework.GameLogicArchitecture
{
    public partial class BuiltInModulesSettingFile
    {
        [TabGroup(TAB_GROUP_NAME, BUILTIN_MODULE_CATEGORY)]
        [Required]
        public MapCoreGeneralSetting mapCoreGeneralSetting;
        
        [TabGroup(TAB_GROUP_NAME, BUILTIN_MODULE_CATEGORY)]
        [Required]
        public ExtendedRuleTileGeneralSetting extendedRuleTileGeneralSetting;
    }
}