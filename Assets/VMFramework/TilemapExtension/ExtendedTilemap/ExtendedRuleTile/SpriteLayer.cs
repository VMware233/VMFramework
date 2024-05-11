using Sirenix.OdinInspector;
using VMFramework.Configuration;
using VMFramework.Core;

namespace VMFramework.ExtendedTilemap
{
    public class SpriteLayer : BaseConfigClass
    {
        [LabelText("层级")]
        public int layer;

        [HideLabel]
        public ISpritePresetChooserConfig sprite;

        public override void CheckSettings()
        {
            base.CheckSettings();
            
            sprite.AssertIsNotNull(nameof(sprite));
        }
    }
}