#if UNITY_EDITOR
using Sirenix.OdinInspector;
using VMFramework.Editor;

namespace VMFramework.GameLogicArchitecture.Editor
{
    public sealed partial class GamePrefabWrapperGeneralSetting : GeneralSettingBase
    {
        [LabelText("单包装模板")]
        public GamePrefabSingleWrapper singleWrapperTemplate;
    }
}
#endif