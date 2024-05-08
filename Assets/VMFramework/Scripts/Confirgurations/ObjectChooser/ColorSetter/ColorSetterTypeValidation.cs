#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;
using VMFramework.OdinExtensions;

namespace VMFramework.Configuration
{
    public sealed partial class ColorSetter
    {
        public override IEnumerable<ValidationResult> GetValidationResults(GUIContent label)
        {
            foreach (var typeValidationResult in base.GetValidationResults(label))
            {
                yield return typeValidationResult;
            }

            if (ContainsColorWithLowAlpha())
            {
                yield return new($"{label?.text}存在颜色的Alpha值过小", ValidateType.Warning);
            }
        }
    }
}
#endif