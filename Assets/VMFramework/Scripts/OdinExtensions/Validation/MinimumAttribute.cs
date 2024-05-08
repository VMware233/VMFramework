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

    public class MinimumAttribute : Attribute
    {
        public double MinValue;
        public string MinExpression;

        public MinimumAttribute(double minValue)
        {
            MinValue = minValue;
        }

        public MinimumAttribute(string minExpression)
        {
            MinExpression = minExpression;
        }
    }

    public interface IMinimumValueProvider
    {
        public void ClampByMinimum(double minimum);
    }

#if UNITY_EDITOR

    public class MinimumAttributeDrawer :
        OdinAttributeDrawer<MinimumAttribute>
    {
        private ValueResolver<double> minValueGetter;

        protected override void Initialize() =>
            minValueGetter = ValueResolver.Get(Property, Attribute.MinExpression,
                Attribute.MinValue);

        protected override void DrawPropertyLayout(GUIContent label)
        {
            var value = Property.ValueEntry.WeakSmartValue;

            if (value is not IMinimumValueProvider minimumValueProvider)
            {
                SirenixEditorGUI.ErrorMessageBox(
                    $"{Property.ValueEntry.TypeOfValue}没有实现{typeof(IMinimumValueProvider)}");
                CallNextDrawer(label);
                return;
            }

            if (minValueGetter.HasError)
            {
                SirenixEditorGUI.ErrorMessageBox(minValueGetter.ErrorMessage);
                CallNextDrawer(label);
            }
            else
            {
                double min = minValueGetter.GetValue();

                minimumValueProvider.ClampByMinimum(min);

                CallNextDrawer(label);
            }
        }
    }

#endif
}
