using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace VMFramework.Core
{
    public struct RectangleFloat : IKCubeFloat<Vector2>, IEquatable<RectangleFloat>, IFormattable
    {
        public static RectangleFloat zero { get; } =
            new(Vector2.zero, Vector2.zero);

        public static RectangleFloat one { get; } =
            new(Vector2.one, Vector2.one);

        public static RectangleFloat unit { get; } =
            new(Vector2.zero, Vector2.one);

        public readonly Vector2 size => max - min;

        public readonly Vector2 pivot => (max + min) / 2;

        public readonly Vector2 extents => (max - min) / 2;

        public readonly Vector2 min, max;

        public readonly RangeFloat xRange => new(min.x, max.x);

        public readonly RangeFloat yRange => new(min.y, max.y);

        public readonly Vector2 leftTop => new(min.x, max.y);

        public readonly Vector2 rightBottom => new(max.x, min.y);

        #region Constructor

        public RectangleFloat(RangeFloat xRange, RangeFloat yRange)
        {
            min = new Vector2(xRange.min, yRange.min);
            max = new Vector2(xRange.max, yRange.max);
        }

        public RectangleFloat(float xMin, float yMin, float xMax, float yMax)
        {
            min = new Vector2(xMin, yMin);
            max = new Vector2(xMax, yMax);
        }

        public RectangleFloat(Vector2 min, Vector2 max)
        {
            this.min = min;
            this.max = max;
        }

        public RectangleFloat(float width, float length)
        {
            min = Vector2.zero;
            max = new Vector2(width, length);
        }

        public RectangleFloat(Vector2 size)
        {
            min = Vector2.zero;
            max = size;
        }

        public RectangleFloat(RectangleFloat source)
        {
            min = source.min;
            max = source.max;
        }

        public RectangleFloat(IKCube<Vector2> config)
        {
            if (config is null)
            {
                min = Vector2.zero;
                max = Vector2.zero;
                return;
            }
            min = config.min;
            max = config.max;
        }

        public RectangleFloat(Rect rect)
        {
            min = rect.min;
            max = rect.max;
        }

        #endregion

        #region IKCube

        readonly Vector2 IKCube<Vector2>.min
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => min;
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            init => min = value;
        }

        readonly Vector2 IKCube<Vector2>.max
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => max;
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            init => max = value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly bool Contains(Vector2 pos) => pos.x >= min.x && pos.x <= max.x &&
                                                      pos.y >= min.y && pos.y <= max.y;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly Vector2 GetRelativePos(Vector2 pos) => pos - min;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly Vector2 ClampMin(Vector2 pos) => pos.ClampMin(min);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly Vector2 ClampMax(Vector2 pos) => pos.ClampMax(max);

        #endregion

        #region Operator

        public static RectangleFloat operator +(RectangleFloat a, Vector2 b) =>
            new(a.min + b, a.max + b);

        public static RectangleFloat operator -(RectangleFloat a, Vector2 b) =>
            new(a.min - b, a.max - b);

        public static RectangleFloat operator *(RectangleFloat a, Vector2 b)
        {
            var xMin = a.min.x;
            var xMax = a.max.x;

            if (b.x >= 0)
            {
                xMin *= b.x;
                xMax *= b.x;
            }
            else
            {
                (xMin, xMax) = (xMax * b.x, xMin * b.x);
            }

            var yMin = a.min.y;
            var yMax = a.max.y;

            if (b.y >= 0)
            {
                yMin *= b.y;
                yMax *= b.y;
            }
            else
            {
                (yMin, yMax) = (yMax * b.y, yMin * b.y);
            }

            return new RectangleFloat(xMin, yMin, xMax, yMax);
        }

        public static RectangleFloat operator *(RectangleFloat a, float b)
        {
            if (b >= 0)
            {
                return new(a.min * b, a.max * b);
            }

            return new(a.max * b, a.min * b);
        }

        public static RectangleFloat operator /(RectangleFloat a, Vector2 b)
        {
            var xMin = a.min.x;
            var xMax = a.max.x;

            if (b.x >= 0)
            {
                xMin /= b.x;
                xMax /= b.x;
            }
            else
            {
                (xMin, xMax) = (xMax / b.x, xMin / b.x);
            }

            var yMin = a.min.y;
            var yMax = a.max.y;

            if (b.y >= 0)
            {
                yMin /= b.y;
                yMax /= b.y;
            }
            else
            {
                (yMin, yMax) = (yMax / b.y, yMin / b.y);
            }

            return new RectangleFloat(xMin, yMin, xMax, yMax);
        }

        public static RectangleFloat operator /(RectangleFloat a, float b)
        {
            if (b >= 0)
            {
                return new(a.min / b, a.max / b);
            }

            return new(a.max / b, a.min / b);
        }

        public static RectangleFloat operator -(RectangleFloat a) =>
            new(-a.max, -a.min);

        public static CubeFloat operator *(RectangleFloat a, RangeFloat b) =>
            new(a, b);

        public static CubeFloat operator *(RangeFloat a, RectangleFloat b) =>
            new(a, b);

        public static bool operator ==(RectangleFloat a, RectangleFloat b) =>
            a.Equals(b);

        public static bool operator !=(RectangleFloat a, RectangleFloat b) =>
            !a.Equals(b);

        #endregion

        #region To Rect

        public readonly Rect ToRect() => new Rect(min, size);

        public static implicit operator Rect(RectangleFloat r) => r.ToRect();

        #endregion

        #region Equatable

        public readonly bool Equals(RectangleFloat other)
        {
            return min.Equals(other.min) && max.Equals(other.max);
        }

        public readonly override bool Equals(object obj)
        {
            return obj is RectangleFloat other && Equals(other);
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
