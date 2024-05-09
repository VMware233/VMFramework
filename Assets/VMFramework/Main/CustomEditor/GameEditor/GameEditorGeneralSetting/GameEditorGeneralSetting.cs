#if UNITY_EDITOR
using VMFramework.GameLogicArchitecture;

namespace VMFramework.Editor
{
    public sealed partial class GameEditorGeneralSetting : GeneralSettingBase
    {
        public int autoStackMenuTreeNodesMaxCount = 4;
    }
}

#endif