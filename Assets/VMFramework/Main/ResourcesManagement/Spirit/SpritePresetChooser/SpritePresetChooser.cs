using UnityEngine;

namespace VMFramework.Configuration
{
    public partial class SpritePresetChooser : ObjectChooser<SpritePresetItem>
    {
        public static implicit operator Sprite(SpritePresetChooser chooser)
        {
            if (chooser == null)
            {
                return null;
            }

            return chooser.GetValue();
        }
    }
}
