#if UNITY_EDITOR
using System.Collections.Generic;
using Sirenix.OdinInspector;
using VMFramework.GameLogicArchitecture;

namespace VMFramework.OdinExtensions
{
    public class GamePrefabIDAttributeDrawer : GeneralValueDropdownAttributeDrawer<GamePrefabIDAttribute>
    {
        protected override IEnumerable<ValueDropdownItem> GetValues()
        {
            if (Attribute.FilterByGameItemType)
            {
                return GamePrefabManager.GetGamePrefabNameListByGameItemType(Attribute.GameItemType);
            }
            return GamePrefabManager.GetGamePrefabNameListByType(Attribute.GamePrefabTypes);
        }
    }
}
#endif