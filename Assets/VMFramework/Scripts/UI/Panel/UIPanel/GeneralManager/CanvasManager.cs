using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using VMFramework.GameLogicArchitecture;
using VMFramework.Procedure;

namespace VMFramework.UI
{
    public sealed class CanvasManager : ManagerBehaviour<CanvasManager>
    {
        private static UIPanelGeneralSetting setting => GameCoreSettingBase.uiPanelGeneralSetting;
        
        [ShowInInspector]
        private static Transform canvasContainer;

        [ShowInInspector]
        private static readonly Dictionary<int, Canvas> canvasDict = new();

        protected override void OnBeforeInit()
        {
            base.OnBeforeInit();
            
            canvasContainer = GameCoreSettingBase.uiPanelGeneralSetting.container;
        }

        public static Canvas GetCanvas(int sortingOrder)
        {
            if (canvasDict.ContainsKey(sortingOrder) == false)
            {
                var (canvas, canvasScaler, graphicRaycaster) =
                    canvasContainer.CreateCanvas($"Canvas:{sortingOrder}");

                canvas.sortingOrder = sortingOrder;
                canvas.renderMode = RenderMode.ScreenSpaceOverlay;

                canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
                canvasScaler.referenceResolution = setting.defaultReferenceResolution;
                canvasScaler.matchWidthOrHeight = setting.defaultMatch;

                canvasDict[sortingOrder] = canvas;
            }

            return canvasDict[sortingOrder];
        }
    }
}