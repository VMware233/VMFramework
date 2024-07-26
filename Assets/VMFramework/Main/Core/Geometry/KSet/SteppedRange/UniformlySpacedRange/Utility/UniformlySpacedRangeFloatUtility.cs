using System.Runtime.CompilerServices;

namespace VMFramework.Core
{
    public static class UniformlySpacedRangeFloatUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UniformlySpacedRangeFloat GetUniformlySpacedRange(this float start, float end, int count)
        {
            return new UniformlySpacedRangeFloat(start, end, count);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UniformlySpacedRangeFloat GetUniformlySpacedRange<TMinMaxOwner>(this TMinMaxOwner minMaxOwner,
            int count)
            where TMinMaxOwner : IMinMaxOwner<float>
        {
            return new UniformlySpacedRangeFloat(minMaxOwner.min, minMaxOwner.max, count);
        }
    }
}