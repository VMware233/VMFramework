using System;
using System.Runtime.CompilerServices;

namespace VMFramework.Core
{
    public readonly partial struct RangeChooser<TNumber> : IChooser<TNumber> where TNumber : struct, IEquatable<TNumber>
    {
        public readonly IKCube<TNumber> range;

        public RangeChooser(IKCube<TNumber> range)
        {
            this.range = range;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TNumber GetValue()
        {
            return range.GetRandomPoint();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ResetChooser()
        {
            
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        object IChooser.GetValue()
        {
            return range.GetRandomPoint();
        }
    }
}