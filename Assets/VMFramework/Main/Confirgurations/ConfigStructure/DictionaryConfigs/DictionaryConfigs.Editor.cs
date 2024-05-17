#if UNITY_EDITOR
namespace VMFramework.Configuration
{
    public partial class DictionaryConfigs<TID, TConfig>
    {
        protected override void OnInspectorInit()
        {
            base.OnInspectorInit();

            configs ??= new();
        }
    }
}
#endif