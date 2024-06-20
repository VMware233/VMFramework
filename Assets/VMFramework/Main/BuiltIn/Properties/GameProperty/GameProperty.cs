using System;
using VMFramework.GameLogicArchitecture;
using Sirenix.OdinInspector;
using UnityEngine;

namespace VMFramework.Properties
{
    public abstract partial class GameProperty : LocalizedGameTypedGamePrefab, IGameProperty
    {
        protected override string idSuffix => "property";

        public sealed override Type gameItemType => null;

        [TabGroup(TAB_GROUP_NAME, BASIC_CATEGORY)]
        [ShowInInspector]
        public abstract Type targetType { get; }

        [TabGroup(TAB_GROUP_NAME, BASIC_CATEGORY)]
        [PreviewField(50, ObjectFieldAlignment.Center)]
        [Required]
        public Sprite icon;

        Sprite IGameProperty.icon => icon;

        public abstract string GetValueString(object target);

        public override void CheckSettings()
        {
            base.CheckSettings();

            if (icon == null)
            {
                Debug.LogWarning($"{this} icon is not set.");
            }
        }
    }
}
