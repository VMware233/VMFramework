using System.Collections.Generic;
using Sirenix.OdinInspector;
using VMFramework.GameLogicArchitecture;

namespace VMFramework.OdinExtensions
{
    public class GameTypeValueDropdownAttribute : GeneralValueDropdownAttribute
    {
        public bool LeafGameTypesOnly;
        
        public GameTypeValueDropdownAttribute(bool leafGameTypesOnly = true)
        {
            LeafGameTypesOnly = leafGameTypesOnly;
        }
    }

#if UNITY_EDITOR
    public class GameTypeValueDropdown : GeneralValueDropdownAttributeDrawer<GameTypeValueDropdownAttribute>
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
#endif
}