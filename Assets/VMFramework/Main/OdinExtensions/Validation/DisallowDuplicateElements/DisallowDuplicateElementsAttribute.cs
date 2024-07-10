using System;
using System.Diagnostics;

namespace VMFramework.OdinExtensions
{
    [AttributeUsage(AttributeTargets.Field)]
    [Conditional("UNITY_EDITOR")]
    public sealed class DisallowDuplicateElementsAttribute : SingleValidationAttribute
    {

    }
}
