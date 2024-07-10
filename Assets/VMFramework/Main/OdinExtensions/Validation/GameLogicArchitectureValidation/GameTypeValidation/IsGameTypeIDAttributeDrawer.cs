#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;

namespace VMFramework.OdinExtensions
{
    internal sealed class IsGameTypeIDAttributeDrawer : MultipleValidationAttributeDrawer<IsGameTypeIDAttribute>
    {
        protected override IEnumerable<ValidationResult> GetValidationResults(object value, GUIContent label)
        {
            if (value is not string id)
            {
                yield break;
            }

            foreach (var result in GameLogicArchitectureAttributeUtility.ValidateID(id))
            {
                yield return result;
            }

            if (id.TrimEnd('_', ' ').EndsWith("_type") == false)
            {
                yield return new("The ID of a GameType must end with '_type'", ValidateType.Warning);
            }
        }
    }
}
#endif