using System.Threading;
using VMFramework.Core;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

namespace VMFramework.UI
{
    public class UGUIPopupController : UGUITracingUIPanelController, ICloseAsyncUIPanelController
    {
        protected UGUIPopupPreset uguiPopupPreset { get; private set; }

        [ShowInInspector]
        protected Transform popupContainer;

        protected override void OnPreInit(UIPanelPreset preset)
        {
            base.OnPreInit(preset);

            uguiPopupPreset = preset as UGUIPopupPreset;

            uguiPopupPreset.AssertIsNotNull(nameof(uguiPopupPreset));

            popupContainer = visualObject.transform.QueryComponentInChildren<RectTransform>(
                uguiPopupPreset.popupContainerName, true);

            popupContainer.AssertIsNotNull(nameof(popupContainer));
        }

        protected override void OnOpenInstantly(IUIPanelController source)
        {
            base.OnOpenInstantly(source);

            popupContainer.ResetLocalArguments();

            if (uguiPopupPreset.enableContainerAnimation)
            {
                if (uguiPopupPreset.splitContainerAnimation)
                {
                    uguiPopupPreset.startContainerAnimation.Run(popupContainer);
                }
                else
                {
                    uguiPopupPreset.containerAnimation.Run(popupContainer);

                    if (uguiPopupPreset.autoCloseAfterContainerAnimation)
                    {
                        uguiPopupPreset.containerAnimation.totalDuration.DelayAction(this.Close);
                    }
                }
            }
        }

        CancellationTokenSource ICloseAsyncUIPanelController.closingCTS { get; set; }

        public async UniTask<bool> AwaitToClose(CancellationToken cts = default)
        {
            if (uguiPopupPreset.enableContainerAnimation)
            {
                if (uguiPopupPreset.splitContainerAnimation)
                {
                    uguiPopupPreset.startContainerAnimation.Kill(popupContainer);

                    await uguiPopupPreset.endContainerAnimation.RunAndAwaitForComplete(
                        popupContainer);
                }
                else
                {
                    uguiPopupPreset.containerAnimation.Kill(popupContainer);
                }
            }
            
            return true;
        }

        protected override void OnCloseInstantly(IUIPanelController source)
        {
            base.OnCloseInstantly(source);

            if (uguiPopupPreset.enableContainerAnimation &&
                uguiPopupPreset.splitContainerAnimation)
            {
                uguiPopupPreset.endContainerAnimation.Kill(popupContainer);
            }
        }
    }
}
