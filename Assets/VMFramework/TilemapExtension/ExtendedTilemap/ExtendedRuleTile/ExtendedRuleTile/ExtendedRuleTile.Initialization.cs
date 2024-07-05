using System;
using System.Collections.Generic;
using VMFramework.Configuration;
using VMFramework.GameLogicArchitecture;
using VMFramework.Procedure;

namespace VMFramework.ExtendedTilemap
{
    public partial class ExtendedRuleTile : IInitializer
    {
        public override void CheckSettings()
        {
            base.CheckSettings();

            defaultSpriteLayers.CheckSettings();
            ruleSet.CheckSettings();
        }

        protected override IEnumerable<InitializationAction> GetInitializationActions()
        {
            foreach (var action in base.GetInitializationActions())
            {
                yield return action;
            }

            yield return new(InitializationOrder.PostInit, OnPostInit, this);
            yield return new(InitializationOrder.InitComplete, OnInitComplete, this);
        }

        protected override void OnInit()
        {
            base.OnInit();

            if (hasParent)
            {
                parentRuleTile = GamePrefabManager.GetGamePrefabStrictly<ExtendedRuleTile>(parentRuleTileID);
            }
            
            defaultSpriteLayers.Init();
        }

        private void OnPostInit(Action onAction)
        {
            InitInheritance();
            onAction();
        }

        private void OnInitComplete(Action onAction)
        {
            runtimeRuleSet.Init();
            onAction();
        }
    }
}