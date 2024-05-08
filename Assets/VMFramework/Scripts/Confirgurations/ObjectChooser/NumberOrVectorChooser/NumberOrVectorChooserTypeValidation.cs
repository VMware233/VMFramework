#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;
using VMFramework.OdinExtensions;

namespace VMFramework.Configuration
{
    public abstract partial class NumberOrVectorChooser<T, TRange>
    {
        public override IEnumerable<ValidationResult> GetValidationResults(GUIContent label)
        {
            foreach (var typeValidationResult in base.GetValidationResults(label))
            {
                yield return typeValidationResult;
            }

            if (isRandomValue)
            {
                if (randomType == RANGE_SELECT)
                {
                    if (rangeValue is ITypeValidationProvider provider)
                    {
                        foreach (var typeValidationResult in provider
                                     .GetValidationResults(label))
                        {
                            yield return typeValidationResult;
                        }
                    }

                    if (rangeValue.min.Equals(rangeValue.max))
                    {
                        yield return new("最小值和最大值相同，请用固定值代替", ValidateType.Warning);
                    }
                }
            }
        }
    }
}
#endif