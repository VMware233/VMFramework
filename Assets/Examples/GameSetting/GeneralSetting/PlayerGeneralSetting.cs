using UnityEngine;
using VMFramework.GameLogicArchitecture;
using VMFramework.OdinExtensions;

namespace VMFramework.Examples
{
    public sealed partial class PlayerGeneralSetting : GeneralSetting
    {
        [field: Layer]
        [field: SerializeField]
        public int playerLayer { get; private set; }
    }
}