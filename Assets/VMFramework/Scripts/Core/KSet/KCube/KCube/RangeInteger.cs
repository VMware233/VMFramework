using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace VMFramework.Core
{
    public partial struct RangeInteger : IKCubeInteger<int>, IEquatable<RangeInteger>, IFormattable, 
        IEnumerable<int>
    {
        public static RangeInteger zero { get; } = new(0, 0);

        public static RangeInteger one { get; } = new(1, 1);

        public static RangeInteger unit { get; } = new(0, 1);

        public readonly int size => max - min + 1;

        public readonly int pivot => (max + min) / 2;

        public readonly int min, max;

        #region Constructor

        public RangeInteger(int min, int max)
        {
            this.min = min;
            this.max = max;
        }

        public RangeInteger(int length)
        {
            min = 0;
            max = length - 1;
        }

        public RangeInteger(RangeInteger source)
        {
            min = source.min;
            max = source.max;
        }

        public RangeInteger(IKCube<int> config)
        {
            if (config is null)
            {
                min = 0;
                max = 0;
                return;
            }

            min = config.min;
            max = config.max;
        }

        public RangeInteger(Vector2Int range)
        {
            min = range.x;
            max = range.y;
        }

        #endregion

        #region IKCube

        readonly int IKCube<int>.min
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => min;
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            init => min = value;
        }

        readonly int IKCube<int>.max
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => max;
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            init => max = value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly bool Contains(int pos) => pos >= min && pos <= max;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly int GetRelativePos(int pos) => pos - min;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly int ClampMin(int pos) => pos.ClampMin(min);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly int ClampMax(int pos) => pos.ClampMax(max);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly int GetPointsCount() => size;

        #endregion

        #region Operator

        public static RangeInteger operator +(RangeInteger a, int b) =>
            new(a.min + b, a.max + b);

        public static RangeInteger operator -(RangeInteger a, int b) =>
            new(a.min - b, a.max - b);

        public static RangeInteger operator *(RangeInteger a, int b)
        {
            if (b >= 0)
            {
                return new(a.min * b, a.max * b);
            }

            return new(a.max * b, a.min * b);
        }

        public static RangeInteger operator /(RangeInteger a, int b)
        {
            if (b >= 0)
            {
                return new(a.min / b, a.max / b);
            }

            return new(a.max / b, a.min / b);
        }

        public static RangeInteger operator -(RangeInteger a) => new(-a.max, -a.min);

        public static RectangleInteger operator *(RangeInteger a, RangeInteger b) =>
            new(a, b);

        public static bool operator ==(RangeInteger a, RangeInteger b) =>
            a.Equals(b);

        public static bool operator !=(RangeInteger a, RangeInteger b) =>
            !a.Equals(b);

        public static implicit operator RangeInteger(Vector2Int range) => new(range);

        #endregion

        #region Equatable

        public readonly bool Equals(RangeInteger other)
        {
            return min.Equals(other.min) && max.Equals(other.max);
        }

        public readonly override bool Equals(object obj)
        {
            return obj is RangeInteger other && Equals(other);
        }

        public readonly override int GetHashCode()
        {
            return HashCode.Combine(min, max);
        }

        #endregion

        #region Enumerator

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<int> GetEnumerator()
        {
            return this.GetAllPoints().GetEnumerator();
        }

        #endregion

        #region To String

        public readonly override string ToString() => $"[{min}, {max}]";

        public readonly string ToString(string format, IFormatProvider formatProvider) =>
            $"[{min.ToString(format, formatProvider)}, {max.ToString(format, formatProvider)}]";

        #endregion
    }
}
