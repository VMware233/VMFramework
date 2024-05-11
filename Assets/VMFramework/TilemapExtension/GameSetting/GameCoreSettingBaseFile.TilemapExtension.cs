using Sirenix.OdinInspector;
using VMFramework.ExtendedTilemap;

namespace VMFramework.GameLogicArchitecture
{
    public partial class GameCoreSettingBaseFile
    {
        [LabelText("拓展瓦片通用设置"), TabGroup(TAB_GROUP_NAME, BUILTIN_MODULE_CATEGORY)]
        [Required]
        public ExtendedRuleTileGeneralSetting extendedRuleTileGeneralSetting;
    }
}