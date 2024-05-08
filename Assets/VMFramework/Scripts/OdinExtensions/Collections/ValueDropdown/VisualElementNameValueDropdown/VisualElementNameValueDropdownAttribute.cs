using System;
using UnityEngine.UIElements;

namespace VMFramework.OdinExtensions
{
    public class VisualElementNameValueDropdownAttribute : GeneralValueDropdownAttribute
    {
        public Type[] VisualElementTypes { get; }

        public VisualElementNameValueDropdownAttribute(params Type[] visualElementTypes)
        {
            VisualElementTypes = visualElementTypes;
        }

        public VisualElementNameValueDropdownAttribute()
        {
            VisualElementTypes = new[] { typeof(VisualElement) };
        }
    }
}