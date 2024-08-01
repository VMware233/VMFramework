using System;
using VMFramework.Core;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace VMFramework.UI
{
    public class UGUIPanelController : UIPanelController, IUIPanelController
    {
        [ShowInInspector]
        protected GameObject VisualObject { get; private set; }

        [ShowInInspector]
        protected RectTransform VisualRectTransform { get; private set; }

        protected RectTransform RectTransform { get; private set; }

        protected UGUIPanelPreset UGUIPanelPreset { get; private set; }

        [ShowInInspector]
        protected Canvas Canvas { get; private set; }

        protected CanvasScaler CanvasScaler { get; private set; }

        protected override void OnPreInit(UIPanelPreset preset)
        {
            base.OnPreInit(preset);

            UGUIPanelPreset = preset as UGUIPanelPreset;

            UGUIPanelPreset.AssertIsNotNull(nameof(UGUIPanelPreset));

            Canvas = CanvasManager.GetCanvas(preset.sortingOrder);

            CanvasScaler = Canvas.GetComponent<CanvasScaler>();

            transform.SetParent(Canvas.transform);

            transform.ResetLocalArguments();

            RectTransform = gameObject.GetOrAddComponent<RectTransform>();

            RectTransform.anchorMin = Vector2.zero;
            RectTransform.anchorMax = Vector2.one;
            RectTransform.offsetMin = Vector2.zero;
            RectTransform.offsetMax = Vector2.zero;

            VisualObject = Instantiate(UGUIPanelPreset.prefab, transform);

            VisualObject.AssertIsNotNull(nameof(VisualObject));

            VisualRectTransform = VisualObject.GetComponent<RectTransform>();

            VisualRectTransform.AssertIsNotNull(nameof(VisualRectTransform));
        }

        #region Open

        public override event Action<IUIPanelController> OnOpenEvent;
        
        void IUIPanelController.PreOpen(IUIPanelController source)
        {
            OnOpen(source);
            OnOpenEvent?.Invoke(this);
        }
        
        protected virtual void OnOpen(IUIPanelController source)
        {
            if (VisualObject != null)
            {
                VisualObject.SetActive(true);
            }
            else
            {
                Debugger.LogWarning("No visual object found for this panel.");
            }
        }

        #endregion

        #region Close

        public override event Action<IUIPanelController> OnCloseEvent;
        
        void IUIPanelController.PostClose(IUIPanelController source)
        {
            OnClose(source);
            OnCloseEvent?.Invoke(this);
        }

        protected virtual void OnClose(IUIPanelController source)
        {
            if (VisualObject != null)
            {
                VisualObject.SetActive(false);
            }
            else
            {
                Debugger.LogWarning("No visual object found for this panel.");
            }
        }

        #endregion
    }
}
