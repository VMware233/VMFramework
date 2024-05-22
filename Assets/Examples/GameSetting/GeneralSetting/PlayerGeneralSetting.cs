using VMFramework.GameLogicArchitecture;
using VMFramework.OdinExtensions;

namespace VMFramework.Examples
{
    public sealed partial class PlayerGeneralSetting : GeneralSetting
    {
        [Layer]
        public int playerLayer { get; private set; }
    }
}