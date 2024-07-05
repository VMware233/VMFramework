#if UNITY_EDITOR && FISHNET && ODIN_INSPECTOR 
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace VMFramework.Network
{
    public partial class UUIDCoreManager
    {
        [ShowInInspector]
        private static Dictionary<string, UUIDInfo> uuidInfosDebug => uuidInfos;
    }
}
#endif