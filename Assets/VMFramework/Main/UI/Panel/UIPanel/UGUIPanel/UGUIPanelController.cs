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
        protected GameObject visualObject { get; private set; }

        [ShowInInspector]
        protected RectTransform visualRectTransform { get; private set; }

        protected RectTransform rectTransform { get; private set; }

        protected UGUIPanelPreset uguiPanelPreset { get; private set; }

        [ShowInInspector]
        protected Canvas canvas { get; private set; }

        protected CanvasScaler canvasScaler { get; private set; }

        protected override void OnPreInit(UIPanelPreset preset)
        {
            base.OnPreInit(preset);

            uguiPanelPreset = preset as UGUIPanelPreset;

            uguiPanelPreset.AssertIsNotNull(nameof(uguiPanelPreset));

            canvas = CanvasManager.GetCanvas(preset.sortingOrder);

            canvasScaler = canvas.GetComponent<CanvasScaler>();

            transform.SetParent(canvas.transform);

            transform.ResetLocalArguments();

            rectTransform = gameObject.GetOrAddComponent<RectTransform>();

            rectTransform.anchorMin = Vector2.zero;
            rectTransform.anchorMax = Vector2.one;
            rectTransform.offsetMin = Vector2.zero;
            rectTransform.offsetMax = Vector2.zero;

            visualObject = Instantiate(uguiPanelPreset.prefab, transform);

            visualObject.AssertIsNotNull(nameof(visualObject));

            visualRectTransform = visualObject.GetComponent<RectTransform>();

            visualRectTransform.AssertIsNotNull(nameof(visualRectTransform));
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
            
        }
        
        protected override void OnOpenInstantly(IUIPanelController source)
        {
            base.OnOpenInstantly(source);

            visualObject.SetActive(true);
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
            
        }

        protected override void OnCloseInstantly(IUIPanelController source)
        {
            base.OnCloseInstantly(source);

            if (visualObject != null)
            {
                visualObject.SetActive(false);
            }
        }

        #endregion
    }
}
