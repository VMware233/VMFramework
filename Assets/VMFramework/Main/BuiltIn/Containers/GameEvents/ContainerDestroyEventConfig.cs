using System;
using VMFramework.GameEvents;
using VMFramework.GameLogicArchitecture;

namespace VMFramework.Containers
{
    [GamePrefabTypeAutoRegister(ID)]
    public sealed class ContainerDestroyEventConfig : GameEventConfig
    {
        public const string ID = "container_destroy_event";
        
        public override Type gameItemType => typeof(ContainerDestroyEvent);
    }
}