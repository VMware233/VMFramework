using VMFramework.GameEvents;

namespace VMFramework.Examples
{
    // sealed class to ensure the event is singleton
    public sealed class GameStopEvent : SingletonGameEvent<GameStopEvent>
    {
        // Some GameStopEvent parameters here
        public int errorCode { get; private set; } = -1;
        
        // Set the parameters of the GameStopEvent
        public static void SetParameters(int errorCode)
        {
            instance.errorCode = errorCode;
        }

        // Triggered after the propagation of the GameStopEvent has stopped
        protected override void OnPropagationStopped()
        {
            base.OnPropagationStopped();

            // Reset the parameters of the GameStopEvent
            errorCode = -1;
        }
    }
}