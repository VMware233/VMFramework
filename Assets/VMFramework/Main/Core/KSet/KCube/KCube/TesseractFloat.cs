using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace VMFramework.Core
{
    public struct TesseractFloat : IKCubeFloat<Vector4>, IEquatable<TesseractFloat>, IFormattable
    {
        public static TesseractFloat zero { get; } =
            new(Vector4.zero, Vector4.zero);

        public static TesseractFloat one { get; } =
            new(Vector4.one, Vector4.one);

        public static TesseractFloat unit { get; } =
            new(Vector4.zero, Vector4.one);

        public readonly Vector4 size => max - min;

        public readonly Vector4 pivot => (max + min) / 2;

        public readonly Vector4 extents => (max - min) / 2;

        public readonly Vector4 min, max;

        public readonly RangeFloat xRange => new(min.x, max.x);

        public readonly RangeFloat yRange => new(min.y, max.y);

        public readonly RangeFloat zRange => new(min.z, max.z);

        public readonly RangeFloat wRange => new(min.w, max.w);

        public readonly RectangleFloat xyRectangle =>
            new(min.x, min.y, max.x, max.y);

        public readonly RectangleFloat xzRectangle =>
            new(min.x, min.z, max.x, max.z);

        public readonly RectangleFloat yzRectangle =>
            new(min.y, min.z, max.y, max.z);

        public readonly RectangleFloat xwRectangle =>
            new(min.x, min.w, max.x, max.w);

        public readonly RectangleFloat ywRectangle =>
            new(min.y, min.w, max.y, max.w);

        public readonly RectangleFloat zwRectangle =>
            new(min.z, min.w, max.z, max.w);

        public readonly CubeFloat xyzCube =>
            new(min.x, min.y, min.z, max.x, max.y, max.z);

        public readonly CubeFloat xywCube =>
            new(min.x, min.y, min.w, max.x, max.y, max.w);

        public readonly CubeFloat xzwCube =>
            new(min.x, min.z, min.w, max.x, max.z, max.w);

        public readonly CubeFloat yzwCube =>
            new(min.y, min.z, min.w, max.y, max.z, max.w);

        #region Constructor

        public TesseractFloat(RangeFloat xRange, RangeFloat yRange,
            RangeFloat zRange, RangeFloat wRange)
        {
            min = new Vector4(xRange.min, yRange.min, zRange.min, wRange.min);
            max = new Vector4(xRange.max, yRange.max, zRange.max, wRange.max);
        }

        public TesseractFloat(CubeFloat xyzCube, RangeFloat wRange)
        {
            min = new Vector4(xyzCube.min.x, xyzCube.min.y, xyzCube.min.z,
                wRange.min);
            max = new Vector4(xyzCube.max.x, xyzCube.max.y, xyzCube.max.z,
                wRange.max);
        }

        public TesseractFloat(RangeFloat xRange, CubeFloat yzwCube)
        {
            min = new Vector4(xRange.min, yzwCube.min.x, yzwCube.min.y,
                yzwCube.min.z);
            max = new Vector4(xRange.max, yzwCube.max.x, yzwCube.max.y,
                yzwCube.max.z);
        }

        public TesseractFloat(float xMin, float yMin, float zMin, float wMin,
            float xMax, float yMax,
            float zMax, float wMax)
        {
            min = new Vector4(xMin, yMin, zMin, wMin);
            max = new Vector4(xMax, yMax, zMax, wMax);
        }

        public TesseractFloat(Vector4 min, Vector4 max)
        {
            this.min = min;
            this.max = max;
        }

        public TesseractFloat(float sizeX, float sizeY, float sizeZ, float sizeW)
        {
            min = Vector4.zero;
            max = new Vector4(sizeX, sizeY, sizeZ, sizeW);
        }

        public TesseractFloat(Vector4 size)
        {
            min = Vector4.zero;
            max = size;
        }

        public TesseractFloat(TesseractFloat source)
        {
            min = source.min;
            max = source.max;
        }

        public TesseractFloat(IKCube<Vector4> config)
        {
            if (config is null)
            {
                min = Vector4.zero;
                max = Vector4.zero;
                return;
            }

            min = config.min;
            max = config.max;
        }

        #endregion

        #region IKCube

        readonly Vector4 IKCube<Vector4>.min
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => min;
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            init => min = value;
        }

        readonly Vector4 IKCube<Vector4>.max
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => max;
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            init => max = value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly bool Contains(Vector4 pos) =>
            pos.x >= min.x && pos.x <= max.x &&
            pos.y >= min.y && pos.y <= max.y &&
            pos.z >= min.z && pos.z <= max.z &&
            pos.w >= min.w && pos.w <= max.w;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly Vector4 GetRelativePos(Vector4 pos) => pos - min;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly Vector4 ClampMin(Vector4 pos) => pos.ClampMin(min);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly Vector4 ClampMax(Vector4 pos) => pos.ClampMax(max);

        #endregion

        #region Operator

        public static TesseractFloat operator +(TesseractFloat a, Vector4 b) =>
            new(a.min + b, a.max + b);

        public static TesseractFloat operator -(TesseractFloat a, Vector4 b) =>
            new(a.min - b, a.max - b);

        public static TesseractFloat operator *(TesseractFloat a, Vector4 b)
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

            var wMin = a.min.w;
            var wMax = a.max.w;

            if (b.w >= 0)
            {
                wMin *= b.w;
                wMax *= b.w;
            }
            else
            {
                (wMin, wMax) = (wMax * b.w, wMin * b.w);
            }

            return new(xMin, yMin, zMin, wMin, xMax, yMax, zMax, wMax);
        }

        public static TesseractFloat operator *(TesseractFloat a, float b)
        {
            if (b >= 0)
            {
                return new(a.min * b, a.max * b);
            }

            return new(a.max * b, a.min * b);
        }

        public static TesseractFloat operator /(TesseractFloat a, Vector4 b)
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

            var wMin = a.min.w;
            var wMax = a.max.w;

            if (b.w >= 0)
            {
                wMin /= b.w;
                wMax /= b.w;
            }
            else
            {
                (wMin, wMax) = (wMax / b.w, wMin / b.w);
            }

            return new(xMin, yMin, zMin, wMin, xMax, yMax, zMax, wMax);
        }

        public static TesseractFloat operator /(TesseractFloat a, float b)
        {
            if (b >= 0)
            {
                return new(a.min / b, a.max / b);
            }

            return new(a.max / b, a.min / b);
        }

        public static TesseractFloat operator -(TesseractFloat a) =>
            new(-a.max, -a.min);

        public static bool operator ==(TesseractFloat a, TesseractFloat b) =>
            a.Equals(b);

        public static bool operator !=(TesseractFloat a, TesseractFloat b) =>
            !a.Equals(b);

        #endregion

        #region Equatable

        public readonly bool Equals(TesseractFloat other)
        {
            return min.Equals(other.min) && max.Equals(other.max);
        }

        public readonly override bool Equals(object obj)
        {
            return obj is TesseractFloat other && Equals(other);
        }

        public readonly override int GetHashCode()
        {
            return HashCode.Combine(min, max);
        }

        #endregion

        #region To String

        public readonly override string ToString() => $"[{min}, {max}]";

        public readonly string ToString(string format,
            IFormatProvider formatProvider) =>
            $"[{min.ToString(format, formatProvider)},{max.ToString(format, formatProvider)}]";

        #endregion
    }
}
