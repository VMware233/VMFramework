using VMFramework.Core;
using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;

#if UNITY_EDITOR

using Sirenix.OdinInspector.Editor;

#endif

namespace VMFramework.OdinExtensions
{
    public interface IEmptyCheckable
    {
        public bool IsEmpty();
    }

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property |
                    AttributeTargets.Parameter | AttributeTargets.Class)]
    [Conditional("UNITY_EDITOR")]
    public class IsNotNullOrEmptyAttribute : SingleValidationAttribute
    {
        public readonly bool Trim = true;
    }

#if UNITY_EDITOR

    [DrawerPriority(DrawerPriorityLevel.WrapperPriority)]
    public sealed class IsNotNullOrEmptyAttributeDrawer : SingleValidationAttributeDrawer<IsNotNullOrEmptyAttribute>
    {
        protected override bool Validate(object value)
        {
            bool isNullOrEmpty = value is null;

            if (value is string stringValue)
            {
                if (Attribute.Trim)
                {
                    if (stringValue.IsEmptyAfterTrim())
                    {
                        isNullOrEmpty = true;
                    }
                }
                else
                {
                    if (stringValue.IsEmpty())
                    {
                        isNullOrEmpty = true;
                    }
                }
            }
            else if (value is ICollection collectionValue)
            {
                if (collectionValue.Count == 0)
                {
                    isNullOrEmpty = true;
                }
            }
            else if (value is IEmptyCheckable emptyCheckable)
            {
                if (emptyCheckable.IsEmpty())
                {
                    isNullOrEmpty = true;
                }
            }

            return isNullOrEmpty == false;
        }

        protected override string GetDefaultMessage(GUIContent label)
        {
            var propertyName = label == null ? Property.Name : label.text;

            return $"{propertyName}不能为空";
        }
    }

#endif
}
