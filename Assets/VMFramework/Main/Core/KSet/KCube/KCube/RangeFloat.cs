using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using Random = UnityEngine.Random;

namespace VMFramework.Core
{
    public partial struct RangeFloat : IKCubeFloat<float>, IEquatable<RangeFloat>, IFormattable
    {
        public static RangeFloat zero { get; } = new(0, 0);

        public static RangeFloat one { get; } = new(1, 1);

        public static RangeFloat unit { get; } = new(0, 1);

        public readonly float size => max - min;

        public readonly float pivot => (max + min) / 2;

        public readonly float extents => (max - min) / 2;

        public readonly float min, max;

        #region Constructor

        public RangeFloat(float min, float max)
        {
            this.min = min;
            this.max = max;
        }

        public RangeFloat(float length)
        {
            min = 0;
            max = length;
        }

        public RangeFloat(RangeFloat source)
        {
            min = source.min;
            max = source.max;
        }

        public RangeFloat(IKCube<float> config)
        {
            if (config == null)
            {
                min = 0;
                max = 0;
                return;
            }
            min = config.min;
            max = config.max;
        }

        public RangeFloat(Vector2 range)
        {
            min = range.x;
            max = range.y;
        }

        #endregion

        #region IKCube

        readonly float IKCube<float>.min
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => min;
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            init => min = value;
        }

        readonly float IKCube<float>.max
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => max;
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            init => max = value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly bool Contains(float pos) => pos >= min && pos <= max;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly float GetRelativePos(float pos) => pos - min;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly float ClampMin(float pos) => pos.ClampMin(min);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly float ClampMax(float pos) => pos.ClampMax(max);

        #endregion

        #region Operator

        public static RangeFloat operator +(RangeFloat a, float b) =>
            new(a.min + b, a.max + b);

        public static RangeFloat operator -(RangeFloat a, float b) =>
            new(a.min - b, a.max - b);

        public static RangeFloat operator *(RangeFloat a, float b)
        {
            if (b >= 0)
            {
                return new(a.min * b, a.max * b);
            }

            return new(a.max * b, a.min * b);
        }


        public static RangeFloat operator /(RangeFloat a, float b)
        {
            if (b >= 0)
            {
                return new(a.min / b, a.max / b);
            }

            return new(a.max / b, a.min / b);
        }

        public static RangeFloat operator -(RangeFloat a) =>
            new(-a.max, -a.min);

        public static RectangleFloat operator *(RangeFloat a, RangeFloat b) =>
            new(a, b);

        public static bool operator ==(RangeFloat a, RangeFloat b) =>
            a.Equals(b);

        public static bool operator !=(RangeFloat a, RangeFloat b) =>
            !a.Equals(b);

        public static implicit operator RangeFloat(Vector2 range) => new(range);

        #endregion

        #region Equatable

        public readonly bool Equals(RangeFloat other)
        {
            return min.Equals(other.min) && max.Equals(other.max);
        }

        public readonly override bool Equals(object obj)
        {
            return obj is RangeFloat other && Equals(other);
        }

        public readonly override int GetHashCode()
        {
            return HashCode.Combine(min, max);
        }

        #endregion

        #region To String

        public readonly override string ToString() => $"[{min}, {max}]";

        public readonly string ToString(string format, IFormatProvider formatProvider) =>
            $"[{min.ToString(format, formatProvider)}, {max.ToString(format, formatProvider)}]";

        #endregion
    }
}
