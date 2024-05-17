using System.Collections.Generic;
using VMFramework.Core;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UIElements;

namespace VMFramework.UI
{
    public class UIToolkitTracingTooltipController : UIToolkitTracingUIPanelController, ITracingTooltip
    {
        protected static readonly HashSet<UIToolkitTracingTooltipController> openedTooltips = new();

        protected UIToolkitTracingTooltipPreset tracingTooltipPreset { get; private set; }

        [ShowInInspector] 
        protected Label title, description;

        [ShowInInspector] 
        protected VisualElement propertyContainer;

        [ShowInInspector] 
        protected ITracingTooltipProvider tooltipProvider;

        [ShowInInspector] 
        private List<TracingTooltipProviderUIToolkitRenderUtility.DynamicPropertyInfo> dynamicPropertyInfos = new();

        [ShowInInspector]
        private Dictionary<string, GroupVisualElement> groupVisualElements = new();

        protected override void OnPreInit(UIPanelPreset preset)
        {
            base.OnPreInit(preset);

            tracingTooltipPreset = preset as UIToolkitTracingTooltipPreset;

            tracingTooltipPreset.AssertIsNotNull(nameof(tracingTooltipPreset));
        }

        protected override void OnOpenInstantly(IUIPanelController source)
        {
            base.OnOpenInstantly(source);

            title = rootVisualElement.Q<Label>(tracingTooltipPreset.titleLabelName);
            description = rootVisualElement.Q<Label>(tracingTooltipPreset.descriptionLabelName);
            propertyContainer = rootVisualElement.Q(tracingTooltipPreset.propertyContainerName);

            title.AssertIsNotNull(nameof(title));
            description.AssertIsNotNull(nameof(description));
            propertyContainer.AssertIsNotNull(nameof(propertyContainer));

            openedTooltips.Add(this);

            dynamicPropertyInfos.Clear();

            groupVisualElements.Clear();
        }

        protected override void OnCloseInstantly(IUIPanelController source)
        {
            base.OnCloseInstantly(source);

            if (tooltipProvider != null)
            {
                if (tooltipProvider.TryGetTooltipBindGlobalEvent(out var gameEvent))
                {
                    gameEvent.OnEnabledChangedEvent -= OnGlobalEventEnabledStateChanged;
                }

                tooltipProvider = null;
            }

            title = null;
            description = null;

            openedTooltips.Remove(this);
        }

        private void FixedUpdate()
        {
            if (isOpened && isClosing == false)
            {
                if (tooltipProvider.isDestroyed)
                {
                    this.Close();
                }
            }
            
            if (isOpened)
            {
                if (dynamicPropertyInfos.Count > 0)
                {
                    foreach (var attributeInfo in dynamicPropertyInfos)
                    {
                        attributeInfo.iconLabel.SetContent(attributeInfo.valueGetter());
                    }
                }
            }
        }

        private void OnGlobalEventEnabledStateChanged(bool previous, bool current)
        {
            if (current == false)
            {
                this.Close();
            }
        }

        public void Open(ITracingTooltipProvider tooltipProvider, IUIPanelController source)
        {
            if (this.tooltipProvider == tooltipProvider)
            {
                return;
            }

            if (tooltipProvider.TryGetTooltipBindGlobalEvent(out var gameEvent))
            {
                if (gameEvent.isEnabled == false)
                {
                    return;
                }

                gameEvent.OnEnabledChangedEvent += OnGlobalEventEnabledStateChanged;
            }

            if (this.tooltipProvider != null)
            {
                if (tooltipProvider.GetTooltipPriority() <
                    this.tooltipProvider.GetTooltipPriority())
                {
                    return;
                }
            }

            if (openedTooltips.Count > 0)
            {
                var minPriority = int.MinValue;

                var willClosedTooltips = new List<UIToolkitTracingTooltipController>();

                foreach (var openedTooltip in openedTooltips)
                {
                    var openedTooltipPriority =
                        openedTooltip.tracingTooltipPreset.tooltipPriority;

                    if (openedTooltipPriority < tracingTooltipPreset.tooltipPriority)
                    {
                        willClosedTooltips.Add(openedTooltip);
                    }
                    else if (openedTooltipPriority > minPriority)
                    {
                        minPriority = openedTooltip.tracingTooltipPreset.tooltipPriority;
                    }
                }

                foreach (var willClosedTooltip in willClosedTooltips)
                {
                    willClosedTooltip.Close();
                }

                if (minPriority > tracingTooltipPreset.tooltipPriority)
                {
                    return;
                }
            }

            this.Open(source);

            this.tooltipProvider = tooltipProvider;

            propertyContainer.Clear();

            var renderResult = tooltipProvider.RenderToVisualElement(title, description,
                propertyContainer, AddVisualElement);

            groupVisualElements = renderResult.groups;
            dynamicPropertyInfos = renderResult.dynamicPropertyInfos;
        }

        public void Close(ITracingTooltipProvider tooltipProvider)
        {
            if (isClosing)
            {
                Debug.LogWarning("Tooltip is already closing.");
                return;
            }
            
            if (this.tooltipProvider == tooltipProvider)
            {
                this.Close();
            }
        }
    }
}
