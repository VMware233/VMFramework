#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VMFramework.Core;
using VMFramework.GameLogicArchitecture;
using VMFramework.OdinExtensions;

namespace VMFramework.Configuration
{
    public partial class GamePrefabIDSetter<TGamePrefab>
    {
        public override IEnumerable<ValidationResult> GetValidationResults(GUIContent label)
        {
            foreach (var result in base.GetValidationResults(label))
            {
                yield return result;
            }
            
            if (isRandomValue == false)
            {
                if (value.id.IsNullOrEmptyAfterTrim())
                {
                    yield return new("ID不能为空", ValidateType.Error);
                }

                if (GamePrefabManager.ContainsGamePrefab<TGamePrefab>(value.id) == false)
                {
                    yield return new("不存在的GamePrefab ID", ValidateType.Error);
                }
            }
            else
            {
                if (isWeightedSelect)
                {
                    if (weightedSelectItems.Any(item => item.value.id.IsNullOrEmptyAfterTrim()))
                    {
                        yield return new("存在空ID", ValidateType.Error);
                    }

                    foreach (var item in weightedSelectItems)
                    {
                        if (GamePrefabManager.ContainsGamePrefab<TGamePrefab>(item.value.id) == false)
                        {
                            yield return new($"不存在的GamePrefab ID:{item.value.id}", ValidateType.Error);
                        }
                    }
                }
                else if (isCircularSelect)
                {
                    if (circularSelectItems.Any(item => item.value.id.IsNullOrEmptyAfterTrim()))
                    {
                        yield return new("存在空ID", ValidateType.Error);
                    }
                    
                    foreach (var item in circularSelectItems)
                    {
                        if (GamePrefabManager.ContainsGamePrefab<TGamePrefab>(item.value.id) == false)
                        {
                            yield return new($"不存在的GamePrefab ID:{item.value.id}", ValidateType.Error);
                        }
                    }
                }
            }
        }
    }
}
#endif