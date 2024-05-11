#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;
using VMFramework.OdinExtensions;

namespace VMFramework.Configuration
{
    public partial class KCubeConfig<TPoint> : ITypeValidationProvider
    {
        IEnumerable<ValidationResult> ITypeValidationProvider.GetValidationResults(GUIContent label)
        {
            if (displayMaxLessThanMinError)
            {
                yield return new($"{sizeName}为负值，最大{pointName}不能小于最小{pointName}", ValidateType.Error);
            }
        }
    }
}
#endif