using System;
using VMFramework.UI;

namespace VMFramework.OdinExtensions
{
    public class UIPresetIDAttribute : GeneralValueDropdownAttribute
    {
        public readonly Type[] UIPresetTypes;

        public bool? IsUnique = null;
        
        public UIPresetIDAttribute(params Type[] uiPrefabTypes)
        {
            UIPresetTypes = uiPrefabTypes;
        }

        public UIPresetIDAttribute()
        {
            UIPresetTypes = new[] { typeof(IUIPanelPreset) };
        }

        public UIPresetIDAttribute(bool isUnique, params Type[] uiPrefabTypes)
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
}