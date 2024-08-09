﻿using System.Collections.Generic;
using VMFramework.Properties;
using VMFramework.UI;

namespace VMFramework.GameLogicArchitecture
{
    public class VisualGameItem : GameItem, IVisualGameItem
    {
        protected IDescribedGamePrefab DescribedGamePrefab => (IDescribedGamePrefab)GamePrefab;

        public virtual string GetTooltipTitle()
        {
            return DescribedGamePrefab.name;
        }

        public virtual IEnumerable<TooltipPropertyInfo> GetTooltipProperties()
        {
            foreach (var config in TooltipPropertyManager.GetTooltipPropertyConfigsRuntime(id))
            {
                string AttributeValueGetter() =>
                    $"{config.property.name}:{config.property.GetValueString(this)}";

                yield return new()
                {
                    attributeValueGetter = AttributeValueGetter,
                    icon = config.property.icon,
                    isStatic = config.isStatic
                };
            }
        }

        public virtual string GetTooltipDescription()
        {
            return DescribedGamePrefab.Description;
        }
    }
}