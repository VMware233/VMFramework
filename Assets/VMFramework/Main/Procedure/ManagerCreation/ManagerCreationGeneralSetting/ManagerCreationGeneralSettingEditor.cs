#if UNITY_EDITOR
using Sirenix.OdinInspector;

namespace VMFramework.Procedure
{
    public partial class ManagerCreationGeneralSetting
    {
        protected override void OnInspectorInit()
        {
            base.OnInspectorInit();

            ManagerCreator.CreateManagers();
        }

        [Button("创建管理器"), TabGroup(TAB_GROUP_NAME, MANAGER_CATEGORY)]
        private void CreateManagersGUI()
        {
            ManagerCreator.CreateManagers();
        }
    }
}
#endif