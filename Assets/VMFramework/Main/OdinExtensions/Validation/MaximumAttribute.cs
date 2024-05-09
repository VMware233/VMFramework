using System;
using System.Diagnostics;
using UnityEngine;

#if UNITY_EDITOR
using Sirenix.OdinInspector.Editor;
using Sirenix.OdinInspector.Editor.ValueResolvers;
using Sirenix.Utilities.Editor;
#endif

namespace VMFramework.OdinExtensions
{
    [AttributeUsage(AttributeTargets.All)]
    [Conditional("UNITY_EDITOR")]

    public class MaximumAttribute : Attribute
    {
        public double MaxValue;
        public string MaxExpression;

        public MaximumAttribute(double maxValue)
        {
            MaxValue = maxValue;
        }

        public MaximumAttribute(string maxExpression)
        {
            MaxExpression = maxExpression;
        }
    }

    public interface IMaximumValueProvider
    {
        public void ClampByMaximum(double maximum);
    }

#if UNITY_EDITOR

    public class MaximumAttributeDrawer :
        OdinAttributeDrawer<MaximumAttribute>
    {
        private ValueResolver<double> maxValueGetter;

        protected override void Initialize() =>
            maxValueGetter = ValueResolver.Get(Property, Attribute.MaxExpression,
                Attribute.MaxValue);

        protected override void DrawPropertyLayout(GUIContent label)
        {
            var value = Property.ValueEntry.WeakSmartValue;

            if (value is not IMaximumValueProvider minimumValueProvider)
            {
                SirenixEditorGUI.ErrorMessageBox(
                    $"{Property.ValueEntry.TypeOfValue}没有实现{typeof(IMaximumValueProvider)}");
                CallNextDrawer(label);
                return;
            }

            if (maxValueGetter.HasError)
            {
                SirenixEditorGUI.ErrorMessageBox(maxValueGetter.ErrorMessage);
                CallNextDrawer(label);
            }
            else
            {
                double max = maxValueGetter.GetValue();

                minimumValueProvider.ClampByMaximum(max);

                CallNextDrawer(label);
            }
        }
    }

#endif
}