using VMFramework.Core;
using VMFramework.GameLogicArchitecture;

#if FISHNET
using FishNet;
using FishNet.Managing.Timing;
#endif

namespace VMFramework.UI
{
    [GamePrefabAutoRegister(ID)]
    public class PingDebugEntry : TitleContentDebugEntry
    {
        public const string ID = "ping_debug_entry";

        public override bool ShouldDisplay()
        {
#if FISHNET
            return InstanceFinder.IsClientStarted;
#else
            return false;
#endif
        }

        protected override string GetContent()
        {
#if FISHNET
            long ping = 0;
            TimeManager tm = InstanceFinder.TimeManager;
            if (tm != null)
            {
                ping = tm.RoundTripTime;
                var deduction = (long)(tm.TickDelta * 2000d);

                ping = (ping - deduction).Max(1);
            }

            return ping + "ms";
#else
            return string.Empty;
#endif
        }
    }
}
