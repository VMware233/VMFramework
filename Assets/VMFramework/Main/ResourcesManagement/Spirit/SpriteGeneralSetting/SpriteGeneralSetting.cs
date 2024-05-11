using System;
using VMFramework.GameLogicArchitecture;
using VMFramework.Core;

namespace VMFramework.ResourcesManagement
{ 
    public sealed partial class SpriteGeneralSetting : GamePrefabGeneralSetting
    {
        #region Meta Data
        
        public override Type baseGamePrefabType => typeof(SpritePreset);

        #endregion
    }
}
