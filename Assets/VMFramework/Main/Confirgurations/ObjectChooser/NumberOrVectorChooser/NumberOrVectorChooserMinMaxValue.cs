using VMFramework.Core;
using VMFramework.Core.Generic;

namespace VMFramework.Configuration
{
    public abstract partial class NumberOrVectorChooser<T, TRange>
    {
        #region MaxValue

        protected virtual T GetMaxRandomValue()
        {
            T maxValue = rangeValue.max;

            if (weightedSelectItems.Count > 0)
            {
                var maxWeightedSelectValue = weightedSelectItems.MaxOrDefault(item => item.value);

                maxValue = maxValue.Max(maxWeightedSelectValue);
            }

            if (circularSelectItems.Count > 0)
            {
                var maxCircularValue = circularSelectItems.MaxOrDefault(item => item.value);
                maxValue = maxValue.Max(maxCircularValue);
            }

            return maxValue;
        }

        protected virtual T GetMaxFixedValue()
        {
            return value;
        }

        public T GetMaxValue()
        {
            return GetMaxFixedValue().Max(GetMaxRandomValue());
        }

        protected virtual void SetMaxRandomValue(T MaxValue)
        {
            rangeValue.max = rangeValue.max.ClampMax(MaxValue);
            rangeValue.max = rangeValue.max.ClampMax(MaxValue);
            weightedSelectItems.Examine(item => item.value = item.value.ClampMax(MaxValue));
            circularSelectItems.Examine(item => item.value = item.value.ClampMax(MaxValue));
        }

        protected virtual void SetMaxFixedValue(T MaxValue)
        {
            value = value.ClampMax(MaxValue);
        }

        public void SetMaxValue(T maxValue)
        {
            SetMaxRandomValue(maxValue);
            SetMaxFixedValue(maxValue);
        }

        #endregion

        #region MinValue

        protected virtual T GetMinRandomValue()
        {
            T minValue = rangeValue.min;

            if (weightedSelectItems.Count > 0)
            {
                var minWeightedSelectValue = weightedSelectItems.MinOrDefault(item => item.value);
                minValue = minValue.Min(minWeightedSelectValue);
            }

            if (circularSelectItems.Count > 0)
            {
                var minCircularValue = circularSelectItems.MinOrDefault(item => item.value);
                minValue = minValue.Min(minCircularValue);
            }

            return minValue;
        }

        protected virtual T GetMinFixedValue()
        {
            return value;
        }

        public T GetMinValue()
        {
            return GetMinFixedValue().Min(GetMinRandomValue());
        }

        protected virtual void SetMinRandomValue(T minValue)
        {
            rangeValue.max = rangeValue.max.ClampMin(minValue);
            rangeValue.min = rangeValue.min.ClampMin(minValue);
            weightedSelectItems.Examine(item => item.value = item.value.ClampMin(minValue));
            circularSelectItems.Examine(item => item.value = item.value.ClampMin(minValue));
        }

        protected virtual void SetMinFixedValue(T minValue)
        {
            value = value.ClampMin(minValue);
        }

        public void SetMinValue(T minValue)
        {
            SetMinRandomValue(minValue);
            SetMinFixedValue(minValue);
        }

        #endregion
    }
}