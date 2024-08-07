﻿#if UNITY_EDITOR && ODIN_INSPECTOR
using System.Collections;
using System.Linq;
using UnityEngine;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using VMFramework.Editor.BatchProcessor;

namespace VMFramework.Editor
{
    [DrawerPriority(DrawerPriorityLevel.SuperPriority)]
    internal sealed class BatchProcessorContextMenuDrawer<T> : OdinValueDrawer<T>, IDefinesGenericMenuItems
    {
        protected override void DrawPropertyLayout(GUIContent label)
        {
            CallNextDrawer(label);
        }

        void IDefinesGenericMenuItems.PopulateGenericMenu(InspectorProperty property, GenericMenu genericMenu)
        {
            var value = property.ValueEntry.WeakSmartValue;

            if (value == null)
            {
                return;
            }

            if (value is ICollection collection)
            {
                if (collection.Count == 0)
                {
                    return;
                }

                genericMenu.AddSeparator("");

                genericMenu.AddItem(new GUIContent(BatchProcessorNames.BATCH_PROCESS_NAME), false,
                    () => { BatchProcessorWindow.OpenWindow(collection.Cast<object>()); });

                genericMenu.AddItem(new GUIContent(BatchProcessorNames.ADD_TO_BATCH_PROCESSOR_NAME), false,
                    () => { BatchProcessorWindow.AddToWindow(collection.Cast<object>()); });
            }
            else
            {
                genericMenu.AddItem(new GUIContent(BatchProcessorNames.ADD_TO_BATCH_PROCESSOR_NAME), false,
                    () => { BatchProcessorWindow.AddToWindow(value); });
            }
        }
    }
}

#endif