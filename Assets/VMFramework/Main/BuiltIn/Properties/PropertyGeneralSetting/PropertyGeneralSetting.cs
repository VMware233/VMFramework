using System;
using System.Collections.Generic;
using VMFramework.UI;
using VMFramework.GameLogicArchitecture;
using VMFramework.Core;
using Sirenix.OdinInspector;
using Newtonsoft.Json;
using UnityEngine;
using VMFramework.OdinExtensions;

namespace VMFramework.Properties
{
    public sealed partial class PropertyGeneralSetting : GamePrefabGeneralSetting
    {
        #region Categories

        public const string TOOLTIP_SETTING_CATEGORY = "Tooltip";

        public const string PROPERTY_SETTING_CATEGORY = "Property";

        #endregion

        #region Metadata

        public override Type baseGamePrefabType => typeof(GameProperty);

        #endregion

        [TabGroup(TAB_GROUP_NAME, PROPERTY_SETTING_CATEGORY)]
        [ShowInInspector]
        [ReadOnly]
        private static Dictionary<Type, List<IGameProperty>> propertyConfigs = new();

        #region Init & Check

        protected override void OnPreInit()
        {
            base.OnPreInit();

            propertyConfigs.Clear();

            foreach (var propertyConfig in GamePrefabManager.GetAllGamePrefabs<IGameProperty>())
            {
                if (propertyConfigs.ContainsKey(propertyConfig.targetType) == false)
                {
                    propertyConfigs[propertyConfig.targetType] = new();
                }

                propertyConfigs[propertyConfig.targetType].Add(propertyConfig);
            }
        }

        #endregion

        [Button, TabGroup(TAB_GROUP_NAME, PROPERTY_SETTING_CATEGORY)]
        public static List<IGameProperty> GetPropertyConfigs(Type targetType)
        {
            var result = new List<IGameProperty>();

            if (propertyConfigs.Count == 0)
            {
                Debug.LogWarning($"{nameof(propertyConfigs)} is not loaded");
                return result;
            }

            foreach (var (type, propertyConfig) in propertyConfigs)
            {
                if (type.IsAssignableFrom(targetType))
                {
                    result.AddRange(propertyConfig);
                }
            }

            return result;
        }

        public IEnumerable<ValueDropdownItem> GetPropertyNameList(Type targetType)
        {
            if (targetType == null)
            {
                yield break;
            }
            
            foreach (var config in GamePrefabManager.GetAllGamePrefabs<IGameProperty>())
            {
                if (config is { isActive: true } && targetType.IsDerivedFrom(config.targetType, true))
                {
                    yield return new(config.name, config.id);
                }
            }
        }
    }
}
