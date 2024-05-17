using VMFramework.GameEvents;
using VMFramework.GameLogicArchitecture;

namespace TH.GameEvents
{
    public static class PlayerGameEvents
    {
        [GamePrefabIDAutoRegister(typeof(BoolInputGameEventConfig))]
        public const string DASH = "player_dash_event";

        [GamePrefabIDAutoRegister(typeof(BoolInputGameEventConfig))]
        public const string JUMP = "player_jump_event";

        [GamePrefabIDAutoRegister(typeof(FloatInputGameEventConfig))]
        public const string MOVE = "player_move_event";
        
        [GamePrefabIDAutoRegister(typeof(Vector2InputGameEventConfig))]
        public const string DIRECTION = "player_direction_event";

        [GamePrefabIDAutoRegister(typeof(BoolInputGameEventConfig))]
        public const string FLY = "player_fly_event";
        
        [GamePrefabIDAutoRegister(typeof(BoolInputGameEventConfig))]
        public const string FLY_CANCEL = "player_fly_cancel_event";

        [GamePrefabIDAutoRegister(typeof(BoolInputGameEventConfig))]
        public const string CROUCH = "player_crouch_event";
        
        [GamePrefabIDAutoRegister(typeof(BoolInputGameEventConfig))]
        public const string CROUCH_CANCEL = "player_crouch_cancel_event";
        
        [GamePrefabIDAutoRegister(typeof(BoolInputGameEventConfig))]
        public const string SPELL_ONE_CAST = "player_spell_one_cast_event";
        
        [GamePrefabIDAutoRegister(typeof(BoolInputGameEventConfig))]
        public const string SPELL_TWO_CAST = "player_spell_two_cast_event";
        
        [GamePrefabIDAutoRegister(typeof(BoolInputGameEventConfig))]
        public const string SPELL_THREE_CAST = "player_spell_three_cast_event";
        
        [GamePrefabIDAutoRegister(typeof(BoolInputGameEventConfig))]
        public const string SPELL_FOUR_CAST = "player_spell_four_cast_event";
    }
}