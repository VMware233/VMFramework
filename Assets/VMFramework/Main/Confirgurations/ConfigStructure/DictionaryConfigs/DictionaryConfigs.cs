using System.Collections.Generic;
using System.Linq;
using VMFramework.Core;
using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.Core.Linq;
using VMFramework.GameLogicArchitecture;
using VMFramework.OdinExtensions;

namespace VMFramework.Configuration
{
    [PreviewComposite]
    public partial class DictionaryConfigs<TID, TConfig> : BaseConfig 
        where TConfig : IConfig, IIDOwner<TID>
    {
        [LabelText("设置")]
        [ListDrawerSettings(ShowFoldout = false)]
        [SerializeField]
        private List<TConfig> configs = new();

        [ShowInInspector]
        [HideInEditorMode]
        private Dictionary<TID, TConfig> configsRuntime = new();

        #region Init & CheckSettings

        public override void CheckSettings()
        {
            base.CheckSettings();

            configs.CheckSettings();
        }

        protected override void OnInit()
        {
            base.OnInit();

            configs.Init();

            configsRuntime = new();

            foreach (var config in configs)
            {
                if (configsRuntime.TryAdd(config.id, config) == false)
                {
                    Debug.LogWarning($"{config.id}重复");
                }
            }
        }

        #endregion

        #region Add Config

        public bool AddConfig(TConfig config)
        {
            if (initDone)
            {
                return configsRuntime.TryAdd(config.id, config);
            }

            if (HasConfig(config.id) == false)
            {
                configs.Add(config);
                return true;
            }
                
            return false;
        }

        #endregion

        #region Remove Config

        public bool RemoveConfig(TID id)
        {
            if (initDone)
            {
                return configsRuntime.Remove(id);
            }

            foreach (var config in configs)
            {
                if (config.id.Equals(id))
                {
                    configs.Remove(config);
                    return true;
                }
            }

            return false;
        }

        #endregion

        #region Get Config

        public IEnumerable<TConfig> GetAllConfigs()
        {
            if (initDone)
            {
                return configsRuntime.Values;
            }

            return configs;
        }

        public TConfig GetConfig(TID id)
        {
            if (initDone)
            {
                return configsRuntime.GetValueOrDefault(id);
            }

            foreach (var config in configs)
            {
                if (config.id.Equals(id))
                {
                    return config;
                }
            }

            return default;
        }

        public bool HasConfig(TID id)
        {
            if (initDone)
            {
                return configsRuntime.ContainsKey(id);
            }

            return configs.Any(config => config.id.Equals(id));
        }

        public bool TryGetConfig(TID id, out TConfig config)
        {
            if (initDone)
            {
                return configsRuntime.TryGetValue(id, out config);
            }

            foreach (var c in configs)
            {
                if (c.id.Equals(id))
                {
                    config = c;
                    return true;
                }
            }

            config = default;
            return false;
        }

        #endregion

        #region To String

        public override string ToString()
        {
            return configs.Select<TConfig, INameOwner>()
                .Select(nameOwner => nameOwner.name).Join(",");
        }

        #endregion
    }
}
