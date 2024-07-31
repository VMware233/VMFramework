#if UNITY_EDITOR
using Sirenix.OdinInspector;

namespace VMFramework.Timers
{
    public partial class Timer
    {
        [ShowInInspector]
        public double expectedTimeDebug => priority;

        [ShowInInspector]
        public double stoppedTimeDebug => StoppedTime;

        [ShowInInspector]
        public double startedTimeDebug => StartedTime;
    }
}
#endif