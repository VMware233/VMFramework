#if FISHNET
using FishNet.Serializing;
using UnityEngine;

namespace VMFramework.GameLogicArchitecture
{
    public partial interface IGameItem
    {
        /// <summary>
        /// 在网络上如何传输，当在此实例被写进byte流时调用
        /// </summary>
        /// <param name="writer"></param>
        public void OnWriteFishnet(Writer writer)
        {
            Debug.LogError("Not implemented On Write");
        }

        /// <summary>
        /// 在网络上如何传输，当在此实例被从byte流中读出时调用
        /// </summary>
        /// <param name="reader"></param>
        public void OnReadFishnet(Reader reader)
        {
            Debug.LogError("Not implemented On Read");
        }
    }
}
#endif