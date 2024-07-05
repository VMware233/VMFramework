#if UNITY_EDITOR
namespace VMFramework.Configuration
{
    public interface IInspectorConfig
    {
        protected void OnInspectorInit();
    }
}
#endif