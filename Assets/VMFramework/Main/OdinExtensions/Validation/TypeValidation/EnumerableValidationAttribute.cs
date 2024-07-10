using System;
using System.Diagnostics;

namespace VMFramework.OdinExtensions
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property |
                    AttributeTargets.Struct)]
    [Conditional("UNITY_EDITOR")]
    public sealed class EnumerableValidationAttribute : MultipleValidationAttribute
    {

    }
}