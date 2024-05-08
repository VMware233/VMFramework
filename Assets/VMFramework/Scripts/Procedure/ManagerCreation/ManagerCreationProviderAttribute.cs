using System;

namespace VMFramework.Procedure
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ManagerCreationProviderAttribute : Attribute
    {
        public readonly ManagerType ManagerType;

        public ManagerCreationProviderAttribute()
        {
            ManagerType = ManagerType.OtherCore;
        }

        public ManagerCreationProviderAttribute(ManagerType managerType)
        {
            ManagerType = managerType;
        }
    }
}