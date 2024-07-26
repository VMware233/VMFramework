using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using VMFramework.Core.Linq;

namespace VMFramework.Core
{
    public static class KCubeUtility
    {
        #region Contains Cube

        /// <summary>
        ///     判断一个K维立方体是否包含另一个K维立方体
        /// </summary>
        /// <typeparam name="TPoint"></typeparam>
        /// <param name="cube"></param>
        /// <param name="smallerCube"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Contains<TPoint>(this IKCube<TPoint> cube, IKCube<TPoint> smallerCube)
            where TPoint : struct, IEquatable<TPoint>
        {
            return cube.Contains(smallerCube.min) && cube.Contains(smallerCube.max);
        }

        /// <summary>
        ///     判断一个K维立方体是否被另一个K维立方体包含
        /// </summary>
        /// <typeparam name="TPoint"></typeparam>
        /// <param name="cube"></param>
        /// <param name="biggerCube"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ContainsBy<TPoint>(this IKCube<TPoint> cube, IKCube<TPoint> biggerCube)
            where TPoint : struct, IEquatable<TPoint>
        {
            return biggerCube.Contains(cube.min) && biggerCube.Contains(cube.max);
        }

        #endregion

        #region Clamp

        /// <summary>
        ///     以此K维立方体为基础，截断一个点，确保这个点在K维立方体内
        /// </summary>
        /// <typeparam name="TPoint"></typeparam>
        /// <param name="cube"></param>
        /// <param name="pos"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TPoint Clamp<TPoint>(this IKCube<TPoint> cube, TPoint pos)
            where TPoint : struct, IEquatable<TPoint>
        {
            return cube.ClampMin(cube.ClampMax(pos));
        }

        /// <summary>
        ///     以一个K维立方体为基础，截断另一个K维立方体，或者说返回两个K维立方体的交
        /// </summary>
        /// <typeparam name="TPoint"></typeparam>
        /// <typeparam name="TKCube"></typeparam>
        /// <param name="cube"></param>
        /// <param name="otherCube"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TKCube Clamp<TPoint, TKCube>(this TKCube cube, TKCube otherCube)
            where TPoint : struct, IEquatable<TPoint> where TKCube : struct, IKCube<TPoint>
        {
            return new TKCube
            {
                min = cube.Clamp(otherCube.min),
                max = cube.Clamp(otherCube.max)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TKCube ClampBy<TPoint, TKCube>(this TKCube cube, TKCube otherCube)
            where TPoint : struct, IEquatable<TPoint> where TKCube : struct, IKCube<TPoint>
        {
            return new TKCube
            {
                min = otherCube.Clamp(cube.min),
                max = otherCube.Clamp(cube.max)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TKCube ClampBy<TPoint, TKCube>(this TKCube cube, TPoint min, TPoint max)
            where TPoint : struct, IEquatable<TPoint> where TKCube : struct, IKCube<TPoint>
        {
            var otherCube = new TKCube
            {
                min = min,
                max = max
            };
            return new TKCube
            {
                min = otherCube.Clamp(cube.min),
                max = otherCube.Clamp(cube.max)
            };
        }

        /// <summary>
        ///     以一个K维立方体为基础，截断poses内的所有点，确保这些点在K维立方体内
        /// </summary>
        /// <typeparam name="TPoint"></typeparam>
        /// <typeparam name="TKCube"></typeparam>
        /// <param name="cube"></param>
        /// <param name="poses"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<TPoint> Clamp<TPoint, TKCube>(this TKCube cube, IEnumerable<TPoint> poses)
            where TPoint : struct, IEquatable<TPoint> where TKCube : struct, IKCube<TPoint>
        {
            return poses.Select(cube.Clamp);
        }

        #endregion

        #region Geometry

        /// <summary>
        ///     返回两个K维立方体的交，或者说以一个K维立方体为基础，截断另一个K维立方体
        /// </summary>
        /// <typeparam name="TPoint"></typeparam>
        /// <param name="cube"></param>
        /// <param name="otherCube"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (TPoint min, TPoint max) IntersectsWith<TPoint>(this IKCube<TPoint> cube,
            IKCube<TPoint> otherCube) where TPoint : struct, IEquatable<TPoint>
        {
            return (cube.ClampMin(otherCube.min), cube.ClampMax(otherCube.max));
        }

        /// <summary>
        ///     判断两个K维立方体是否相交
        /// </summary>
        /// <param name="cube"></param>
        /// <param name="otherCube"></param>
        /// <returns></returns>
        public static bool Overlaps<TPoint>(this IKCube<TPoint> cube, IKCube<TPoint> otherCube)
            where TPoint : struct, IEquatable<TPoint>
        {
            return cube.Contains(otherCube.min) || cube.Contains(otherCube.max);
        }

        #endregion

        #region Get Random Unique Points

        /// <summary>
        ///     获取K维立方体内特定数量的不重复的随机点
        /// </summary>
        /// <param name="cube"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<int> GetRandomUniquePoints(this IKCube<int> cube, int count)
        {
            return count.GenerateUniqueIntegers(cube.min, cube.max);
        }

        /// <summary>
        ///     获取K维立方体内特定数量的不重复的随机点
        /// </summary>
        /// <param name="cube"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Vector2Int> GetRandomUniquePoints(this IKCube<Vector2Int> cube, int count)
        {
            return count.GenerateUniqueVector2Ints(cube.min, cube.max);
        }

        /// <summary>
        ///     获取K维立方体内特定数量的不重复的随机点
        /// </summary>
        /// <param name="cube"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Vector3Int> GetRandomUniquePoints(this IKCube<Vector3Int> cube, int count)
        {
            return count.GenerateUniqueVector3Ints(cube.min, cube.max);
        }

        #endregion

        #region Boundary

        #region Get All Boundary Points

        /// <summary>
        ///     获取K维立方体的边界上的所有点
        /// </summary>
        /// <param name="cube"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Vector3Int> GetAllBoundaryPoints(this IKCube<Vector3Int> cube)
        {
            return cube.min.GetAllFacePointsOfCube(cube.max);
        }

        #endregion

        #endregion
    }
}