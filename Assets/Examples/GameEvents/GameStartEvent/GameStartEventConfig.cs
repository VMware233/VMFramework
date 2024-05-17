using System;
using VMFramework.GameEvents;
using VMFramework.GameLogicArchitecture;

namespace VMFramework.Examples
{
    [GamePrefabTypeAutoRegister(ID)]
    public class GameStartEventConfig : GameEventConfig
    {
        public const string ID = "game_start_event";

        public override Type gameItemType => typeof(GameStartEvent);
    }
}