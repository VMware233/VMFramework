using VMFramework.GameEvents;

namespace VMFramework.Examples
{
    public sealed class GameStartEvent : GameEvent<GameStartEvent>
    {
        // Some Game Start Parameters
        public int playerCount { get; private set; } = -1;

        // Initializes the Game Start Event with the given parameters before propagation.
        public void SetParameters(int playerCount)
        {
            this.playerCount = playerCount;
        }

        // Triggered after the Game Start Event is propagated.
        protected override void OnPropagationStopped()
        {
            base.OnPropagationStopped();

            // Reset the Game Start Parameters
            playerCount = -1;
        }
    }
}