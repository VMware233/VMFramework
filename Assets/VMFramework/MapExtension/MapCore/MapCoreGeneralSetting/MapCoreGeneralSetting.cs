using System;
using VMFramework.GameLogicArchitecture;

namespace VMFramework.Map
{
    public sealed partial class MapCoreGeneralSetting : GamePrefabGeneralSetting
    {
        public override Type baseGamePrefabType => typeof(MapCoreConfiguration);
    }
}
