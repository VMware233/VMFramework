#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.Core;

namespace VMFramework.OdinExtensions
{
    internal sealed class NamespaceAttributeDrawer : GeneralValueDropdownAttributeDrawer<NamespaceAttribute>
    {
        protected override IEnumerable<ValueDropdownItem> GetValues()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();
            
            var executingAssembly = Assembly.GetExecutingAssembly();
            
            assemblies.Remove(executingAssembly);
            assemblies.Prepend(executingAssembly);

            return assemblies.SelectMany(assembly => assembly.GetNamespaces()).ToValueDropdownItems();
        }
    }
}
#endif