

namespace VMFramework.OdinExtensions
{
    public class GameTypeAttribute : GeneralValueDropdownAttribute
    {
        public bool LeafGameTypesOnly;
        
        public GameTypeAttribute(bool leafGameTypesOnly = true)
        {
            LeafGameTypesOnly = leafGameTypesOnly;
        }
    }
}