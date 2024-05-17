#if UNITY_EDITOR
using Sirenix.OdinInspector;

namespace VMFramework.GameLogicArchitecture.Editor
{
    public sealed partial class GamePrefabWrapperGeneralSetting : GeneralSettingBase
    {
        [LabelText("单包装模板")]
        public GamePrefabSingleWrapper singleWrapperTemplate;
    }
}
#endif