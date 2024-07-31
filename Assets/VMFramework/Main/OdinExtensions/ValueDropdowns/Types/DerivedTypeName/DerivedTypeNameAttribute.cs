using System;

namespace VMFramework.OdinExtensions
{
    public sealed class DerivedTypeNameAttribute : GeneralValueDropdownAttribute
    {
        public readonly Type[] ParentTypes;

        public bool IncludingSelf = true;
        
        public bool IncludingAbstract = true;
        
        public bool IncludingInterfaces = true;

        public bool IncludingGeneric = true;

        public bool IncludingSealed = true;

        public DerivedTypeNameAttribute(params Type[] parentTypes)
        {
            ParentTypes = parentTypes;
        }
    }
}