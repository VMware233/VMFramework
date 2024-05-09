using System.Collections.Generic;
using Sirenix.OdinInspector;
using VMFramework.Procedure;

namespace VMFramework.OdinExtensions
{
    public class ProcedureIDValueDropdownAttribute : GeneralValueDropdownAttribute
    {
        
    }

#if UNITY_EDITOR
    public class ProcedureIDValueDropdownAttributeDrawer :
        GeneralValueDropdownAttributeDrawer<ProcedureIDValueDropdownAttribute>
    {
        protected override IEnumerable<ValueDropdownItem> GetValues()
        {
            return ProcedureManager.GetNameList();
        }
    }
#endif
}