#if UNITY_EDITOR
namespace VMFramework.GlobalEvent
{
    public partial class InputEventConfig
    {
        protected override void OnInspectorInit()
        {
            base.OnInspectorInit();

            requireUpdate = true;
        }
        
        protected InputActionGroup AddActionGroupGUI()
        {
            return new InputActionGroup();
        }
    }
}
#endif