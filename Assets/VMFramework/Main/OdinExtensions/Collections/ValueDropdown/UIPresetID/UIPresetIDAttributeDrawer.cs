#if UNITY_EDITOR
using System.Collections.Generic;
using Sirenix.OdinInspector;
using VMFramework.GameLogicArchitecture;
using VMFramework.UI;

namespace VMFramework.OdinExtensions
{
    public class UIPresetIDAttributeDrawer : GeneralValueDropdownAttributeDrawer<UIPresetIDAttribute>
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
}
#endif