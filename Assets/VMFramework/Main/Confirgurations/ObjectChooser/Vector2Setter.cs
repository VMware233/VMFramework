using System.Linq;
using VMFramework.Core;
using Sirenix.OdinInspector;
using UnityEngine;

namespace VMFramework.Configuration
{
    public sealed class Vector2Setter : NumberOrVectorChooser<Vector2, RectangleFloatConfig>
    {
        private const string WEIGHTED_SELECT_TOOLS = "WeightedSelectTools";

        protected override string valueName => "向量";

        [LabelText("小数点后显示几位")]
        [MinValue(0)]
        [OnValueChanged("PreviewValue")]
        public int decimalPlaces = 1;

        #region GUI

        [Button("X轴对称")]
        [ShowIf(nameof(isWeightedSelect))]
        [ButtonGroup(WEIGHTED_SELECT_TOOLS)]
        private void AddXAxisymmetric()
        {
            foreach (var item in weightedSelectItems.ToArray())
            {
                var symmetric = item.value.XAxisymmetric();

                if (weightedSelectItems.All(item => item.value != symmetric))
                {
                    weightedSelectItems.Add(new()
                    {
                        value = symmetric,
                        ratio = 1
                    });
                }
            }

            OnWeightedSelectItemsChanged();
        }

        [Button("Y轴对称")]
        [ShowIf(nameof(isWeightedSelect))]
        [ButtonGroup(WEIGHTED_SELECT_TOOLS)]
        private void AddYAxisymmetric()
        {
            foreach (var item in weightedSelectItems.ToArray())
            {
                var symmetric = item.value.YAxisymmetric();

                if (weightedSelectItems.All(item => item.value != symmetric))
                {
                    weightedSelectItems.Add(new()
                    {
                        value = symmetric,
                        ratio = 1
                    });
                }
            }

            OnWeightedSelectItemsChanged();
        }

        [Button("原点对称")]
        [ShowIf(nameof(isWeightedSelect))]
        [ButtonGroup(WEIGHTED_SELECT_TOOLS)]
        private void AddOriginSymmetric()
        {
            foreach (var item in weightedSelectItems.ToArray())
            {
                var symmetric = item.value.PointSymmetric();

                if (weightedSelectItems.All(item => item.value != symmetric))
                {
                    weightedSelectItems.Add(new()
                    {
                        value = symmetric,
                        ratio = 1
                    });
                }
            }

            OnWeightedSelectItemsChanged();
        }

        #endregion

        #region To String

        protected override string ValueToString(Vector2 value)
        {
            return value.ToString(decimalPlaces);
        }

        #endregion

        #region Operator

        public static implicit operator Vector2Setter(Vector2 fixedVector)
        {
            return new()
            {
                isRandomValue = false,
                value = fixedVector
            };
        }

        #endregion
    }
}

