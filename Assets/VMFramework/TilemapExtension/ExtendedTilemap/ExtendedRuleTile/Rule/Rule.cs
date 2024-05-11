using System.Collections.Generic;
using Sirenix.OdinInspector;
using VMFramework.Configuration;
using VMFramework.Core;
using VMFramework.Core.Linq;

namespace VMFramework.ExtendedTilemap
{
    public partial class Rule : BaseConfigClass
    {
        [LabelText("是否开启")]
        [HorizontalGroup("Config")]
        public bool enable = true;

        [LabelText("是否开启动画")]
        [HorizontalGroup("Config")]
        public bool enableAnimation = false;

        [LabelText("层级"), HorizontalGroup("Rule")]
        [HideIf(nameof(enableAnimation))]
        [ListDrawerSettings(ShowFoldout = false)]
        public List<SpriteLayer> layers = new();

        // [LabelText("动画列表")]
        // [HorizontalGroup("Rule")]
        // [ShowIf(nameof(enableAnimation))]
        // public SpriteListSetter animationSprites = new();

        [HideLabel]
        [VerticalGroup("Rule/Limit")]
        [HorizontalGroup("Rule/Limit/Upper")]
        public Limit upperLeft = new();

        [HideLabel]
        [HorizontalGroup("Rule/Limit/Upper")]
        public Limit upper = new();

        [HideLabel]
        [HorizontalGroup("Rule/Limit/Upper")]
        public Limit upperRight = new();

        [HideLabel]
        [HorizontalGroup("Rule/Limit/Center")]
        public Limit left = new();

        [HideLabel]
        [HorizontalGroup("Rule/Limit/Center")]
        [ShowInInspector, DisplayAsString]
        private string center = "";

        [HideLabel]
        [HorizontalGroup("Rule/Limit/Center")]
        public Limit right = new();

        [HideLabel]
        [HorizontalGroup("Rule/Limit/Lower")]
        public Limit lowerLeft = new();

        [HideLabel]
        [HorizontalGroup("Rule/Limit/Lower")]
        public Limit lower = new();

        [HideLabel]
        [HorizontalGroup("Rule/Limit/Lower")]
        public Limit lowerRight = new();

        [LabelText("动画间隔")]
        [ShowIf(nameof(enableAnimation))]
        [MinValue(0)]
        public float gap = 0.2f;

        [LabelText("开始时自动播放动画")]
        [ShowIf(nameof(enableAnimation))]
        public bool autoPlayOnStart = true;

        #region Init & Check

        public override void CheckSettings()
        {
            base.CheckSettings();

            foreach (var layer in layers)
            {
                layer.CheckSettings();
            }
        }

        #endregion

        public IList<Limit> GetAllLimits()
        {
            return new List<Limit>()
            {
                upperLeft, upper, upperRight,
                left, right,
                lowerLeft, lower, lowerRight
            };
        }

        #region Limits

        public bool HasSameLimits(Rule otherRule)
        {
            foreach (var (limit, otherLimit) in GetAllLimits()
                         .Zip(otherRule.GetAllLimits()))
            {
                if (limit.Equals(otherLimit) == false)
                {
                    return false;
                }
            }

            return true;
        }

        public bool HasSubLimitsOf(Rule otherRule)
        {
            foreach (var (limit, otherLimit) in GetAllLimits()
                         .Zip(otherRule.GetAllLimits()))
            {
                if (otherLimit.limitType == LimitType.None)
                {
                    continue;
                }

                if (limit.Equals(otherLimit) == false)
                {
                    return false;
                }
            }

            return true;
        }

        #endregion

        #region Preset

        public static Rule UpperLeftOnly(LimitType upperLowerLeftRightType,
            LimitType upperLeft)
        {
            return new()
            {
                left = upperLowerLeftRightType,
                right = upperLowerLeftRightType,
                upper = upperLowerLeftRightType,
                lower = upperLowerLeftRightType,
                upperLeft = upperLeft
            };
        }

        public static Rule UpperRightOnly(LimitType upperLowerLeftRightType,
            LimitType upperRight)
        {
            return new()
            {
                left = upperLowerLeftRightType,
                right = upperLowerLeftRightType,
                upper = upperLowerLeftRightType,
                lower = upperLowerLeftRightType,
                upperRight = upperRight
            };
        }

        public static Rule LowerLeftOnly(LimitType upperLowerLeftRightType,
            LimitType lowerLeft)
        {
            return new()
            {
                left = upperLowerLeftRightType,
                right = upperLowerLeftRightType,
                upper = upperLowerLeftRightType,
                lower = upperLowerLeftRightType,
                lowerLeft = lowerLeft
            };
        }

        public static Rule LowerRightOnly(LimitType upperLowerLeftRightType,
            LimitType lowerRight)
        {
            return new()
            {
                left = upperLowerLeftRightType,
                right = upperLowerLeftRightType,
                upper = upperLowerLeftRightType,
                lower = upperLowerLeftRightType,
                lowerRight = lowerRight
            };
        }

        #endregion

        #region GetClone

        public Rule GetClone()
        {
            var result = new Rule()
            {
                enable = enable,
                enableAnimation = enableAnimation,
                // animationSprites = animationSprites,
                gap = gap,
                autoPlayOnStart = autoPlayOnStart,
                upperLeft = upperLeft,
                upper = upper,
                upperRight = upperRight,
                left = left,
                right = right,
                lowerLeft = lowerLeft,
                lower = lower,
                lowerRight = lowerRight,
            };

            foreach (var layer in layers)
            {
                result.layers.Add(new()
                {
                    layer = layer.layer,
                    sprite = layer.sprite
                });
            }

            return result;
        }

        #endregion
    }
}