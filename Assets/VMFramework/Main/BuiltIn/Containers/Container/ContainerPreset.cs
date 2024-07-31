using System;
using VMFramework.GameLogicArchitecture;

namespace VMFramework.Containers
{
    public class ContainerPreset : GameTypedGamePrefab
    {
        protected override string IDSuffix => "container";

        public override Type GameItemType => typeof(Container);
    }
}