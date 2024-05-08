#if UNITY_EDITOR
namespace VMFramework.GameLogicArchitecture
{
    public partial class GameTypeGeneralSetting
    {
        protected override void OnInspectorInit()
        {
            base.OnInspectorInit();

            subrootGameTypeInfos ??= new();
        }

        private void OnSubrootGameTypeInfosChanged()
        {
            InitGameTypeInfo();
        }
    }
}
#endif