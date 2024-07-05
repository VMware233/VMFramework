using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.Configuration;
using VMFramework.GameLogicArchitecture;
using VMFramework.OdinExtensions;

namespace VMFramework.Properties
{
    public class InstanceTooltipPropertyConfig : BaseConfig
    {
        [HideInInspector]
        public Type filterType;

        [ValueDropdown(nameof(GetPropertyNameList))]
        [IsNotNullOrEmpty]
        public string propertyID;

        [HideInEditorMode]
        public IGameProperty property;

        public bool isStatic;

        #region GUI

        private IEnumerable<ValueDropdownItem> GetPropertyNameList()
        {
            return BuiltInModulesSetting.gamePropertyGeneralSetting.GetPropertyNameList(filterType);
        }

        #endregion

        public InstanceTooltipPropertyConfigRuntime ConvertToRuntime()
        {
            return new InstanceTooltipPropertyConfigRuntime()
            {
                property = property,
                isStatic = isStatic
            };
        }

        protected override void OnInit()
        {
            base.OnInit();

            property = GamePrefabManager.GetGamePrefabStrictly<IGameProperty>(propertyID);
        }
    }
}