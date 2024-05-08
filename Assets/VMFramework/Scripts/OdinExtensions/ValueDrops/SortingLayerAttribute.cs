using System;
using System.Linq;
using UnityEngine;
using System.Diagnostics;

#if UNITY_EDITOR

using UnityEditor;
using Sirenix.OdinInspector.Editor;

#endif

namespace VMFramework.OdinExtensions
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    [Conditional("UNITY_EDITOR")]

    public class SortingLayerAttribute : Attribute
    {

    }

#if UNITY_EDITOR

    [DrawerPriority(DrawerPriorityLevel.AttributePriority)]
    public class
        SortingLayerDrawer : OdinAttributeDrawer<SortingLayerAttribute, string>
    {
        protected override void DrawPropertyLayout(GUIContent label)
        {
            var rect = EditorGUILayout.GetControlRect();
            var layerNames =
                SortingLayer.layers.Select(layer => layer.name).ToArray();

            var currentLayerName = ValueEntry.SmartValue;
            var selectedLayerIndex = Array.IndexOf(layerNames, currentLayerName);
            EditorGUI.BeginChangeCheck();

            var newSelectedIndex = EditorGUI.Popup(rect, label.text,
                selectedLayerIndex, layerNames);
            if (EditorGUI.EndChangeCheck())
            {
                ValueEntry.SmartValue = layerNames[newSelectedIndex];
            }
        }
    }

#endif
}
