﻿using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using VMFramework.Configuration;
using VMFramework.Core;
using VMFramework.GameLogicArchitecture;
using VMFramework.Procedure;

namespace VMFramework.UI
{
    [ManagerCreationProvider(ManagerType.UICore)]
    public sealed class TooltipManager : ManagerBehaviour<TooltipManager>
    {
        private static TooltipGeneralSetting tooltipGeneralSetting => UISetting.TooltipGeneralSetting;
        
        public static void Open(ITooltipProvider tooltipProvider, IUIPanelController source)
        {
            if (tooltipProvider == null)
            {
                Debugger.LogWarning($"{nameof(tooltipProvider)} is Null");
                return;
            }

            if (tooltipProvider.ShowTooltip() == false)
            {
                return;
            }

            string tooltipID = null;
            TooltipOpenInfo info = new();
            
            bool priorityFound = false;

            if (tooltipProvider is IReadOnlyGameTypeOwner readOnlyGameTypeOwner)
            {
                if (tooltipGeneralSetting.tooltipIDBindConfigs.TryGetConfigRuntime(
                        readOnlyGameTypeOwner.GameTypeSet, out var tooltipBindConfig))
                {
                    tooltipID = tooltipBindConfig.tooltipID;
                }

                if (tooltipGeneralSetting.tooltipPriorityBindConfigs.TryGetConfigRuntime(
                        readOnlyGameTypeOwner.GameTypeSet, out var priorityBindConfig))
                {
                    info.priority = priorityBindConfig.priority;
                    priorityFound = true;
                }
            }
            
            tooltipID ??= tooltipGeneralSetting.defaultTooltipID;

            if (priorityFound == false)
            {
                info.priority = tooltipGeneralSetting.defaultPriority;
            }

            if (UIPanelPool.TryGetUniquePanelWithWarning(tooltipID, out ITooltip tooltip) == false)
            {
                return;
            }

            tooltip.Open(tooltipProvider, source, info);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Close(ITooltipProvider tooltipProvider)
        {
            if (tooltipProvider == null)
            {
                Debugger.LogWarning($"{nameof(tooltipProvider)} is Null");
                return;
            }

            foreach (var tooltip in UIPanelPool.GetUniquePanels<ITooltip>())
            {
                tooltip.Close(tooltipProvider);
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<ITooltip> GetTooltips()
        {
            return UIPanelPool.GetUniquePanels<ITooltip>();
        }
    }
}