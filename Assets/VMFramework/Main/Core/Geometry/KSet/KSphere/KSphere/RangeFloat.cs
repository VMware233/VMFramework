using System.Runtime.CompilerServices;
using UnityEngine;

namespace VMFramework.Core
{
    public partial struct RangeFloat : IKSphere<float, float>
    {
        public float radius
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => extents;
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            init
            {
                var pivot = this.Pivot;
                min = pivot - value;
                max = pivot + value;
            }
        }

        public float center
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => Pivot;
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            init
            {
                var radius = extents;
                min = value - radius;
                max = value + radius;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float Clamp(float pos) => pos.Clamp(min, max);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float GetRandomPointInside() => this.GetRandomPoint();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float GetRandomPointOnSurface()
        {
            var r = Random.value;

            if (r < 0.5f)
            {
                return min;
            }

            return max;
        }

    }
}