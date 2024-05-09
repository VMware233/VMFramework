using System.Collections;
using UnityEngine;

#if UNITY_EDITOR
using Sirenix.OdinInspector.Editor.ValueResolvers;
#endif

namespace VMFramework.OdinExtensions
{
    public class ValidateListLengthAttribute : SingleValidationAttribute
    {
        public int MinLength;

        public int MaxLength;

        public string MinLengthGetter;
        
        public string MaxLengthGetter;

        public ValidateListLengthAttribute(int fixedLength)
        {
            MinLength = fixedLength;
            MaxLength = fixedLength;
        }

        public ValidateListLengthAttribute(int minLength, int maxLength)
        {
            MinLength = minLength;
            MaxLength = maxLength;
        }

        public ValidateListLengthAttribute(int minLength, string maxLengthGetter)
        {
            MinLength = minLength;
            MaxLengthGetter = maxLengthGetter;
        }

        public ValidateListLengthAttribute(string fixedLengthGetter)
        {
            MinLengthGetter = fixedLengthGetter;
            MaxLengthGetter = fixedLengthGetter;
        }

        public ValidateListLengthAttribute(string minLengthGetter, string maxLengthGetter)
        {
            MinLengthGetter = minLengthGetter;
            MaxLengthGetter = maxLengthGetter;
        }

        public ValidateListLengthAttribute(string minLengthGetter, int maxLength)
        {
            MinLengthGetter = minLengthGetter;
            MaxLength = maxLength;
        }
    }

#if UNITY_EDITOR

    public class ValidateListLengthAttributeDrawer : 
        SingleValidationAttributeDrawer<ValidateListLengthAttribute>
    {
        private ValueResolver<int> minLengthResolver;
        private ValueResolver<int> maxLengthResolver;

        protected override void Initialize()
        {
            base.Initialize();

            minLengthResolver = ValueResolver.Get(Property,
                Attribute.MinLengthGetter, Attribute.MinLength);
            maxLengthResolver = ValueResolver.Get(Property,
                Attribute.MaxLengthGetter, Attribute.MaxLength);
        }

        protected override string GetDefaultMessage(GUIContent label)
        {
            var minLength = minLengthResolver.GetValue();
            var maxLength = maxLengthResolver.GetValue();

            var labelName = label?.text;
            if (minLength == maxLength)
            {
                return $"{labelName}长度必须为{minLength}";
            }

            if (minLength <= 0)
            {
                return $"{labelName}长度必须小于等于{maxLength}";
            }

            if (maxLength >= int.MaxValue)
            {
                return $"{labelName}长度必须大于等于{minLength}";
            }

            return $"{labelName}长度必须在{minLength}和{maxLength}之间";
        }

        protected override bool Validate(object value)
        {
            if (value is IList list)
            {
                var minLength = minLengthResolver.GetValue();
                var maxLength = maxLengthResolver.GetValue();

                if (list.Count < minLength || list.Count > maxLength)
                {
                    return false;
                }
            }

            return true;
        }
    }

#endif
}
