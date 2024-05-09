using Sirenix.OdinInspector;
using VMFramework.Configuration;

namespace VMFramework.ExtendedTilemap
{
    public class SpriteLayer : BaseConfigClass
    {
        [LabelText("层级")]
        public int layer;

        [HideLabel]
        public SpritePresetChooser sprite = new();

        protected override void OnInspectorInit()
        {
            base.OnInspectorInit();

            sprite ??= new();
        }
    }
}