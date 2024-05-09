using VMFramework.Core;
using System;
using System.Diagnostics;
using UnityEngine;

#if UNITY_EDITOR
using Sirenix.OdinInspector.Editor.ValueResolvers;
using Sirenix.OdinInspector.Editor;
#endif

namespace VMFramework.OdinExtensions
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property |
                    AttributeTargets.Parameter, AllowMultiple = true)]
    [Conditional("UNITY_EDITOR")]

    public class RequiredComponentAttribute : SingleValidationAttribute
    {
        public Type ComponentType;
        public string ComponentTypeGetter;

        public RequiredComponentAttribute(string componentTypeGetter) : base()
        {
            ComponentTypeGetter = componentTypeGetter;
        }

        public RequiredComponentAttribute(Type componentType) : base()
        {
            ComponentType = componentType;
        }

        public RequiredComponentAttribute(Type componentType, string errorMessage) :
            base(errorMessage)
        {
            ComponentType = componentType;
        }
    }

#if UNITY_EDITOR

    [DrawerPriority(DrawerPriorityLevel.WrapperPriority)]
    public class RequiredComponentAttributeDrawer : 
            SingleValidationAttributeDrawer<RequiredComponentAttribute>
    {
        private ValueResolver<Type> componentTypeGetter;
        private ValueResolver<string> errorMessageGetter;

        protected override void Initialize()
        {
            if (Attribute.Message.IsNullOrEmpty() == false)
            {
                errorMessageGetter =
                    ValueResolver.GetForString(Property, Attribute.Message);
            }

            if (Attribute.ComponentTypeGetter.IsNullOrEmpty() == false)
            {
                componentTypeGetter =
                    ValueResolver.Get<Type>(Property, Attribute.ComponentTypeGetter);
            }
        }

        protected override string GetDefaultMessage(GUIContent label)
        {
            var componentType =
                Attribute.ComponentType ?? componentTypeGetter?.GetValue();

            if (componentType == null)
            {
                return string.Empty;
            }

            var name = componentType.Name;

            return errorMessageGetter == null
                ? $"{name} is Required"
                : errorMessageGetter.ErrorMessage;
        }

        protected override bool Validate(object value)
        {
            var componentType =
                Attribute.ComponentType ?? componentTypeGetter?.GetValue();

            if (componentType == null)
            {
                return true;
            }

            var gameObject = value as GameObject;

            if (gameObject == null)
            {
                return false;
            }

            if (gameObject.HasComponent(componentType) == false)
            {
                return false;
            }

            return true;
        }
    }

#endif
}
