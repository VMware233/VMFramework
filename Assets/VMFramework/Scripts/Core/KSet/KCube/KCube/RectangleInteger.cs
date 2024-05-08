using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace VMFramework.Core
{
    public struct RectangleInteger : IKCubeInteger<Vector2Int>, IEquatable<RectangleInteger>, 
        IFormattable, IEnumerable<Vector2Int>
    {
        /// <summary>
        /// 创建一个[0, 0]x[0, 0]的整数矩形
        /// </summary>
        public static RectangleInteger zero { get; } =
            new(Vector2Int.zero, Vector2Int.zero);

        /// <summary>
        /// 创建一个[1, 1]x[1, 1]的整数矩形
        /// </summary>
        public static RectangleInteger one { get; } =
            new(Vector2Int.one, Vector2Int.one);

        /// <summary>
        /// 创建一个[0, 1]x[0, 1]的整数矩形
        /// </summary>
        public static RectangleInteger unit { get; } =
            new(Vector2Int.zero, Vector2Int.one);

        public readonly Vector2Int size => max - min + Vector2Int.one;

        public readonly Vector2Int pivot => (max + min) / 2;

        public readonly Vector2Int min, max;

        public readonly RangeInteger xRange => new(min.x, max.x);

        public readonly RangeInteger yRange => new(min.y, max.y);

        #region Constructor

        public RectangleInteger(RangeInteger xRange, RangeInteger yRange)
        {
            min = new Vector2Int(xRange.min, yRange.min);
            max = new Vector2Int(xRange.max, yRange.max);
        }

        public RectangleInteger(int xMin, int yMin, int xMax, int yMax)
        {
            min = new Vector2Int(xMin, yMin);
            max = new Vector2Int(xMax, yMax);
        }

        public RectangleInteger(Vector2Int min, Vector2Int max)
        {
            this.min = min;
            this.max = max;
        }

        public RectangleInteger(int width, int length)
        {
            min = Vector2Int.zero;
            max = new Vector2Int(width - 1, length - 1);
        }

        public RectangleInteger(Vector2Int size)
        {
            min = Vector2Int.zero;
            max = new(size.x - 1, size.y - 1);
        }

        public RectangleInteger(RectangleInteger source)
        {
            min = source.min;
            max = source.max;
        }

        public RectangleInteger(IKCube<Vector2Int> config)
        {
            if (config is null)
            {
                min = Vector2Int.zero;
                max = Vector2Int.zero;
                return;
            }
            min = config.min;
            max = config.max;
        }

        #endregion

        #region IKCube

        readonly Vector2Int IKCube<Vector2Int>.min
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => min;
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            init => min = value;
        }

        readonly Vector2Int IKCube<Vector2Int>.max
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => max;
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            init => max = value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly bool Contains(Vector2Int pos) => pos.x >= min.x && pos.x <= max.x &&
                                                pos.y >= min.y && pos.y <= max.y;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly Vector2Int GetRelativePos(Vector2Int pos) => pos - min;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly Vector2Int ClampMin(Vector2Int pos) => pos.ClampMin(min);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly Vector2Int ClampMax(Vector2Int pos) => pos.ClampMax(max);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly int GetPointsCount() => size.Products();

        #endregion

        #region Geometry Extension

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly bool IsOnBoundary(Vector2Int pos, out FourTypesDirection2D directions)
        {
            return pos.IsOnBoundary(min, max, out directions);
        }

        #endregion

        #region Operator

        public static RectangleInteger operator
            +(RectangleInteger a, Vector2Int b) =>
            new(a.min + b, a.max + b);

        public static RectangleInteger operator
            -(RectangleInteger a, Vector2Int b) =>
            new(a.min - b, a.max - b);

        public static RectangleInteger operator *(RectangleInteger a, int b)
        {
            if (b >= 0)
            {
                return new RectangleInteger(a.min * b, a.max * b);
            }

            return new RectangleInteger(a.max * b, a.min * b);
        }

        public static RectangleInteger operator
            *(RectangleInteger a, Vector2Int b)
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

            return new RectangleInteger(xMin, yMin, xMax, yMax);
        }

        public static RectangleInteger operator /(RectangleInteger a, int b)
        {
            if (b >= 0)
            {
                return new(a.min / b, a.max / b);
            }

            return new(a.max / b, a.min / b);
        }

        public static RectangleInteger operator
            /(RectangleInteger a, Vector2Int b)
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

            return new RectangleInteger(xMin, yMin, xMax, yMax);
        }

        public static RectangleInteger operator -(RectangleInteger a) =>
            new(-a.max, -a.min);

        public static CubeInteger operator *(RectangleInteger a, RangeInteger b) =>
            new(a, b);

        public static CubeInteger operator *(RangeInteger a, RectangleInteger b) =>
            new(a, b);

        public static bool operator ==(RectangleInteger a, RectangleInteger b) =>
            a.Equals(b);

        public static bool operator !=(RectangleInteger a, RectangleInteger b) =>
            !a.Equals(b);

        #endregion

        #region Equatable

        public readonly bool Equals(RectangleInteger other)
        {
            return min.Equals(other.min) && max.Equals(other.max);
        }

        public readonly override bool Equals(object obj)
        {
            return obj is RectangleInteger other && Equals(other);
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

        public IEnumerator<Vector2Int> GetEnumerator()
        {
            return this.GetAllPoints().GetEnumerator();
        }

        #endregion

        #region To String

        public readonly override string ToString() => $"[{min}, {max}]";

        public readonly string ToString(string format, IFormatProvider formatProvider) =>
            $"[{min.ToString(format, formatProvider)},{max.ToString(format, formatProvider)}]";

        #endregion
    }
}
