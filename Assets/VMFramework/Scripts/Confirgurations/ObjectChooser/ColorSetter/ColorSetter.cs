using System;
using System.Linq;
using VMFramework.Core;
using Newtonsoft.Json;
using Sirenix.OdinInspector;
using UnityEngine;

namespace VMFramework.Configuration
{

    [JsonObject(MemberSerialization.OptIn)]
    public sealed partial class ColorSetter : NumberOrVectorChooser<Color, ColorRangeConfig>
    {
        protected override string valueName => "颜色";

        private float alphaThreshold => 0.2f;

        [LabelText("颜色显示格式")]
        [EnumToggleButtons]
        public ColorStringFormat colorStringFormat;

        #region GUI

        #region SetAlpha

        [Button("所有Alpha值置1")]
        [ShowIf(nameof(RequireAlphaSetTo1))]
        private void SetAlphaTo1()
        {
            if (isRandomValue)
            {
                switch (randomType)
                {
                    case WEIGHTED_SELECT:
                        weightedSelectItems.Examine(item => item.value.ChangeAlpha(1f));
                        break;

                    case CIRCULAR_SELECT:
                        circularSelectItems.Examine(item => item.value.ChangeAlpha(1f));
                        break;

                    case RANGE_SELECT:
                        rangeValue.min.a = 1f;
                        rangeValue.max.a = 1f;
                        break;
                }
            }
            else
            {
                switch (fixedType)
                {
                    case SINGLE_VALUE:
                        value.a = 1;
                        break;
                }
            }
            
        }

        private bool RequireAlphaSetTo1()
        {
            if (isRandomValue)
            {
                return randomType switch
                {
                    WEIGHTED_SELECT => weightedSelectItems.Any(item => item.value.a < 1 - alphaThreshold),
                    CIRCULAR_SELECT => circularSelectItems.Any(item => item.value.a < 1 - alphaThreshold),
                    RANGE_SELECT => rangeValue.min.a < 1 - alphaThreshold || rangeValue.max.a < 1 - alphaThreshold,
                    _ => throw new ArgumentException()
                };
            }

            return fixedType switch
            {
                SINGLE_VALUE => value.a < 1 - alphaThreshold,
                _ => throw new ArgumentException()
            };
        }

        private bool ContainsColorWithLowAlpha()
        {
            if (isRandomValue)
            {
                return randomType switch
                {
                    WEIGHTED_SELECT => weightedSelectItems.Any(item => item.value.a < alphaThreshold),
                    CIRCULAR_SELECT => circularSelectItems.Any(item => item.value.a < alphaThreshold),
                    RANGE_SELECT => rangeValue.max.a < alphaThreshold || rangeValue.min.a < alphaThreshold,
                    _ => throw new ArgumentException()
                };
            }

            return fixedType switch
            {
                SINGLE_VALUE => value.a < alphaThreshold,
                _ => throw new ArgumentException()
            };
        }

        #endregion

        #region SetRandomColor

        [Button(@"@""随机"" + valueName")]
        private void SetRandomColorGUI()
        {
            if (isRandomValue)
            {
                switch (randomType)
                {
                    case WEIGHTED_SELECT:
                        weightedSelectItems.Examine(item => item.value = GenerateRandomColorGUI());
                        break;

                    case CIRCULAR_SELECT:
                        circularSelectItems.Examine(item => item.value = GenerateRandomColorGUI());
                        break;

                    case RANGE_SELECT:
                        rangeValue.min = GenerateRandomColorGUI();
                        rangeValue.max = GenerateRandomColorGUI();
                        break;

                    default:
                        throw new ArgumentException();
                }
            }
            else
            {
                switch (fixedType)
                {
                    case SINGLE_VALUE:
                        value = GenerateRandomColorGUI();
                        break;

                    default:
                        throw new ArgumentException();
                }
            }
        }

        private Color GenerateRandomColorGUI()
        {
            return Color.black.RandomRange(Color.white);
        }

        #endregion

        #endregion

        #region To String

        protected override string ValueToString(Color value)
        {
            return value.ToString(colorStringFormat);
        }

        #endregion
    }
}

