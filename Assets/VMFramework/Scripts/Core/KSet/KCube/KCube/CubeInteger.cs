using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace VMFramework.Core
{
    public struct CubeInteger : IKCubeInteger<Vector3Int>, IEquatable<CubeInteger>, IFormattable,
        IEnumerable<Vector3Int>
    {
        public static CubeInteger zero { get; } =
            new(Vector3Int.zero, Vector3Int.zero);

        public static CubeInteger one { get; } =
            new(Vector3Int.one, Vector3Int.one);

        public static CubeInteger unit { get; } =
            new(Vector3Int.zero, Vector3Int.one);

        public readonly Vector3Int size => max - min + Vector3Int.one;

        public readonly Vector3Int pivot => (max + min) / 2;

        public readonly Vector3Int min, max;

        public readonly RangeInteger xRange => new(min.x, max.x);

        public readonly RangeInteger yRange => new(min.y, max.y);

        public readonly RangeInteger zRange => new(min.z, max.z);

        public readonly RectangleInteger xyRectangle => new(min.x, min.y, max.x, max.y);

        public readonly RectangleInteger xzRectangle => new(min.x, min.z, max.x, max.z);

        public readonly RectangleInteger yzRectangle => new(min.y, min.z, max.y, max.z);

        #region Constructor

        public CubeInteger(RangeInteger xRange, RangeInteger yRange,
            RangeInteger zRange)
        {
            min = new Vector3Int(xRange.min, yRange.min, zRange.min);
            max = new Vector3Int(xRange.max, yRange.max, zRange.max);
        }

        public CubeInteger(RectangleInteger xyRectangle, RangeInteger zRange)
        {
            min = new Vector3Int(xyRectangle.min.x, xyRectangle.min.y, zRange.min);
            max = new Vector3Int(xyRectangle.max.x, xyRectangle.max.y, zRange.max);
        }

        public CubeInteger(RangeInteger xRange, RectangleInteger yzRectangle)
        {
            min = new Vector3Int(xRange.min, yzRectangle.min.x, yzRectangle.min.y);
            max = new Vector3Int(xRange.max, yzRectangle.max.x, yzRectangle.max.y);
        }

        public CubeInteger(int xMin, int yMin, int zMin, int xMax, int yMax,
            int zMax)
        {
            min = new Vector3Int(xMin, yMin, zMin);
            max = new Vector3Int(xMax, yMax, zMax);
        }

        public CubeInteger(Vector3Int min, Vector3Int max)
        {
            this.min = min;
            this.max = max;
        }

        public CubeInteger(int width, int length, int height)
        {
            min = Vector3Int.zero;
            max = new Vector3Int(width - 1, length - 1, height - 1);
        }

        public CubeInteger(Vector3Int size)
        {
            min = Vector3Int.zero;
            max = new(size.x - 1, size.y - 1, size.z - 1);
        }

        public CubeInteger(CubeInteger source)
        {
            min = source.min;
            max = source.max;
        }

        public CubeInteger(IKCube<Vector3Int> config)
        {
            if (config is null)
            {
                min = Vector3Int.zero;
                max = Vector3Int.zero;
                return;
            }

            min = config.min;
            max = config.max;
        }

        #endregion

        #region IKCube

        readonly Vector3Int IKCube<Vector3Int>.min
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => min;
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            init => min = value;
        }

        readonly Vector3Int IKCube<Vector3Int>.max
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => max;
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            init => max = value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly bool Contains(Vector3Int pos) => pos.x >= min.x && pos.x <= max.x &&
                                                pos.y >= min.y && pos.y <= max.y &&
                                                pos.z >= min.z && pos.z <= max.z;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly Vector3Int GetRelativePos(Vector3Int pos) => pos - min;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly Vector3Int ClampMin(Vector3Int pos) => pos.ClampMin(min);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly Vector3Int ClampMax(Vector3Int pos) => pos.ClampMax(max);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly int GetPointsCount() => size.Products();

        #endregion

        #region Operator

        public static CubeInteger operator +(CubeInteger a, Vector3Int b) =>
            new(a.min + b, a.max + b);

        public static CubeInteger operator -(CubeInteger a, Vector3Int b) =>
            new(a.min - b, a.max - b);

        public static CubeInteger operator *(CubeInteger a, Vector3Int b)
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

            var zMin = a.min.z;
            var zMax = a.max.z;

            if (b.z >= 0)
            {
                zMin *= b.z;
                zMax *= b.z;
            }
            else
            {
                (zMin, zMax) = (zMax * b.z, zMin * b.z);
            }

            return new(xMin, yMin, zMin, xMax, yMax, zMax);
        }

        public static CubeInteger operator *(CubeInteger a, int b)
        {
            if (b >= 0)
            {
                return new(a.min * b, a.max * b);
            }

            return new(a.max * b, a.min * b);
        }

        public static CubeInteger operator /(CubeInteger a, Vector3Int b)
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

            var zMin = a.min.z;
            var zMax = a.max.z;

            if (b.z >= 0)
            {
                zMin /= b.z;
                zMax /= b.z;
            }
            else
            {
                (zMin, zMax) = (zMax / b.z, zMin / b.z);
            }

            return new(xMin, yMin, zMin, xMax, yMax, zMax);
        }

        public static CubeInteger operator /(CubeInteger a, int b)
        {
            if (b >= 0)
            {
                return new(a.min / b, a.max / b);
            }

            return new(a.max / b, a.min / b);
        }

        public static CubeInteger operator -(CubeInteger a) =>
            new(-a.max, -a.min);

        public static bool operator ==(CubeInteger a, CubeInteger b) =>
            a.Equals(b);

        public static bool operator !=(CubeInteger a, CubeInteger b) =>
            !a.Equals(b);

        #endregion

        #region Equatable

        public readonly bool Equals(CubeInteger other)
        {
            return min.Equals(other.min) && max.Equals(other.max);
        }

        public readonly override bool Equals(object obj)
        {
            return obj is CubeInteger other && Equals(other);
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

        public IEnumerator<Vector3Int> GetEnumerator()
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
