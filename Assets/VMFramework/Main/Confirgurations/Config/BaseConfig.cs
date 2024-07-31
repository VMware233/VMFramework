using Newtonsoft.Json;
using UnityEngine;
using VMFramework.Core;

namespace VMFramework.Configuration
{
    [JsonObject(MemberSerialization.OptIn, ItemTypeNameHandling = TypeNameHandling.All)]
    public abstract partial class BaseConfig : IConfig
    {
        public bool initDone { get; private set; } = false;
        
        protected virtual bool showInitLog => false;

        public virtual void CheckSettings()
        {

        }

        public void Init()
        {
            if (showInitLog)
            {
                Debugger.Log($"Starting Init for {this}");
            }
            
            OnInit();

            initDone = true;
        }

        protected virtual void OnInit()
        {

        }
    }
}
