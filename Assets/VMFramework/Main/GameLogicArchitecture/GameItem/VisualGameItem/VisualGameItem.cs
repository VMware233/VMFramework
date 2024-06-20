using System.Collections.Generic;
using Sirenix.OdinInspector;
using VMFramework.Properties;
using VMFramework.UI;

namespace VMFramework.GameLogicArchitecture
{
    public class VisualGameItem : GameItem, IVisualGameItem
    {
        protected IDescribedGamePrefab describedGamePrefab => (IDescribedGamePrefab)gamePrefab;

        public virtual string GetTooltipTitle()
        {
            return describedGamePrefab.name;
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
            return describedGamePrefab.description;
        }
    }
}