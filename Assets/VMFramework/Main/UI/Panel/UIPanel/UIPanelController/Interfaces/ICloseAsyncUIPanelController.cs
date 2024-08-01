﻿using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VMFramework.Core;

namespace VMFramework.UI
{
    public interface ICloseAsyncUIPanelController : IUIPanelController
    {
        protected CancellationTokenSource closingCTS { get; set; }
        
        async void IUIPanelController.Close(IUIPanelController source)
        {
            if (isOpening)
            {
                StopOpening();
            }
            else if (isOpened == false)
            {
                Debugger.LogWarning("UIPanelController is already closed.");
                return;
            }
            
            if (isClosing)
            {
                Debugger.LogWarning("UIPanelController is already closing.");
                return;
            }
            
            isClosing = true;
            
            if (SourceUIPanel != null)
            {
                SourceUIPanel.OnCloseInstantlyEvent -= Close;
            }
            SourceUIPanel = null;
            
            closingCTS = new CancellationTokenSource();
            
            var closeResult = await AwaitToClose(closingCTS.Token);

            isClosing = false;
            
            if (closeResult)
            {
                CloseInstantly(source);
            }
            
            PostClose(source);
        }

        public UniTask<bool> AwaitToClose(CancellationToken token = default);

        void IUIPanelController.StopClosing()
        {
            if (isClosing)
            {
                isClosing = false;
                
                if (closingCTS == null)
                {
                    Debugger.LogWarning(
                        "UIPanelController is already closing, but no cancellation token source is available.");
                    return;
                }
                
                closingCTS.Cancel();
            }
            else
            {
                Debugger.LogWarning("UIPanelController is not opening.");
            }
        }
    }
}