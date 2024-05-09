using Newtonsoft.Json;
using Sirenix.OdinInspector;
using VMFramework.Core;
using VMFramework.OdinExtensions;

namespace VMFramework.Configuration
{
    [JsonObject(MemberSerialization.OptIn)]
    public sealed class FloatSetter : NumberOrVectorChooser<float, RangeFloatConfig>,
        IMinimumValueProvider, IMaximumValueProvider
    {
        protected override string valueName => "浮点数";

        [LabelText("小数点后显示几位")]
        [MinValue(0)]
        public int decimalPlaces = 1;

        #region GUI

        [Button("添加定步长的范围浮点数", Style = ButtonStyle.Box)]
        [ShowIf(nameof(isCircularSelect))]
        private void SetCircularItemsToRange([LabelText("起始值")] float start = 0,
            [LabelText("结束值")] float end = 5, 
            [LabelText("步长"), MinValue(0.01)] float step = 1)
        {
            circularSelectItems.Clear();

            foreach (var point in start.GetSteppedPoints(end, step))
            {
                circularSelectItems.Add(new CircularSelectItem() {times = 1, value = point});
            }
        }

        #endregion

        #region To String

        protected override string ValueToString(float value)
        { 
            return value.ToString(decimalPlaces);
        }

        #endregion

        #region Minimum & Maximum Value Provider

        void IMaximumValueProvider.ClampByMaximum(double maximum)
        {
            SetMaxValue(maximum.F());
        }

        void IMinimumValueProvider.ClampByMinimum(double minimum)
        {
            SetMinValue(minimum.F());
        }

        #endregion

        #region Operator

        public static implicit operator FloatSetter(double fixedFloat)
        {
            return new FloatSetter()
            {
                isRandomValue = false,
                value = fixedFloat.F()
            };
        }

        #endregion
    }
}

