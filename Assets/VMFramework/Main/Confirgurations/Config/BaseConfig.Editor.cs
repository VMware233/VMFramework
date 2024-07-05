#if UNITY_EDITOR
using Sirenix.OdinInspector;

namespace VMFramework.Configuration
{
    [HideDuplicateReferenceBox]
    [HideReferenceObjectPicker]
    [OnInspectorInit("@((IInspectorConfig)$value)?.OnInspectorInit()")]
    public partial class BaseConfig : IInspectorConfig
    {
        protected virtual void OnInspectorInit()
        {
            
        }
        
        void IInspectorConfig.OnInspectorInit()
        {
            OnInspectorInit();
        }
    }
}
#endif