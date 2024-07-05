using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Sirenix.OdinInspector;
using UnityEngine;

namespace VMFramework.UI
{
    public partial class TracingUIManager
    {
        #region Debug Utility

        private static void WarningAlreadyTracing(ITracingUIPanel tracingUIPanel, TracingType tracingType)
        {
            if (tracingType == TracingType.Transform)
            {
                Debug.LogWarning($"{tracingUIPanel} already tracing a transform");
            }
            else if (tracingType == TracingType.MousePosition)
            {
                Debug.LogWarning($"{tracingUIPanel} already tracing mouse position");
            }
        }

        #endregion

        [ShowInInspector]
        private static readonly Queue<TracingInfo> infoCaches = new();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void StartTracing(ITracingUIPanel tracingUIPanel, TracingConfig tracingConfig)
        {
            StopTracing(tracingUIPanel);

            TracingInfo info;

            if (infoCaches.Count > 0)
            {
                info = infoCaches.Dequeue();
            }
            else
            {
                info = new();
            }
            
            info.SetConfig(tracingConfig);
            
            allTracingInfos.Add(tracingUIPanel, info);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool StopTracing(ITracingUIPanel tracingUIPanel)
        {
            if (allTracingInfos.Remove(tracingUIPanel, out var info))
            {
                infoCaches.Enqueue(info);
                return true;
            }
            
            return false;
        }
    }
}