using System;
using Newtonsoft.Json;
using System.Runtime.CompilerServices;
using VMFramework.Core;
using VMFramework.OdinExtensions;

namespace VMFramework.Configuration
{
    [JsonObject(MemberSerialization.OptIn)]
    [Serializable]
    public class RangeIntegerConfig : KCubeIntegerConfig<int>, IRangeSliderValueProvider
    {
        protected override string pointName => "值";

        protected override string sizeName => "长度";

        public override int size => max - min + 1;

        public override int pivot => (min + max) / 2;

        #region Constructor

        public RangeIntegerConfig()
        {
            min = 0;
            max = 0;
        }

        public RangeIntegerConfig(int length)
        {
            min = 0;
            max = length - 1;
            max = max.ClampMin(-1);
        }

        public RangeIntegerConfig(int min, int max)
        {
            this.min = min;
            this.max = max;
        }

        #endregion

        #region KCube

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Contains(int pos) => pos >= min && pos <= max;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetRelativePos(int pos) => pos - min;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int ClampMin(int pos) => pos.ClampMin(min);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int ClampMax(int pos) => pos.ClampMax(max);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetPointsCount() => size;

        #endregion

        #region Cloneable

        public override object Clone()
        {
            return new RangeIntegerConfig(min, max);
        }

        #endregion

        #region Range Slider Value Provider

        float IRangeSliderValueProvider.min
        {
            get => min;
            set => min = value.Round();
        }

        float IRangeSliderValueProvider.max
        {
            get => max;
            set => max = value.Round();
        }

        #endregion
    }
}
