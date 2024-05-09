using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace VMFramework.Core
{
    public struct CubeFloat : IKCubeFloat<Vector3>, IEquatable<CubeFloat>, IFormattable
    {
        public static CubeFloat zero { get; } =
            new(Vector3.zero, Vector3.zero);

        public static CubeFloat one { get; } =
            new(Vector3.one, Vector3.one);

        public static CubeFloat unit { get; } =
            new(Vector3.zero, Vector3.one);

        public readonly Vector3 size => max - min;

        public readonly Vector3 pivot => (max + min) / 2;

        public readonly Vector3 extents => (max - min) / 2;

        public readonly Vector3 min, max;

        public readonly RangeFloat xRange => new(min.x, max.x);

        public readonly RangeFloat yRange => new(min.y, max.y);

        public readonly RangeFloat zRange => new(min.z, max.z);

        public readonly RectangleFloat xyRectangle => new(min.x, min.y, max.x, max.y);

        public readonly RectangleFloat xzRectangle => new(min.x, min.z, max.x, max.z);

        public readonly RectangleFloat yzRectangle => new(min.y, min.z, max.y, max.z);


        #region Constructor

        public CubeFloat(RangeFloat xRange, RangeFloat yRange, RangeFloat zRange)
        {
            min = new Vector3(xRange.min, yRange.min, zRange.min);
            max = new Vector3(xRange.max, yRange.max, zRange.max);
        }

        public CubeFloat(RectangleFloat xyRectangle, RangeFloat zRange)
        {
            min = new Vector3(xyRectangle.min.x, xyRectangle.min.y, zRange.min);
            max = new Vector3(xyRectangle.max.x, xyRectangle.max.y, zRange.max);
        }

        public CubeFloat(RangeFloat xRange, RectangleFloat yzRectangle)
        {
            min = new Vector3(xRange.min, yzRectangle.min.x, yzRectangle.min.y);
            max = new Vector3(xRange.max, yzRectangle.max.x, yzRectangle.max.y);
        }

        public CubeFloat(float xMin, float yMin, float zMin, float xMax, float yMax,
            float zMax)
        {
            min = new Vector3(xMin, yMin, zMin);
            max = new Vector3(xMax, yMax, zMax);
        }

        public CubeFloat(Vector3 min, Vector3 max)
        {
            this.min = min;
            this.max = max;
        }

        public CubeFloat(float width, float length, float height)
        {
            min = Vector3.zero;
            max = new Vector3(width, length, height);
        }

        public CubeFloat(Vector3 size)
        {
            min = Vector3.zero;
            max = size;
        }

        public CubeFloat(CubeFloat source)
        {
            min = source.min;
            max = source.max;
        }

        public CubeFloat(IKCube<Vector3> config)
        {
            if (config is null)
            {
                min = Vector3.zero;
                max = Vector3.zero;
                return;
            }
            min = config.min;
            max = config.max;
        }

        #endregion

        #region IKCube

        readonly Vector3 IKCube<Vector3>.min
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => min;
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            init => min = value;
        }

        readonly Vector3 IKCube<Vector3>.max
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => max;
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            init => max = value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly bool Contains(Vector3 pos) => pos.x >= min.x && pos.x <= max.x &&
                                             pos.y >= min.y && pos.y <= max.y &&
                                             pos.z >= min.z && pos.z <= max.z;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly Vector3 GetRelativePos(Vector3 pos) => pos - min;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly Vector3 ClampMin(Vector3 pos) => pos.ClampMin(min);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly Vector3 ClampMax(Vector3 pos) => pos.ClampMax(max);

        #endregion

        #region Operator

        public static CubeFloat operator +(CubeFloat a, Vector3 b) =>
            new(a.min + b, a.max + b);

        public static CubeFloat operator -(CubeFloat a, Vector3 b) =>
            new(a.min - b, a.max - b);

        public static CubeFloat operator *(CubeFloat a, Vector3 b)
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

        public static CubeFloat operator *(CubeFloat a, float b)
        {
            if (b >= 0)
            {
                return new(a.min * b, a.max * b);
            }

            return new(a.max * b, a.min * b);
        }

        public static CubeFloat operator /(CubeFloat a, Vector3 b)
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

        public static CubeFloat operator /(CubeFloat a, float b)
        {
            if (b >= 0)
            {
                return new(a.min / b, a.max / b);
            }

            return new(a.max / b, a.min / b);
        }

        public static CubeFloat operator -(CubeFloat a) =>
            new(-a.max, -a.min);

        public static TesseractFloat operator *(CubeFloat a, RangeFloat b) =>
            new(a, b);

        public static TesseractFloat operator *(RangeFloat a, CubeFloat b) =>
            new(a, b);

        public static bool operator ==(CubeFloat a, CubeFloat b) =>
            a.Equals(b);

        public static bool operator !=(CubeFloat a, CubeFloat b) =>
            !a.Equals(b);

        #endregion

        #region Equatable

        public readonly bool Equals(CubeFloat other)
        {
            return min.Equals(other.min) && max.Equals(other.max);
        }

        public readonly override bool Equals(object obj)
        {
            return obj is CubeFloat other && Equals(other);
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
