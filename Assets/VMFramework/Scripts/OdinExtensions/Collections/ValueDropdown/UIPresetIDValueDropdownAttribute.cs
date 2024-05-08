using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using VMFramework.GameLogicArchitecture;
using VMFramework.UI;

namespace VMFramework.OdinExtensions
{
    public class UIPresetIDValueDropdownAttribute : GeneralValueDropdownAttribute
    {
        public readonly Type[] UIPresetTypes;

        public bool? IsUnique = null;
        
        public UIPresetIDValueDropdownAttribute(params Type[] uiPrefabTypes)
        {
            UIPresetTypes = uiPrefabTypes;
        }

        public UIPresetIDValueDropdownAttribute()
        {
            UIPresetTypes = new[] { typeof(IUIPanelPreset) };
        }

        public UIPresetIDValueDropdownAttribute(bool isUnique, params Type[] uiPrefabTypes)
        {
            IsUnique = isUnique;

            if (uiPrefabTypes.Length == 0)
            {
                UIPresetTypes = new[] { typeof(IUIPanelPreset) };
            }
            else
            {
                UIPresetTypes = uiPrefabTypes;
            }
        }
    }

#if UNITY_EDITOR

    public class UIPresetIDValueDropdownAttributeDrawer :
        GeneralValueDropdownAttributeDrawer<UIPresetIDValueDropdownAttribute>
    {
        protected override IEnumerable<ValueDropdownItem> GetValues()
        {
            foreach (var uiPreset in GamePrefabManager.GetGamePrefabsByTypes<IUIPanelPreset>(Attribute
                         .UIPresetTypes))
            {
                if (Attribute.IsUnique != null)
                {
                    if (uiPreset.isUnique != Attribute.IsUnique)
                    {
                        continue;
                    }
                }

                yield return uiPreset.GetNameIDDropDownItem();
            }
        }
    }

#endif
}