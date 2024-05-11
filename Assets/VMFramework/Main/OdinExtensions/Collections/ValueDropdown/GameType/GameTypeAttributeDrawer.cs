#if UNITY_EDITOR
using System.Collections.Generic;
using Sirenix.OdinInspector;
using VMFramework.GameLogicArchitecture;

namespace VMFramework.OdinExtensions
{
    public class GameTypeAttributeDrawer : GeneralValueDropdownAttributeDrawer<GameTypeAttribute>
    {
        protected override IEnumerable<ValueDropdownItem> GetValues()
        {
            if (Attribute.LeafGameTypesOnly == false)
            {
                return GameTypeNameUtility.GetAllGameTypeNameList();
            }
            
            return GameTypeNameUtility.GetGameTypeNameList();
        }
    }
}
#endif