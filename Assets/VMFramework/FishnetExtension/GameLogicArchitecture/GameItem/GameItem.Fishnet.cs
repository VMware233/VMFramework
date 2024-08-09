#if FISHNET
using System.Runtime.CompilerServices;
using FishNet;
using FishNet.CodeGenerating;
using FishNet.Managing.Client;
using FishNet.Managing.Server;
using FishNet.Serializing;

namespace VMFramework.GameLogicArchitecture
{
    public partial class GameItem
    {
        #region Properties

        public bool isServer
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => InstanceFinder.IsServerStarted;
        }

        public bool isClient
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => InstanceFinder.IsClientStarted;
        }

        public bool isHost
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => InstanceFinder.IsHostStarted;
        }

        public bool isServerOnly
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => InstanceFinder.IsServerOnlyStarted;
        }

        public bool isClientOnly
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => InstanceFinder.IsClientOnlyStarted;
        }

        public ServerManager serverManager => InstanceFinder.ServerManager;
        public ClientManager clientManager => InstanceFinder.ClientManager;

        #endregion
        
        /// <summary>
        /// 在网络上如何传输，当在此实例被写进byte流时调用
        /// </summary>
        /// <param name="writer"></param>
        [NotSerializer]
        protected virtual void OnWrite(Writer writer)
        {
            // Debug.LogWarning($"is Writing GameItem :{this}");
        }

        /// <summary>
        /// 在网络上如何传输，当在此实例被从byte流中读出时调用
        /// </summary>
        /// <param name="reader"></param>
        [NotSerializer]
        protected virtual void OnRead(Reader reader)
        {
            // Debug.LogWarning($"is Reading GameItem :{this}");
        }

        void IGameItem.OnWriteFishnet(Writer writer)
        {
            OnWrite(writer);
        }

        void IGameItem.OnReadFishnet(Reader reader)
        {
            OnRead(reader);
        }
    }
}
#endif