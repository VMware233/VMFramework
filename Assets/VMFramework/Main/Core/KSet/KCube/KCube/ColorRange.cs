using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace VMFramework.Core
{
    public struct ColorRange : IKCubeFloat<Color>, IEquatable<ColorRange>, IFormattable
    {
        public static Color zeroColor { get; } = new(0, 0, 0, 0);

        public static ColorRange zero { get; } =
            new(zeroColor, zeroColor);

        public static ColorRange one { get; } =
            new(Color.white, Color.white);

        public static ColorRange unit { get; } =
            new(zeroColor, Color.white);

        public readonly Color size => max - min;

        public readonly Color pivot => (max + min) / 2;

        public readonly Color extents => (max - min) / 2;

        public readonly Color min, max;

        public readonly RangeFloat rRange => new(min.r, max.r);

        public readonly RangeFloat gRange => new(min.g, max.g);

        public readonly RangeFloat bRange => new(min.b, max.b);

        public readonly RangeFloat aRange => new(min.a, max.a);

        #region Constructor

        public ColorRange(RangeFloat xRange, RangeFloat yRange, RangeFloat zRange)
        {
            min = new Color(xRange.min, yRange.min, zRange.min);
            max = new Color(xRange.max, yRange.max, zRange.max);
        }

        public ColorRange(float xMin, float yMin, float zMin, float xMax, float yMax,
            float zMax)
        {
            min = new Color(xMin, yMin, zMin);
            max = new Color(xMax, yMax, zMax);
        }

        public ColorRange(Color min, Color max)
        {
            this.min = min;
            this.max = max;
        }

        public ColorRange(float width, float length, float height)
        {
            min = zeroColor;
            max = new Color(width, length, height);
        }

        public ColorRange(Color size)
        {
            min = zeroColor;
            max = size;
        }

        public ColorRange(ColorRange source)
        {
            min = source.min;
            max = source.max;
        }

        public ColorRange(IKCube<Color> config)
        {
            if (config is null)
            {
                min = zeroColor;
                max = zeroColor;
                return;
            }
            min = config.min;
            max = config.max;
        }

        #endregion

        #region IKCube

        readonly Color IKCube<Color>.min
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => min;
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            init => min = value;
        }

        readonly Color IKCube<Color>.max
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => max;
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            init => max = value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly bool Contains(Color pos) => pos.r >= min.r && pos.r <= max.r &&
                                           pos.g >= min.g && pos.g <= max.g &&
                                           pos.b >= min.b && pos.b <= max.b &&
                                           pos.a >= min.a && pos.a <= max.a;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly Color GetRelativePos(Color pos) => pos - min;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly Color ClampMin(Color pos) => pos.ClampMin(min);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly Color ClampMax(Color pos) => pos.ClampMax(max);

        #endregion

        #region Operator

        public static ColorRange operator +(ColorRange a, Color b) =>
            new(a.min + b, a.max + b);

        public static ColorRange operator -(ColorRange a, Color b) =>
            new(a.min - b, a.max - b);

        public static ColorRange operator *(ColorRange a, Color b) =>
            new(a.min.Multiply(b), a.max.Multiply(b));

        public static ColorRange operator *(ColorRange a, float b) =>
            new(a.min * b, a.max * b);

        public static ColorRange operator /(ColorRange a, Color b) =>
            new(a.min.Divide(b), a.max.Divide(b));

        public static ColorRange operator /(ColorRange a, float b) =>
            new(a.min / b, a.max / b);

        public static bool operator ==(ColorRange a, ColorRange b) =>
            a.Equals(b);

        public static bool operator !=(ColorRange a, ColorRange b) =>
            !a.Equals(b);

        #endregion

        #region Equatable

        public readonly bool Equals(ColorRange other)
        {
            return min.Equals(other.min) && max.Equals(other.max);
        }

        public readonly override bool Equals(object obj)
        {
            return obj is ColorRange other && Equals(other);
        }

        public readonly override int GetHashCode()
        {
            return HashCode.Combine(min, max);
        }

        #endregion

        #region To String

        public readonly override string ToString() => $"[{min}, {max}]";

        public readonly string ToString(string format, IFormatProvider formatProvider) =>
            $"[{min.ToString(format, formatProvider)},{max.ToString(format, formatProvider)}]";

        #endregion
    }
}
