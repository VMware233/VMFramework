using System;
using System.Diagnostics;
using UnityEngine;

#if UNITY_EDITOR

using Sirenix.OdinInspector.Editor;
using UnityEditor;

#endif

namespace VMFramework.OdinExtensions
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    [Conditional("UNITY_EDITOR")]

    public class LayerAttribute : Attribute
    {

    }

#if UNITY_EDITOR

    [DrawerPriority(DrawerPriorityLevel.AttributePriority)]
    public class LayerAttributeDrawer : OdinAttributeDrawer<LayerAttribute, int>
    {
        protected override void DrawPropertyLayout(GUIContent label)
        {
            ValueEntry.SmartValue = label == null
                ? EditorGUILayout.LayerField(ValueEntry.SmartValue)
                : EditorGUILayout.LayerField(label, ValueEntry.SmartValue);
        }
    }

#endif
}