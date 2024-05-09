using System;
using VMFramework.GameLogicArchitecture;

namespace VMFramework.Containers
{
    public sealed partial class ContainerGeneralSetting : GamePrefabGeneralSetting
    {
        public override string prefabName => "Container";

        public override Type baseGamePrefabType => typeof(ContainerPreset);
    }
}
