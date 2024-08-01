using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using VMFramework.Core.Linq;

namespace VMFramework.Core
{
    public static partial class Math
    {
        #region Number

        #region Basic

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float F(this int num)
        {
            return num;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float F(this double num)
        {
            return (float)num;
        }

        #endregion

        #region PingPong

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int PingPong(this int num, int length)
        {
            length--;
            return length - (num.Repeat(length + length) - length).Abs();
        }

        #endregion

        #region Caculate

        #region Average

        public static float Average(this IEnumerable<float> enumerable,
            int fromIndex, int endIndex)
        {
            return enumerable.Sum(fromIndex, endIndex) / (endIndex - fromIndex + 1);
        }

        #endregion

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Any(params bool[] args)
        {
            return Enumerable.Any(args);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool All(params bool[] args)
        {
            return Enumerable.Any(args);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Percent(this float t, float min, float max)
        {
            float percent = t.Normalize01(min, max).Clamp(0, 1);

            return percent * 100;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Percent(this float t)
        {
            return t.Percent(0, 100);
        }

        #endregion

        #endregion

        #region Vector

        #region Basic

        #region Insert

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int InsertAsX(this Vector2Int vector, int x)
        {
            return new(x, vector.x, vector.y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int InsertAsY(this Vector2Int vector, int y)
        {
            return new(vector.x, y, vector.y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int InsertAsZ(this Vector2Int vector, int z)
        {
            return new(vector.x, vector.y, z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int InsertAs(this Vector2Int vector2, int extraNum,
            PlaneType planeType)
        {
            return planeType switch
            {
                PlaneType.XY => vector2.InsertAsZ(extraNum),
                PlaneType.XZ => vector2.InsertAsY(extraNum),
                PlaneType.YZ => vector2.InsertAsX(extraNum),
                _ => throw new ArgumentOutOfRangeException(nameof(planeType),
                    planeType, null)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 InsertAsX(this Vector2 vector, float x)
        {
            return new(x, vector.x, vector.y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 InsertAsY(this Vector2 vector, float y)
        {
            return new(vector.x, y, vector.y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 InsertAsZ(this Vector2 vector, float z)
        {
            return new(vector.x, vector.y, z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 InsertAs(this Vector2 vector2, float extraNum,
            PlaneType planeType)
        {
            return planeType switch
            {
                PlaneType.XY => vector2.InsertAsZ(extraNum),
                PlaneType.XZ => vector2.InsertAsY(extraNum),
                PlaneType.YZ => vector2.InsertAsX(extraNum),
                _ => throw new ArgumentOutOfRangeException(nameof(planeType),
                    planeType, null)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int As3DXY(this Vector2Int vector)
        {
            return vector.InsertAsZ(0);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int As3DXZ(this Vector2Int vector)
        {
            return vector.InsertAsY(0);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int As3DYZ(this Vector2Int vector)
        {
            return vector.InsertAsX(0);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 As3DXY(this Vector2 vector)
        {
            return vector.InsertAsZ(0);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 As3DXZ(this Vector2 vector)
        {
            return vector.InsertAsY(0);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 As3DYZ(this Vector2 vector)
        {
            return vector.InsertAsX(0);
        }

        #endregion

        #region Extract

        public static Vector2 ExtractAs(this Vector3 vector3, PlaneType planeType)
        {
            return planeType switch
            {
                PlaneType.XY => new(vector3.x, vector3.y),
                PlaneType.XZ => new(vector3.x, vector3.z),
                PlaneType.YZ => new(vector3.y, vector3.z),
                _ => throw new ArgumentOutOfRangeException(nameof(planeType),
                    planeType, null)
            };
        }
        
        public static Vector2Int ExtractAs(this Vector3Int vector3, PlaneType planeType)
        {
            return planeType switch
            {
                PlaneType.XY => new(vector3.x, vector3.y),
                PlaneType.XZ => new(vector3.x, vector3.z),
                PlaneType.YZ => new(vector3.y, vector3.z),
                _ => throw new ArgumentOutOfRangeException(nameof(planeType),
                    planeType, null)
            };
        }

        #endregion

        #region Replace

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 ReplaceX(this Vector4 vector, float x)
        {
            return new(x, vector.y, vector.z, vector.w);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 ReplaceY(this Vector4 vector, float y)
        {
            return new(vector.x, y, vector.z, vector.w);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 ReplaceZ(this Vector4 vector, float z)
        {
            return new(vector.x, vector.y, z, vector.w);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 ReplaceW(this Vector4 vector, float w)
        {
            return new(vector.x, vector.y, vector.z, w);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color ReplaceRed(this Color color, float red)
        {
            return new(red, color.g, color.b, color.a);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color ReplaceGreen(this Color color, float green)
        {
            return new(color.r, green, color.b, color.a);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color ReplaceBlue(this Color color, float blue)
        {
            return new(color.r, color.g, blue, color.a);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color ReplaceAlpha(this Color color, float alpha)
        {
            return new(color.r, color.g, color.b, alpha);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 ReplaceX(this Vector3 vector, float x)
        {
            return new(x, vector.y, vector.z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 ReplaceY(this Vector3 vector, float y)
        {
            return new(vector.x, y, vector.z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 ReplaceZ(this Vector3 vector, float z)
        {
            return new(vector.x, vector.y, z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int ReplaceX(this Vector3Int vector, int x)
        {
            return new(x, vector.y, vector.z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int ReplaceY(this Vector3Int vector, int y)
        {
            return new(vector.x, y, vector.z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int ReplaceZ(this Vector3Int vector, int z)
        {
            return new(vector.x, vector.y, z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 ReplaceXY(this Vector3 vector, float x, float y)
        {
            return new(x, y, vector.z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 ReplaceYZ(this Vector3 vector, float y, float z)
        {
            return new(vector.x, y, z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 ReplaceXZ(this Vector3 vector, float x, float z)
        {
            return new(x, vector.y, z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int ReplaceXY(this Vector3Int vector, int x, int y)
        {
            return new(x, y, vector.z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int ReplaceYZ(this Vector3Int vector, int y, int z)
        {
            return new(vector.x, y, z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int ReplaceXZ(this Vector3Int vector, int x, int z)
        {
            return new(x, vector.y, z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 ReplaceXY(this Vector3 vector, Vector2 xy)
        {
            return new(xy.x, xy.y, vector.z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 ReplaceYZ(this Vector3 vector, Vector2 yz)
        {
            return new(vector.x, yz.x, yz.y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 ReplaceXZ(this Vector3 vector, Vector2 xz)
        {
            return new(xz.x, vector.y, xz.y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int ReplaceXY(this Vector3Int vector, Vector2Int xy)
        {
            return new(xy.x, xy.y, vector.z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int ReplaceYZ(this Vector3Int vector, Vector2Int yz)
        {
            return new(vector.x, yz.x, yz.y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int ReplaceXZ(this Vector3Int vector, Vector2Int xz)
        {
            return new(xz.x, vector.y, xz.y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 ReplaceX(this Vector2 vector, float x)
        {
            return new(x, vector.y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 ReplaceY(this Vector2 vector, float y)
        {
            return new(vector.x, y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Int ReplaceX(this Vector2Int vector, int x)
        {
            return new(x, vector.y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Int ReplaceY(this Vector2Int vector, int y)
        {
            return new(vector.x, y);
        }

        #endregion

        #region Add

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 AddX(this Vector4 vector, float x)
        {
            return vector.ReplaceX(vector.x + x);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 AddY(this Vector4 vector, float y)
        {
            return vector.ReplaceY(vector.y + y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 AddZ(this Vector4 vector, float z)
        {
            return vector.ReplaceZ(vector.z + z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 AddW(this Vector4 vector, float w)
        {
            return vector.ReplaceW(vector.w + w);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color AddRed(this Color color, float r)
        {
            return color.ReplaceRed(color.r + r);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color AddGreen(this Color color, float g)
        {
            return color.ReplaceGreen(color.g + g);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color AddBlue(this Color color, float b)
        {
            return color.ReplaceBlue(color.b + b);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color AddAlpha(this Color color, float a)
        {
            return color.ReplaceAlpha(color.a + a);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int AddX(this Vector3Int vector, int x)
        {
            return vector.ReplaceX(vector.x + x);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int AddY(this Vector3Int vector, int y)
        {
            return vector.ReplaceY(vector.y + y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int AddZ(this Vector3Int vector, int z)
        {
            return vector.ReplaceZ(vector.z + z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Int AddX(this Vector2Int vector, int x)
        {
            return vector.ReplaceX(vector.x + x);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Int AddY(this Vector2Int vector, int y)
        {
            return vector.ReplaceY(vector.y + y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 AddX(this Vector3 vector, float x)
        {
            return vector.ReplaceX(vector.x + x);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 AddY(this Vector3 vector, float y)
        {
            return vector.ReplaceY(vector.y + y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 AddZ(this Vector3 vector, float z)
        {
            return vector.ReplaceZ(vector.z + z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 AddX(this Vector2 vector, float x)
        {
            return vector.ReplaceX(vector.x + x);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 AddY(this Vector2 vector, float y)
        {
            return vector.ReplaceY(vector.y + y);
        }

        #endregion

        #endregion

        #region Scale To

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 ScaleTo(this Vector2 vector, float length)
        {
            var magnitude = vector.magnitude;
            if (magnitude <= float.Epsilon)
            {
                return Vector2.zero;
            }
            
            var factor = length / magnitude;
            return vector * factor;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 ScaleTo(this Vector3 vector, float length) {
            var magnitude = vector.magnitude;
            if (magnitude <= float.Epsilon)
            {
                return Vector3.zero;
            }
            
            var factor = length / magnitude;
            return vector * factor;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 ScaleTo(this Vector4 vector, float length) {
            var magnitude = vector.magnitude;
            if (magnitude <= float.Epsilon)
            {
                return Vector4.zero;
            }
            
            var factor = length / magnitude;
            return vector * factor;
        }

        #endregion

        #endregion
    }
}