using System;
using VMFramework.GameLogicArchitecture;

namespace VMFramework.Maps
{
    public sealed partial class MapCoreGeneralSetting : GamePrefabGeneralSetting
    {
        #region Meta Data

        public override Type BaseGamePrefabType => typeof(MapCoreConfiguration);

        #endregion
    }
}
