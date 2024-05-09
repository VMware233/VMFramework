using Newtonsoft.Json;
using VMFramework.Core;
using VMFramework.OdinExtensions;

namespace VMFramework.Configuration
{
    [JsonObject(MemberSerialization.OptIn)]
    public sealed class IntegerSetter : NumberOrVectorChooser<int, RangeIntegerConfig>, 
        IMinimumValueProvider, IMaximumValueProvider
    {
        protected override string valueName => "整数";

        #region Minimum & Maximum Value Provider

        void IMinimumValueProvider.ClampByMinimum(double minimum)
        {
            SetMinValue(minimum.Ceiling());
        }

        void IMaximumValueProvider.ClampByMaximum(double maximum)
        {
            SetMaxValue(maximum.Floor());
        }

        #endregion

        #region Operator

        public static implicit operator IntegerSetter(int fixedInt)
        {
            return new IntegerSetter()
            {
                isRandomValue = false,
                value = fixedInt
            };
        }

        #endregion
    }
}

