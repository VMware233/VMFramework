using Sirenix.OdinInspector;
using VMFramework.Map;

namespace VMFramework.GameLogicArchitecture
{
    public partial class GameCoreSettingBaseFile
    {
        [LabelText("地图核心通用设置"), TabGroup(TAB_GROUP_NAME, BUILTIN_MODULE_CATEGORY)]
        [Required]
        public MapCoreGeneralSetting mapCoreGeneralSetting;
    }
}