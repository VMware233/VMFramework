#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;
using VMFramework.Core.Linq;
using VMFramework.OdinExtensions;

namespace VMFramework.Configuration
{
    [TypeValidation]
    public partial class ObjectChooser<T> : ITypeValidationProvider
    {
        public virtual IEnumerable<ValidationResult> GetValidationResults(
            GUIContent label)
        {
            if (isRandomValue == false)
            {
                yield break;
            }
            
            if (randomType == WEIGHTED_SELECT)
            {
                if (weightedSelectItems.IsNullOrEmpty())
                {
                    yield return new("概率权值列表不能为空", ValidateType.Error);
                    yield break;
                }
                    
                if (IsWeightedSelectItemsContainsSameValue())
                {
                    yield return new("概率权值列表中存在相同的值", ValidateType.Warning);
                }

                if (IsWeightedSelectRatiosCoprime() == false)
                {
                    yield return new("概率权值列表中的占比不是互质，可以化简",
                        ValidateType.Warning);
                }

                if (IsWeightedSelectRatiosAllZero())
                {
                    yield return new("占比不能全为0", ValidateType.Error);
                }

                if (weightedSelectItems.Count == 1)
                {
                    yield return new("概率权值列表中只有一个选项，请用固定值代替", 
                        ValidateType.Warning);
                }
            }
            else if (randomType == CIRCULAR_SELECT)
            {
                if (circularSelectItems.IsNullOrEmpty())
                {
                    yield return new("循环列表不能为空", ValidateType.Error);
                    yield break;
                }
                    
                if (startCircularIndex >= circularSelectItems.Count)
                {
                    yield return new("起始索引超出循环列表长度", ValidateType.Error);
                }
            }
        }
    }
}
#endif