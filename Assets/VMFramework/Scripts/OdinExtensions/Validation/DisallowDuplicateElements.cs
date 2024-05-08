using System;
using System.Collections;
using System.Diagnostics;
using System.Linq;
using UnityEngine;
using VMFramework.Core.Linq;

#if UNITY_EDITOR

using Sirenix.OdinInspector.Editor;

#endif

namespace VMFramework.OdinExtensions
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    [Conditional("UNITY_EDITOR")]
    public class DisallowDuplicateElementsAttribute : SingleValidationAttribute
    {

    }

#if UNITY_EDITOR

    [DrawerPriority(DrawerPriorityLevel.WrapperPriority)]
    public class DisallowDuplicateElementsDrawer : 
        SingleValidationAttributeDrawer<DisallowDuplicateElementsAttribute>
    {
        protected override string GetDefaultMessage(GUIContent label)
        {
            return $"{label}不能包含重复元素";
        }

        protected override bool Validate(object value)
        {
            if (value is not ICollection collection)
            {
                return true;
            }

            if (collection.Cast<object>().ContainsSame())
            {
                return false;
            }

            return true;
        }
    }

#endif
}
