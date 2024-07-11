#if UNITY_EDITOR && ODIN_INSPECTOR
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VMFramework.Core;

namespace VMFramework.OdinExtensions
{
    internal sealed class IsClassNameAttributeDrawer : MultipleValidationAttributeDrawer<IsClassNameAttribute>
    {
        protected override IEnumerable<ValidationResult> GetValidationResults(object value, GUIContent label)
        {
            if (value is not string className)
            {
                yield break;
            }

            if (className.IsNullOrWhiteSpace())
            {
                yield return new($"{label.text} cannot be null or empty.", ValidateType.Error);
                yield break;
            }

            if (className.Contains(' '))
            {
                yield return new($"{label.text} cannot contain spaces.", ValidateType.Error);
                yield break;
            }

            if (ReservedKeyWords.all.Contains(className))
            {
                yield return new($"{label.text} cannot be a reserved keyword.", ValidateType.Error);
                yield break;
            }

            if (className[0].IsLetter() == false && className[0] != '_')
            {
                yield return new($"{label.text} must start with a letter or underscore.", ValidateType.Error);
                yield break;
            }

            if (className.Any(c => c.IsLetterOrDigit() == false && c != '_'))
            {
                yield return new($"{label.text} can only contain letters, digits, and underscores.", ValidateType.Error);
            }
        }
    }
}
#endif