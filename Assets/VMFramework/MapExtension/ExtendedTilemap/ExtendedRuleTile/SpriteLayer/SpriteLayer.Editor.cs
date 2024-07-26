#if UNITY_EDITOR
using VMFramework.Configuration;

namespace VMFramework.Maps
{
    public partial class SpriteLayer
    {
        protected override void OnInspectorInit()
        {
            base.OnInspectorInit();

            sprite ??= new SingleSpritePresetChooserConfig();
        }
    }
}
#endif