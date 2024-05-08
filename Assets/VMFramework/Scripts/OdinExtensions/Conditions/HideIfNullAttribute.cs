using System;
using System.Diagnostics;
using UnityEngine;

#if UNITY_EDITOR

using Sirenix.OdinInspector.Editor;

#endif

namespace VMFramework.OdinExtensions
{
    [Conditional("UNITY_EDITOR")]
    [AttributeUsage(AttributeTargets.Property)]

    public class HideIfNullAttribute : Attribute
    {

    }

#if UNITY_EDITOR

    [DrawerPriority(0, 0.0, 5000)]
    public sealed class
        HideIfNullAttributeDrawer : OdinAttributeDrawer<HideIfNullAttribute>
    {
        public override bool CanDrawTypeFilter(Type type) => type.IsClass;

        protected override void DrawPropertyLayout(GUIContent label)
        {
            if (Property.ValueEntry.WeakSmartValue == null)
            {
                Property.State.Visible = false;
            }

            CallNextDrawer(label);
        }
    }

#endif
}
