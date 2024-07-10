using System;
using System.Diagnostics;

namespace VMFramework.OdinExtensions
{
    [AttributeUsage(AttributeTargets.Class)]
    [Conditional("UNITY_EDITOR")]
    public sealed class TypeValidationAttribute : MultipleValidationAttribute
    {

    }
}
