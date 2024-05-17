using System;
using VMFramework.GameEvents;
using VMFramework.GameLogicArchitecture;

namespace VMFramework.Containers
{
    [GamePrefabTypeAutoRegister(ID)]
    public sealed class ContainerCreateEventConfig : GameEventConfig
    {
        public const string ID = "container_create_event";
        
        public override Type gameItemType => typeof(ContainerCreateEvent);
    }
}