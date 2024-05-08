#if UNITY_EDITOR
namespace VMFramework.GlobalEvent
{
    public partial class InputEventConfigOfBoolArg
    {
        protected override void OnInspectorInit()
        {
            base.OnInspectorInit();

            boolActionGroups ??= new();
        }
    }
}
#endif