using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using VMFramework.GameLogicArchitecture;
using VMFramework.Core;
using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.Core.Linq;
using VMFramework.OdinExtensions;

namespace VMFramework.Maps
{
    public sealed partial class ExtendedRuleTile : GameTypedGamePrefab
    {
        public const string RULE_CATEGORY = "Rule";

        protected override string IDSuffix => "tile";

        public bool hasParent
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => ruleMode is RuleMode.Inheritance;
        }

        [TabGroup(TAB_GROUP_NAME, RULE_CATEGORY, SdfIconType.BoundingBoxCircles, TextColor = "red")]
        public bool enableUpdate = true;

#if UNITY_EDITOR
        [TabGroup(TAB_GROUP_NAME, RULE_CATEGORY)]
        [EnumToggleButtons, GUIColor(nameof(GetRuleModeColor))]
#endif
        public RuleMode ruleMode = RuleMode.Normal;

        [TabGroup(TAB_GROUP_NAME, RULE_CATEGORY)]
        [GamePrefabID(typeof(ExtendedRuleTile))]
        [InfoBox("不能为自己的ID", InfoMessageType.Error, "@parentRuleTileID == id")]
        [ShowIf(nameof(hasParent))]
        [IsNotNullOrEmpty]
        public string parentRuleTileID;

        public ExtendedRuleTile parentRuleTile
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get;
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private set;
        }

        [TabGroup(TAB_GROUP_NAME, RULE_CATEGORY)]
        [HideIf(nameof(hasParent))]
        [SerializeField]
        private List<SpriteLayer> defaultSpriteLayers = new();

#if UNITY_EDITOR
        [TabGroup(TAB_GROUP_NAME, RULE_CATEGORY)]
        [ListDrawerSettings(NumberOfItemsPerPage = 6, CustomAddFunction = nameof(AddRuleToRuleSetGUI))]
#endif
        public List<Rule> ruleSet = new();

        [TabGroup(TAB_GROUP_NAME, RUNTIME_DATA_CATEGORY)]
        [ShowInInspector]
        [ListDrawerSettings(NumberOfItemsPerPage = 4)]
        private List<Rule> runtimeRuleSet = new();

        #region Inheritance

        private bool hasInitInheritance = false;

        private void InitInheritance()
        {
            if (hasInitInheritance)
            {
                return;
            }

            if (ruleMode == RuleMode.Normal)
            {
                runtimeRuleSet = new();
                
                foreach (var rule in ruleSet)
                {
                    foreach (var generatedRule in rule.GenerateRules())
                    {
                        runtimeRuleSet.Add(generatedRule);
                    }
                }

                hasInitInheritance = true;
                return;
            }

            parentRuleTile.InitInheritance();

            runtimeRuleSet = new();

            foreach (var rule in parentRuleTile.runtimeRuleSet)
            {
                runtimeRuleSet.Add(rule.GetClone(false, false));
            }

            foreach (var rule in ruleSet)
            {
                foreach (var generatedRule in rule.GenerateRules())
                {
                    int count = 0;
                    foreach (var subLimitRule in GetRuntimeRuleWithSubLimitsOf(generatedRule))
                    {
                        foreach (var layer in rule.layers)
                        {
                            subLimitRule.layers.RemoveAll(spriteLayer => spriteLayer.layer == layer.layer);

                            subLimitRule.layers.Add(layer);
                        }

                        count++;
                    }

                    if (count == 0)
                    {
                        runtimeRuleSet.Add(generatedRule);
                    }
                }
            }

            hasInitInheritance = true;
        }

        #endregion

        #region Get Rule

        private IEnumerable<Rule> GetRuntimeRuleWithSubLimitsOf(Rule otherRule)
        {
            foreach (var rule in runtimeRuleSet)
            {
                if (rule.HasSubLimitsOf(otherRule))
                {
                    yield return rule;
                }
            }
        }

        #endregion

        #region Rule

        public IReadOnlyList<SpriteLayer> GetDefaultSpriteLayers()
        {
            if (hasParent)
            {
                return parentRuleTile.GetDefaultSpriteLayers();
            }

            return defaultSpriteLayers;
        }

        public IReadOnlyList<SpriteLayer> GetSpriteLayers(EightDirectionsNeighbors<ExtendedRuleTile> neighbor)
        {
            var rule = GetRule(neighbor);

            return rule == null ? GetDefaultSpriteLayers() : rule.layers;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Rule GetRule(EightDirectionsNeighbors<ExtendedRuleTile> neighbor)
        {
            return this.GetRule(neighbor, runtimeRuleSet);
        }

        #endregion
    }
}