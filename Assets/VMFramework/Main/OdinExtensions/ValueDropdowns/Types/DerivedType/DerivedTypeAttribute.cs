using System;

namespace VMFramework.OdinExtensions
{
    public sealed class DerivedTypeAttribute : GeneralValueDropdownAttribute
    {
        public readonly Type[] ParentTypes;

        public bool IncludingSelf = true;
        
        public bool IncludingAbstract = true;
        
        public bool IncludingInterfaces = true;

        public bool IncludingGeneric = true;

        public bool IncludingSealed = true;

        public DerivedTypeAttribute(params Type[] parentTypes)
        {
            ParentTypes = parentTypes;
        }
    }
}