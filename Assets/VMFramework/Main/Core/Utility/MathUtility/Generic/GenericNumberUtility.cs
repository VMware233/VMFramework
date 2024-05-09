using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace VMFramework.Core.Generic
{
    public static class GenericNumberUtility
    {
        public static readonly HashSet<Type> numberTypes = new()
        {
            typeof(int),
            typeof(float),
            typeof(double)
        };

        public static readonly HashSet<Type> vectorTypes = new()
        {
            typeof(Vector2),
            typeof(Vector3),
            typeof(Vector2Int),
            typeof(Vector3Int),
            typeof(Vector4),
            typeof(Color)
        };

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNumber(this Type type) => numberTypes.Contains(type);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsVector(this Type type) => vectorTypes.Contains(type);

        public static T ConvertNumber<T>(object value) =>
            (T)Convert.ChangeType(value, typeof(T));

        #region Clamp

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ClampMin<T>(this T a, T min) where T : IEquatable<T>
        {
            return a switch
            {
                int => MathUtility.ClampMin(a.ConvertTo<int>(), min.ConvertTo<int>())
                    .ConvertTo<T>(),
                float => MathUtility
                    .ClampMin(a.ConvertTo<float>(), min.ConvertTo<float>())
                    .ConvertTo<T>(),
                double => MathUtility
                    .ClampMin(a.ConvertTo<double>(), min.ConvertTo<double>())
                    .ConvertTo<T>(),
                Vector2 => MathUtility
                    .ClampMin(a.ConvertTo<Vector2>(), min.ConvertTo<Vector2>())
                    .ConvertTo<T>(),
                Vector3 => MathUtility
                    .ClampMin(a.ConvertTo<Vector3>(), min.ConvertTo<Vector3>())
                    .ConvertTo<T>(),
                Vector4 => MathUtility
                    .ClampMin(a.ConvertTo<Vector4>(), min.ConvertTo<Vector4>())
                    .ConvertTo<T>(),
                Vector2Int => MathUtility.ClampMin(a.ConvertTo<Vector2Int>(),
                    min.ConvertTo<Vector2Int>()).ConvertTo<T>(),
                Vector3Int => MathUtility.ClampMin(a.ConvertTo<Vector3Int>(),
                    min.ConvertTo<Vector3Int>()).ConvertTo<T>(),
                Color => MathUtility
                    .ClampMin(a.ConvertTo<Color>(), min.ConvertTo<Color>())
                    .ConvertTo<T>(),
                _ => throw new ArgumentException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ClampMax<T>(this T a, T min) where T : IEquatable<T>
        {
            return a switch
            {
                int => MathUtility.ClampMax(a.ConvertTo<int>(), min.ConvertTo<int>())
                    .ConvertTo<T>(),
                float => MathUtility
                    .ClampMax(a.ConvertTo<float>(), min.ConvertTo<float>())
                    .ConvertTo<T>(),
                double => MathUtility
                    .ClampMax(a.ConvertTo<double>(), min.ConvertTo<double>())
                    .ConvertTo<T>(),
                Vector2 => MathUtility
                    .ClampMax(a.ConvertTo<Vector2>(), min.ConvertTo<Vector2>())
                    .ConvertTo<T>(),
                Vector3 => MathUtility
                    .ClampMax(a.ConvertTo<Vector3>(), min.ConvertTo<Vector3>())
                    .ConvertTo<T>(),
                Vector4 => MathUtility
                    .ClampMax(a.ConvertTo<Vector4>(), min.ConvertTo<Vector4>())
                    .ConvertTo<T>(),
                Vector2Int => MathUtility.ClampMax(a.ConvertTo<Vector2Int>(),
                    min.ConvertTo<Vector2Int>()).ConvertTo<T>(),
                Vector3Int => MathUtility.ClampMax(a.ConvertTo<Vector3Int>(),
                    min.ConvertTo<Vector3Int>()).ConvertTo<T>(),
                Color => MathUtility
                    .ClampMax(a.ConvertTo<Color>(), min.ConvertTo<Color>())
                    .ConvertTo<T>(),
                _ => throw new ArgumentException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ClampMin<T>(this T a, double min) where T : IEquatable<T>
        {
            return a switch
            {
                int intValue => MathUtility.ClampMin(intValue, min).ConvertTo<T>(),
                float floatValue => MathUtility.ClampMin(floatValue, min)
                    .ConvertTo<T>(),
                double doubleValue => MathUtility.ClampMin(doubleValue, min)
                    .ConvertTo<T>(),
                Vector2 vector => MathUtility.ClampMin(vector, (float)min)
                    .ConvertTo<T>(),
                Vector3 vector => MathUtility.ClampMin(vector, (float)min)
                    .ConvertTo<T>(),
                Vector4 vector => MathUtility.ClampMin(vector, (float)min)
                    .ConvertTo<T>(),
                Vector2Int vector => MathUtility
                    .ClampMin(vector, MathUtility.Ceiling(min)).ConvertTo<T>(),
                Vector3Int vector => MathUtility
                    .ClampMin(vector, MathUtility.Ceiling(min)).ConvertTo<T>(),
                Color color => MathUtility.ClampMin(color, (float)min)
                    .ConvertTo<T>(),
                _ => throw new ArgumentException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ClampMax<T>(this T a, double min) where T : IEquatable<T>
        {
            return a switch
            {
                int intValue => MathUtility.ClampMax(intValue, min).ConvertTo<T>(),
                float floatValue => MathUtility.ClampMax(floatValue, min)
                    .ConvertTo<T>(),
                double doubleValue => MathUtility.ClampMax(doubleValue, min)
                    .ConvertTo<T>(),
                Vector2 vector => MathUtility.ClampMax(vector, (float)min)
                    .ConvertTo<T>(),
                Vector3 vector => MathUtility.ClampMax(vector, (float)min)
                    .ConvertTo<T>(),
                Vector4 vector => MathUtility.ClampMax(vector, (float)min)
                    .ConvertTo<T>(),
                Vector2Int vector => MathUtility
                    .ClampMax(vector, MathUtility.Floor(min)).ConvertTo<T>(),
                Vector3Int vector => MathUtility
                    .ClampMax(vector, MathUtility.Floor(min)).ConvertTo<T>(),
                Color color => MathUtility.ClampMax(color, (float)min)
                    .ConvertTo<T>(),
                _ => throw new ArgumentException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Clamp<T>(this T a, T min, T max) where T : IEquatable<T>
        {
            return a.ClampMin(min).ClampMax(max);
        }

        #endregion

        #region Compare

        #region AllNumber

        /// <summary>
        /// return true if num is in range [min, max]
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="num"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AllNumberBetweenInclusive<T>(this T num, T min, T max)
            where T : IEquatable<T>
        {
            return num.AllNumberAboveOrEqual(min) && num.AllNumberBelowOrEqual(max);
        }

        /// <summary>
        /// return true if num is in range (min, max)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="num"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AllNumberBetweenExclusive<T>(this T num, T min, T max)
            where T : IEquatable<T>
        {
            return num.AllNumberAbove(min) && num.AllNumberBelow(max);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AllNumberBelow<T>(this T a, T comparison)
            where T : IEquatable<T>
        {
            return a switch
            {
                int => MathUtility.AllNumberBelow(a.ConvertTo<int>(),
                    comparison.ConvertTo<int>()),
                float => MathUtility.AllNumberBelow(a.ConvertTo<float>(),
                    comparison.ConvertTo<float>()),
                double => MathUtility.AllNumberBelow(a.ConvertTo<double>(),
                    comparison.ConvertTo<double>()),
                Vector2 => MathUtility.AllNumberBelow(a.ConvertTo<Vector2>(),
                    comparison.ConvertTo<Vector2>()),
                Vector3 => MathUtility.AllNumberBelow(a.ConvertTo<Vector3>(),
                    comparison.ConvertTo<Vector3>()),
                Vector4 => MathUtility.AllNumberBelow(a.ConvertTo<Vector4>(),
                    comparison.ConvertTo<Vector4>()),
                Vector2Int => MathUtility.AllNumberBelow(a.ConvertTo<Vector2Int>(),
                    comparison.ConvertTo<Vector2Int>()),
                Vector3Int => MathUtility.AllNumberBelow(a.ConvertTo<Vector3Int>(),
                    comparison.ConvertTo<Vector3Int>()),
                Color => MathUtility.AllNumberBelow(a.ConvertTo<Color>(),
                    comparison.ConvertTo<Color>()),
                _ => throw new ArgumentException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AllNumberAbove<T>(this T a, T comparison)
            where T : IEquatable<T>
        {
            return a switch
            {
                int => MathUtility.AllNumberAbove(a.ConvertTo<int>(),
                    comparison.ConvertTo<int>()),
                float => MathUtility.AllNumberAbove(a.ConvertTo<float>(),
                    comparison.ConvertTo<float>()),
                double => MathUtility.AllNumberAbove(a.ConvertTo<double>(),
                    comparison.ConvertTo<double>()),
                Vector2 => MathUtility.AllNumberAbove(a.ConvertTo<Vector2>(),
                    comparison.ConvertTo<Vector2>()),
                Vector3 => MathUtility.AllNumberAbove(a.ConvertTo<Vector3>(),
                    comparison.ConvertTo<Vector3>()),
                Vector4 => MathUtility.AllNumberAbove(a.ConvertTo<Vector4>(),
                    comparison.ConvertTo<Vector4>()),
                Vector2Int => MathUtility.AllNumberAbove(a.ConvertTo<Vector2Int>(),
                    comparison.ConvertTo<Vector2Int>()),
                Vector3Int => MathUtility.AllNumberAbove(a.ConvertTo<Vector3Int>(),
                    comparison.ConvertTo<Vector3Int>()),
                Color => MathUtility.AllNumberAbove(a.ConvertTo<Color>(),
                    comparison.ConvertTo<Color>()),
                _ => throw new ArgumentException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AllNumberBelowOrEqual<T>(this T a, T comparison)
            where T : IEquatable<T>
        {
            return a switch
            {
                int => MathUtility.AllNumberBelowOrEqual(a.ConvertTo<int>(),
                    comparison.ConvertTo<int>()),
                float => MathUtility.AllNumberBelowOrEqual(a.ConvertTo<float>(),
                    comparison.ConvertTo<float>()),
                double => MathUtility.AllNumberBelowOrEqual(a.ConvertTo<double>(),
                    comparison.ConvertTo<double>()),
                Vector2 => MathUtility.AllNumberBelowOrEqual(a.ConvertTo<Vector2>(),
                    comparison.ConvertTo<Vector2>()),
                Vector3 => MathUtility.AllNumberBelowOrEqual(a.ConvertTo<Vector3>(),
                    comparison.ConvertTo<Vector3>()),
                Vector4 => MathUtility.AllNumberBelowOrEqual(a.ConvertTo<Vector4>(),
                    comparison.ConvertTo<Vector4>()),
                Vector2Int => MathUtility.AllNumberBelowOrEqual(
                    a.ConvertTo<Vector2Int>(), comparison.ConvertTo<Vector2Int>()),
                Vector3Int => MathUtility.AllNumberBelowOrEqual(
                    a.ConvertTo<Vector3Int>(), comparison.ConvertTo<Vector3Int>()),
                Color => MathUtility.AllNumberBelowOrEqual(a.ConvertTo<Color>(),
                    comparison.ConvertTo<Color>()),
                _ => throw new ArgumentException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AllNumberAboveOrEqual<T>(this T a, T comparison)
            where T : IEquatable<T>
        {
            return a switch
            {
                int => MathUtility.AllNumberAboveOrEqual(a.ConvertTo<int>(),
                    comparison.ConvertTo<int>()),
                float => MathUtility.AllNumberAboveOrEqual(a.ConvertTo<float>(),
                    comparison.ConvertTo<float>()),
                double => MathUtility.AllNumberAboveOrEqual(a.ConvertTo<double>(),
                    comparison.ConvertTo<double>()),
                Vector2 => MathUtility.AllNumberAboveOrEqual(a.ConvertTo<Vector2>(),
                    comparison.ConvertTo<Vector2>()),
                Vector3 => MathUtility.AllNumberAboveOrEqual(a.ConvertTo<Vector3>(),
                    comparison.ConvertTo<Vector3>()),
                Vector4 => MathUtility.AllNumberAboveOrEqual(a.ConvertTo<Vector4>(),
                    comparison.ConvertTo<Vector4>()),
                Vector2Int => MathUtility.AllNumberAboveOrEqual(
                    a.ConvertTo<Vector2Int>(), comparison.ConvertTo<Vector2Int>()),
                Vector3Int => MathUtility.AllNumberAboveOrEqual(
                    a.ConvertTo<Vector3Int>(), comparison.ConvertTo<Vector3Int>()),
                Color => MathUtility.AllNumberAboveOrEqual(a.ConvertTo<Color>(),
                    comparison.ConvertTo<Color>()),
                _ => throw new ArgumentException()
            };
        }

        #endregion

        #region AnyNumber

        /// <summary>
        /// return true if num is in range [min, max]
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="num"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AnyNumberBetweenInclusive<T>(this T num, T min, T max)
            where T : IEquatable<T>
        {
            return num.AnyNumberAboveOrEqual(min) && num.AnyNumberBelowOrEqual(max);
        }

        /// <summary>
        /// return true if num is in range (min, max)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="num"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AnyNumberBetweenExclusive<T>(this T num, T min, T max)
            where T : IEquatable<T>
        {
            return num.AnyNumberAbove(min) && num.AnyNumberBelow(max);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AnyNumberBelow<T>(this T a, T comparison)
            where T : IEquatable<T>
        {
            return a switch
            {
                int => MathUtility.AnyNumberBelow(a.ConvertTo<int>(),
                    comparison.ConvertTo<int>()),
                float => MathUtility.AnyNumberBelow(a.ConvertTo<float>(),
                    comparison.ConvertTo<float>()),
                double => MathUtility.AnyNumberBelow(a.ConvertTo<double>(),
                    comparison.ConvertTo<double>()),
                Vector2 => MathUtility.AnyNumberBelow(a.ConvertTo<Vector2>(),
                    comparison.ConvertTo<Vector2>()),
                Vector3 => MathUtility.AnyNumberBelow(a.ConvertTo<Vector3>(),
                    comparison.ConvertTo<Vector3>()),
                Vector4 => MathUtility.AnyNumberBelow(a.ConvertTo<Vector4>(),
                    comparison.ConvertTo<Vector4>()),
                Vector2Int => MathUtility.AnyNumberBelow(a.ConvertTo<Vector2Int>(),
                    comparison.ConvertTo<Vector2Int>()),
                Vector3Int => MathUtility.AnyNumberBelow(a.ConvertTo<Vector3Int>(),
                    comparison.ConvertTo<Vector3Int>()),
                Color => MathUtility.AnyNumberBelow(a.ConvertTo<Color>(),
                    comparison.ConvertTo<Color>()),
                _ => throw new ArgumentException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AnyNumberAbove<T>(this T a, T comparison)
            where T : IEquatable<T>
        {
            return a switch
            {
                int => MathUtility.AnyNumberAbove(a.ConvertTo<int>(),
                    comparison.ConvertTo<int>()),
                float => MathUtility.AnyNumberAbove(a.ConvertTo<float>(),
                    comparison.ConvertTo<float>()),
                double => MathUtility.AnyNumberAbove(a.ConvertTo<double>(),
                    comparison.ConvertTo<double>()),
                Vector2 => MathUtility.AnyNumberAbove(a.ConvertTo<Vector2>(),
                    comparison.ConvertTo<Vector2>()),
                Vector3 => MathUtility.AnyNumberAbove(a.ConvertTo<Vector3>(),
                    comparison.ConvertTo<Vector3>()),
                Vector4 => MathUtility.AnyNumberAbove(a.ConvertTo<Vector4>(),
                    comparison.ConvertTo<Vector4>()),
                Vector2Int => MathUtility.AnyNumberAbove(a.ConvertTo<Vector2Int>(),
                    comparison.ConvertTo<Vector2Int>()),
                Vector3Int => MathUtility.AnyNumberAbove(a.ConvertTo<Vector3Int>(),
                    comparison.ConvertTo<Vector3Int>()),
                Color => MathUtility.AnyNumberAbove(a.ConvertTo<Color>(),
                    comparison.ConvertTo<Color>()),
                _ => throw new ArgumentException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AnyNumberBelowOrEqual<T>(this T a, T comparison)
            where T : IEquatable<T>
        {
            return a switch
            {
                int => MathUtility.AnyNumberBelowOrEqual(a.ConvertTo<int>(),
                    comparison.ConvertTo<int>()),
                float => MathUtility.AnyNumberBelowOrEqual(a.ConvertTo<float>(),
                    comparison.ConvertTo<float>()),
                double => MathUtility.AnyNumberBelowOrEqual(a.ConvertTo<double>(),
                    comparison.ConvertTo<double>()),
                Vector2 => MathUtility.AnyNumberBelowOrEqual(a.ConvertTo<Vector2>(),
                    comparison.ConvertTo<Vector2>()),
                Vector3 => MathUtility.AnyNumberBelowOrEqual(a.ConvertTo<Vector3>(),
                    comparison.ConvertTo<Vector3>()),
                Vector4 => MathUtility.AnyNumberBelowOrEqual(a.ConvertTo<Vector4>(),
                    comparison.ConvertTo<Vector4>()),
                Vector2Int => MathUtility.AnyNumberBelowOrEqual(
                    a.ConvertTo<Vector2Int>(), comparison.ConvertTo<Vector2Int>()),
                Vector3Int => MathUtility.AnyNumberBelowOrEqual(
                    a.ConvertTo<Vector3Int>(), comparison.ConvertTo<Vector3Int>()),
                Color => MathUtility.AnyNumberBelowOrEqual(a.ConvertTo<Color>(),
                    comparison.ConvertTo<Color>()),
                _ => throw new ArgumentException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AnyNumberAboveOrEqual<T>(this T a, T comparison)
            where T : IEquatable<T>
        {
            return a switch
            {
                int => MathUtility.AnyNumberAboveOrEqual(a.ConvertTo<int>(),
                    comparison.ConvertTo<int>()),
                float => MathUtility.AnyNumberAboveOrEqual(a.ConvertTo<float>(),
                    comparison.ConvertTo<float>()),
                double => MathUtility.AnyNumberAboveOrEqual(a.ConvertTo<double>(),
                    comparison.ConvertTo<double>()),
                Vector2 => MathUtility.AnyNumberAboveOrEqual(a.ConvertTo<Vector2>(),
                    comparison.ConvertTo<Vector2>()),
                Vector3 => MathUtility.AnyNumberAboveOrEqual(a.ConvertTo<Vector3>(),
                    comparison.ConvertTo<Vector3>()),
                Vector4 => MathUtility.AnyNumberAboveOrEqual(a.ConvertTo<Vector4>(),
                    comparison.ConvertTo<Vector4>()),
                Vector2Int => MathUtility.AnyNumberAboveOrEqual(
                    a.ConvertTo<Vector2Int>(), comparison.ConvertTo<Vector2Int>()),
                Vector3Int => MathUtility.AnyNumberAboveOrEqual(
                    a.ConvertTo<Vector3Int>(), comparison.ConvertTo<Vector3Int>()),
                Color => MathUtility.AnyNumberAboveOrEqual(a.ConvertTo<Color>(),
                    comparison.ConvertTo<Color>()),
                _ => throw new ArgumentException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AnyNumberBelowOrEqual<T>(this T a, double comparison)
            where T : IEquatable<T>
        {
            return a switch
            {
                int => MathUtility.AnyNumberBelowOrEqual(a.ConvertTo<int>(),
                    MathUtility.Floor(comparison)),
                float => MathUtility.AnyNumberBelowOrEqual(a.ConvertTo<float>(),
                    comparison.F()),
                double => MathUtility.AnyNumberBelowOrEqual(a.ConvertTo<double>(),
                    comparison),
                Vector2 => MathUtility.AnyNumberBelowOrEqual(a.ConvertTo<Vector2>(),
                    comparison.F()),
                Vector3 => MathUtility.AnyNumberBelowOrEqual(a.ConvertTo<Vector3>(),
                    comparison.F()),
                Vector4 => MathUtility.AnyNumberBelowOrEqual(a.ConvertTo<Vector4>(),
                    comparison.F()),
                Vector2Int => MathUtility.AnyNumberBelowOrEqual(
                    a.ConvertTo<Vector2Int>(), MathUtility.Floor(comparison)),
                Vector3Int => MathUtility.AnyNumberBelowOrEqual(
                    a.ConvertTo<Vector3Int>(), MathUtility.Floor(comparison)),
                Color => MathUtility.AnyNumberBelowOrEqual(a.ConvertTo<Color>(),
                    comparison.F()),
                _ => throw new ArgumentException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AnyNumberBelow<T>(this T a, double comparison)
            where T : IEquatable<T>
        {
            return a switch
            {
                int => MathUtility.AnyNumberBelow(a.ConvertTo<int>(),
                    MathUtility.Floor(comparison)),
                float => MathUtility.AnyNumberBelow(a.ConvertTo<float>(),
                    comparison.F()),
                double => MathUtility.AnyNumberBelow(a.ConvertTo<double>(),
                    comparison),
                Vector2 => MathUtility.AnyNumberBelow(a.ConvertTo<Vector2>(),
                    comparison.F()),
                Vector3 => MathUtility.AnyNumberBelow(a.ConvertTo<Vector3>(),
                    comparison.F()),
                Vector4 => MathUtility.AnyNumberBelow(a.ConvertTo<Vector4>(),
                    comparison.F()),
                Vector2Int => MathUtility.AnyNumberBelow(a.ConvertTo<Vector2Int>(),
                    MathUtility.Floor(comparison)),
                Vector3Int => MathUtility.AnyNumberBelow(a.ConvertTo<Vector3Int>(),
                    MathUtility.Floor(comparison)),
                Color => MathUtility.AnyNumberBelow(a.ConvertTo<Color>(),
                    comparison.F()),
                _ => throw new ArgumentException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AnyNumberAboveOrEqual<T>(this T a, double comparison)
            where T : IEquatable<T>
        {
            return a switch
            {
                int => MathUtility.AnyNumberAboveOrEqual(a.ConvertTo<int>(),
                    MathUtility.Ceiling(comparison)),
                float => MathUtility.AnyNumberAboveOrEqual(a.ConvertTo<float>(),
                    comparison.F()),
                double => MathUtility.AnyNumberAboveOrEqual(a.ConvertTo<double>(),
                    comparison),
                Vector2 => MathUtility.AnyNumberAboveOrEqual(a.ConvertTo<Vector2>(),
                    comparison.F()),
                Vector3 => MathUtility.AnyNumberAboveOrEqual(a.ConvertTo<Vector3>(),
                    comparison.F()),
                Vector4 => MathUtility.AnyNumberAboveOrEqual(a.ConvertTo<Vector4>(),
                    comparison.F()),
                Vector2Int => MathUtility.AnyNumberAboveOrEqual(
                    a.ConvertTo<Vector2Int>(), MathUtility.Ceiling(comparison)),
                Vector3Int => MathUtility.AnyNumberAboveOrEqual(
                    a.ConvertTo<Vector3Int>(), MathUtility.Ceiling(comparison)),
                Color => MathUtility.AnyNumberAboveOrEqual(a.ConvertTo<Color>(),
                    comparison.F()),
                _ => throw new ArgumentException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AnyNumberAbove<T>(this T a, double comparison)
            where T : IEquatable<T>
        {
            return a switch
            {
                int => MathUtility.AnyNumberAbove(a.ConvertTo<int>(),
                    MathUtility.Ceiling(comparison)),
                float => MathUtility.AnyNumberAbove(a.ConvertTo<float>(),
                    comparison.F()),
                double => MathUtility.AnyNumberAbove(a.ConvertTo<double>(),
                    comparison),
                Vector2 => MathUtility.AnyNumberAbove(a.ConvertTo<Vector2>(),
                    comparison.F()),
                Vector3 => MathUtility.AnyNumberAbove(a.ConvertTo<Vector3>(),
                    comparison.F()),
                Vector4 => MathUtility.AnyNumberAbove(a.ConvertTo<Vector4>(),
                    comparison.F()),
                Vector2Int => MathUtility.AnyNumberAbove(a.ConvertTo<Vector2Int>(),
                    MathUtility.Ceiling(comparison)),
                Vector3Int => MathUtility.AnyNumberAbove(a.ConvertTo<Vector3Int>(),
                    MathUtility.Ceiling(comparison)),
                Color => MathUtility.AnyNumberAbove(a.ConvertTo<Color>(),
                    comparison.F()),
                _ => throw new ArgumentException()
            };
        }


        #endregion

        #endregion

        #region Round

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Round<T>(this T a) where T : IEquatable<T>
        {
            return a switch
            {
                int => a.ConvertTo<T>(),
                float => MathUtility.Round(a.ConvertTo<float>()).ConvertTo<T>(),
                double => MathUtility.Round(a.ConvertTo<double>()).ConvertTo<T>(),
                Vector2 => MathUtility.Round(a.ConvertTo<Vector2>()).ConvertTo<T>(),
                Vector3 => MathUtility.Round(a.ConvertTo<Vector3>()).ConvertTo<T>(),
                Vector4 => MathUtility.Round(a.ConvertTo<Vector4>()).ConvertTo<T>(),
                Vector2Int => a.ConvertTo<T>(),
                Vector3Int => a.ConvertTo<T>(),
                Color => a.ConvertTo<T>(),
                _ => throw new ArgumentException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Ceiling<T>(this T a) where T : IEquatable<T>
        {
            return a switch
            {
                int => a.ConvertTo<T>(),
                float => MathUtility.Ceiling(a.ConvertTo<float>()).ConvertTo<T>(),
                double => MathUtility.Ceiling(a.ConvertTo<double>()).ConvertTo<T>(),
                Vector2 => MathUtility.Ceiling(a.ConvertTo<Vector2>())
                    .ConvertTo<T>(),
                Vector3 => MathUtility.Ceiling(a.ConvertTo<Vector3>())
                    .ConvertTo<T>(),
                Vector4 => MathUtility.Ceiling(a.ConvertTo<Vector4>())
                    .ConvertTo<T>(),
                Vector2Int => a.ConvertTo<T>(),
                Vector3Int => a.ConvertTo<T>(),
                Color => a.ConvertTo<T>(),
                _ => throw new ArgumentException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Floor<T>(this T a) where T : IEquatable<T>
        {
            return a switch
            {
                int => a.ConvertTo<T>(),
                float => MathUtility.Floor(a.ConvertTo<float>()).ConvertTo<T>(),
                double => MathUtility.Floor(a.ConvertTo<double>()).ConvertTo<T>(),
                Vector2 => MathUtility.Floor(a.ConvertTo<Vector2>()).ConvertTo<T>(),
                Vector3 => MathUtility.Floor(a.ConvertTo<Vector3>()).ConvertTo<T>(),
                Vector4 => MathUtility.Floor(a.ConvertTo<Vector4>()).ConvertTo<T>(),
                Vector2Int => a.ConvertTo<T>(),
                Vector3Int => a.ConvertTo<T>(),
                Color => a.ConvertTo<T>(),
                _ => throw new ArgumentException()
            };
        }

        #endregion

        #region + - * / ^

        #region Add

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Add<T>(this T a, T b) where T : IEquatable<T>
        {
            return a switch
            {
                int => (a.ConvertTo<int>() + b.ConvertTo<int>()).ConvertTo<T>(),
                float => (a.ConvertTo<float>() + b.ConvertTo<float>())
                    .ConvertTo<T>(),
                double => (a.ConvertTo<double>() + b.ConvertTo<double>())
                    .ConvertTo<T>(),
                Vector2 => (a.ConvertTo<Vector2>() + b.ConvertTo<Vector2>())
                    .ConvertTo<T>(),
                Vector3 => (a.ConvertTo<Vector3>() + b.ConvertTo<Vector3>())
                    .ConvertTo<T>(),
                Vector4 => (a.ConvertTo<Vector4>() + b.ConvertTo<Vector4>())
                    .ConvertTo<T>(),
                Vector2Int => (a.ConvertTo<Vector2Int>() + b.ConvertTo<Vector2Int>())
                    .ConvertTo<T>(),
                Vector3Int => (a.ConvertTo<Vector3Int>() + b.ConvertTo<Vector3Int>())
                    .ConvertTo<T>(),
                Color => (a.ConvertTo<Color>() + b.ConvertTo<Color>())
                    .ConvertTo<T>(),
                _ => throw new ArgumentException()
            };
        }

        #endregion

        #region Substract

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Subtract<T>(this T a, T b) where T : IEquatable<T>
        {
            return a switch
            {
                int => (a.ConvertTo<int>() - b.ConvertTo<int>()).ConvertTo<T>(),
                float => (a.ConvertTo<float>() - b.ConvertTo<float>())
                    .ConvertTo<T>(),
                double => (a.ConvertTo<double>() - b.ConvertTo<double>())
                    .ConvertTo<T>(),
                Vector2 => (a.ConvertTo<Vector2>() - b.ConvertTo<Vector2>())
                    .ConvertTo<T>(),
                Vector3 => (a.ConvertTo<Vector3>() - b.ConvertTo<Vector3>())
                    .ConvertTo<T>(),
                Vector4 => (a.ConvertTo<Vector4>() - b.ConvertTo<Vector4>())
                    .ConvertTo<T>(),
                Vector2Int => (a.ConvertTo<Vector2Int>() - b.ConvertTo<Vector2Int>())
                    .ConvertTo<T>(),
                Vector3Int => (a.ConvertTo<Vector3Int>() - b.ConvertTo<Vector3Int>())
                    .ConvertTo<T>(),
                Color => (a.ConvertTo<Color>() - b.ConvertTo<Color>())
                    .ConvertTo<T>(),
                _ => throw new ArgumentException()
            };
        }

        #endregion

        #region Negate

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Negate<T>(this T a) where T : IEquatable<T>
        {
            return a switch
            {
                int => (-a.ConvertTo<int>()).ConvertTo<T>(),
                float => (-a.ConvertTo<float>()).ConvertTo<T>(),
                double => (-a.ConvertTo<double>()).ConvertTo<T>(),
                Vector2 => (-a.ConvertTo<Vector2>()).ConvertTo<T>(),
                Vector3 => (-a.ConvertTo<Vector3>()).ConvertTo<T>(),
                Vector4 => (-a.ConvertTo<Vector4>()).ConvertTo<T>(),
                Vector2Int => (-a.ConvertTo<Vector2Int>()).ConvertTo<T>(),
                Vector3Int => (-a.ConvertTo<Vector3Int>()).ConvertTo<T>(),
                Color => throw new ArgumentException(),
                _ => throw new ArgumentException(),
            };
        }

        #endregion

        #region Multiply

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Multiply<T>(this T a, double b) where T : IEquatable<T>
        {
            return a switch
            {
                int num => (num * b).ConvertTo<T>(),
                float num => (num * b).ConvertTo<T>(),
                double num => (num * b).ConvertTo<T>(),
                Vector2 vector => (vector * b.F()).ConvertTo<T>(),
                Vector3 vector => (vector * b.F()).ConvertTo<T>(),
                Vector4 vector => (vector * b.F()).ConvertTo<T>(),
                Vector2Int vector => MathUtility
                    .Multiply(vector, MathUtility.Round(b)).ConvertTo<T>(),
                Vector3Int vector => MathUtility
                    .Multiply(vector, MathUtility.Round(b)).ConvertTo<T>(),
                Color color => (color * b.F()).ConvertTo<T>(),
                _ => throw new ArgumentException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Multiply<T>(this T a, T b) where T : IEquatable<T>
        {
            return a switch
            {
                int => (a.ConvertTo<int>() * b.ConvertTo<int>()).ConvertTo<T>(),
                float => (a.ConvertTo<float>() * b.ConvertTo<float>())
                    .ConvertTo<T>(),
                double => (a.ConvertTo<double>() * b.ConvertTo<double>())
                    .ConvertTo<T>(),
                Vector2 => MathUtility
                    .Multiply(a.ConvertTo<Vector2>(), b.ConvertTo<Vector2>())
                    .ConvertTo<T>(),
                Vector3 => MathUtility
                    .Multiply(a.ConvertTo<Vector3>(), b.ConvertTo<Vector3>())
                    .ConvertTo<T>(),
                Vector4 => MathUtility
                    .Multiply(a.ConvertTo<Vector4>(), b.ConvertTo<Vector4>())
                    .ConvertTo<T>(),
                Vector2Int => MathUtility
                    .Multiply(a.ConvertTo<Vector2Int>(), b.ConvertTo<Vector2Int>())
                    .ConvertTo<T>(),
                Vector3Int => MathUtility
                    .Multiply(a.ConvertTo<Vector3Int>(), b.ConvertTo<Vector3Int>())
                    .ConvertTo<T>(),
                Color => MathUtility
                    .Multiply(a.ConvertTo<Color>(), b.ConvertTo<Color>())
                    .ConvertTo<T>(),
                _ => throw new ArgumentException()
            };
        }

        #endregion

        #region Divide

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Divide<T>(this T dividend, double divisor)
            where T : IEquatable<T>
        {
            return dividend switch
            {
                int num => MathUtility.Divide(num, divisor).ConvertTo<T>(),
                float num => MathUtility.Divide(num, divisor).ConvertTo<T>(),
                double num => MathUtility.Divide(num, divisor).ConvertTo<T>(),
                Vector2 vector => MathUtility.Divide(vector, divisor.F())
                    .ConvertTo<T>(),
                Vector3 vector => MathUtility.Divide(vector, divisor.F())
                    .ConvertTo<T>(),
                Vector4 vector => MathUtility.Divide(vector, divisor.F())
                    .ConvertTo<T>(),
                Vector2Int vector => MathUtility
                    .Divide(vector, MathUtility.Round(divisor)).ConvertTo<T>(),
                Vector3Int vector => MathUtility
                    .Divide(vector, MathUtility.Round(divisor)).ConvertTo<T>(),
                Color color => MathUtility.Divide(color, divisor.F()).ConvertTo<T>(),
                _ => throw new ArgumentException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Divide<T>(this T dividend, T divisor) where T : IEquatable<T>
        {
            return dividend switch
            {
                int => MathUtility
                    .Divide(dividend.ConvertTo<int>(), divisor.ConvertTo<int>())
                    .ConvertTo<T>(),
                float => MathUtility
                    .Divide(dividend.ConvertTo<float>(), divisor.ConvertTo<float>())
                    .ConvertTo<T>(),
                double => MathUtility.Divide(dividend.ConvertTo<double>(),
                    divisor.ConvertTo<double>()).ConvertTo<T>(),
                Vector2 => MathUtility.Divide(dividend.ConvertTo<Vector2>(),
                    divisor.ConvertTo<Vector2>()).ConvertTo<T>(),
                Vector3 => MathUtility.Divide(dividend.ConvertTo<Vector3>(),
                    divisor.ConvertTo<Vector3>()).ConvertTo<T>(),
                Vector4 => MathUtility.Divide(dividend.ConvertTo<Vector4>(),
                    divisor.ConvertTo<Vector4>()).ConvertTo<T>(),
                Vector2Int => MathUtility.Divide(dividend.ConvertTo<Vector2Int>(),
                    divisor.ConvertTo<Vector2Int>()).ConvertTo<T>(),
                Vector3Int => MathUtility.Divide(dividend.ConvertTo<Vector3Int>(),
                    divisor.ConvertTo<Vector3Int>()).ConvertTo<T>(),
                Color => MathUtility
                    .Divide(dividend.ConvertTo<Color>(), divisor.ConvertTo<Color>())
                    .ConvertTo<T>(),
                _ => throw new ArgumentException()
            };
        }

        #endregion

        #region Power

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Power<T>(this T toPow, float power) where T : IEquatable<T>
        {
            return toPow switch
            {
                int => MathUtility.Power(toPow.ConvertTo<int>(), power)
                    .ConvertTo<T>(),
                float => MathUtility.Power(toPow.ConvertTo<float>(), power)
                    .ConvertTo<T>(),
                double => MathUtility.Power(toPow.ConvertTo<double>(), power)
                    .ConvertTo<T>(),
                Vector2 => MathUtility.Power(toPow.ConvertTo<Vector2>(), power)
                    .ConvertTo<T>(),
                Vector3 => MathUtility.Power(toPow.ConvertTo<Vector3>(), power)
                    .ConvertTo<T>(),
                Vector4 => MathUtility.Power(toPow.ConvertTo<Vector4>(), power)
                    .ConvertTo<T>(),
                Vector2Int => MathUtility.Power(toPow.ConvertTo<Vector2Int>(), power)
                    .ConvertTo<T>(),
                Vector3Int => MathUtility.Power(toPow.ConvertTo<Vector3Int>(), power)
                    .ConvertTo<T>(),
                Color => MathUtility.Power(toPow.ConvertTo<Color>(), power)
                    .ConvertTo<T>(),
                _ => throw new ArgumentException()
            };
        }

        #endregion

        #endregion

        #region Min & Max

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Min<T>(this T a, T min) where T : struct, IEquatable<T>
        {
            return a switch
            {
                int => MathUtility.Min(a.ConvertTo<int>(), min.ConvertTo<int>())
                    .ConvertTo<T>(),
                float => MathUtility
                    .Min(a.ConvertTo<float>(), min.ConvertTo<float>())
                    .ConvertTo<T>(),
                double => MathUtility
                    .Min(a.ConvertTo<double>(), min.ConvertTo<double>())
                    .ConvertTo<T>(),
                Vector2 => MathUtility
                    .Min(a.ConvertTo<Vector2>(), min.ConvertTo<Vector2>())
                    .ConvertTo<T>(),
                Vector3 => MathUtility
                    .Min(a.ConvertTo<Vector3>(), min.ConvertTo<Vector3>())
                    .ConvertTo<T>(),
                Vector4 => MathUtility
                    .Min(a.ConvertTo<Vector4>(), min.ConvertTo<Vector4>())
                    .ConvertTo<T>(),
                Vector2Int => MathUtility
                    .Min(a.ConvertTo<Vector2Int>(), min.ConvertTo<Vector2Int>())
                    .ConvertTo<T>(),
                Vector3Int => MathUtility
                    .Min(a.ConvertTo<Vector3Int>(), min.ConvertTo<Vector3Int>())
                    .ConvertTo<T>(),
                Color => MathUtility
                    .Min(a.ConvertTo<Color>(), min.ConvertTo<Color>())
                    .ConvertTo<T>(),
                _ => throw new ArgumentException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Min<T>(this IEnumerable<T> enumerable)
            where T : struct, IEquatable<T>
        {
            return enumerable.Aggregate(Min);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TResult Min<T, TResult>(this IEnumerable<T> enumerable,
            Func<T, TResult> selector)
            where TResult : struct, IEquatable<TResult>
        {
            return enumerable.Select(selector).Aggregate(Min);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T MinOrDefault<T>(this IEnumerable<T> enumerable)
            where T : struct, IEquatable<T>
        {
            return MinOrDefault(enumerable, item => item);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TResult MinOrDefault<T, TResult>(
            this IEnumerable<T> enumerable, Func<T, TResult> selector)
            where TResult : struct, IEquatable<TResult>
        {
            var list = enumerable.ToList();
            return list.Count == 0 ? default : list.Select(selector).Aggregate(Min);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Max<T>(this T a, T min) where T : struct, IEquatable<T>
        {
            return a switch
            {
                int => MathUtility.Max(a.ConvertTo<int>(), min.ConvertTo<int>())
                    .ConvertTo<T>(),
                float => MathUtility
                    .Max(a.ConvertTo<float>(), min.ConvertTo<float>())
                    .ConvertTo<T>(),
                double => MathUtility
                    .Max(a.ConvertTo<double>(), min.ConvertTo<double>())
                    .ConvertTo<T>(),
                Vector2 => MathUtility
                    .Max(a.ConvertTo<Vector2>(), min.ConvertTo<Vector2>())
                    .ConvertTo<T>(),
                Vector3 => MathUtility
                    .Max(a.ConvertTo<Vector3>(), min.ConvertTo<Vector3>())
                    .ConvertTo<T>(),
                Vector4 => MathUtility
                    .Max(a.ConvertTo<Vector4>(), min.ConvertTo<Vector4>())
                    .ConvertTo<T>(),
                Vector2Int => MathUtility
                    .Max(a.ConvertTo<Vector2Int>(), min.ConvertTo<Vector2Int>())
                    .ConvertTo<T>(),
                Vector3Int => MathUtility
                    .Max(a.ConvertTo<Vector3Int>(), min.ConvertTo<Vector3Int>())
                    .ConvertTo<T>(),
                Color => MathUtility
                    .Max(a.ConvertTo<Color>(), min.ConvertTo<Color>())
                    .ConvertTo<T>(),
                _ => throw new ArgumentException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Max<T>(this IEnumerable<T> enumerable)
            where T : struct, IEquatable<T>
        {
            return enumerable.Aggregate(Max);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TResult Max<T, TResult>(this IEnumerable<T> enumerable,
            Func<T, TResult> selector)
            where TResult : struct, IEquatable<TResult>
        {
            return enumerable.Select(selector).Aggregate(Max);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T MaxOrDefault<T>(this IEnumerable<T> enumerable)
            where T : struct, IEquatable<T>
        {
            return MaxOrDefault(enumerable, item => item);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TResult MaxOrDefault<T, TResult>(
            this IEnumerable<T> enumerable, Func<T, TResult> selector)
            where TResult : struct, IEquatable<TResult>
        {
            var list = enumerable.ToList();
            return list.Count == 0 ? default : list.Select(selector).Aggregate(Max);
        }

        #endregion

        #region Abs

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Abs<T>(this T a) where T : IEquatable<T>
        {
            return a switch
            {
                int => MathUtility.Abs(a.ConvertTo<int>()).ConvertTo<T>(),
                float => MathUtility.Abs(a.ConvertTo<float>()).ConvertTo<T>(),
                double => MathUtility.Abs(a.ConvertTo<double>()).ConvertTo<T>(),
                Vector2 => MathUtility.Abs(a.ConvertTo<Vector2>()).ConvertTo<T>(),
                Vector3 => MathUtility.Abs(a.ConvertTo<Vector3>()).ConvertTo<T>(),
                Vector4 => MathUtility.Abs(a.ConvertTo<Vector4>()).ConvertTo<T>(),
                Vector2Int => MathUtility.Abs(a.ConvertTo<Vector2Int>())
                    .ConvertTo<T>(),
                Vector3Int => MathUtility.Abs(a.ConvertTo<Vector3Int>())
                    .ConvertTo<T>(),
                Color => MathUtility.Abs(a.ConvertTo<Color>()).ConvertTo<T>(),
                _ => throw new ArgumentException()
            };
        }

        #endregion

        #region Sum & Products

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Products<T>(this T a) where T : IEquatable<T>
        {
            return a switch
            {
                int => a.ConvertTo<double>(),
                float => a.ConvertTo<double>(),
                double => a.ConvertTo<double>(),
                Vector2 => MathUtility.Products(a.ConvertTo<Vector2>()),
                Vector3 => MathUtility.Products(a.ConvertTo<Vector3>()),
                Vector4 => MathUtility.Products(a.ConvertTo<Vector4>()),
                Vector2Int => MathUtility.Products(a.ConvertTo<Vector2Int>()),
                Vector3Int => MathUtility.Products(a.ConvertTo<Vector3Int>()),
                Color => MathUtility.Products(a.ConvertTo<Color>()),
                _ => throw new ArgumentException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Sum<T>(this T a) where T : IEquatable<T>
        {
            return a switch
            {
                int => a.ConvertTo<double>(),
                float => a.ConvertTo<double>(),
                double => a.ConvertTo<double>(),
                Vector2 => MathUtility.Sum(a.ConvertTo<Vector2>()),
                Vector3 => MathUtility.Sum(a.ConvertTo<Vector3>()),
                Vector4 => MathUtility.Sum(a.ConvertTo<Vector4>()),
                Vector2Int => MathUtility.Sum(a.ConvertTo<Vector2Int>()),
                Vector3Int => MathUtility.Sum(a.ConvertTo<Vector3Int>()),
                Color => MathUtility.Sum(a.ConvertTo<Color>()),
                _ => throw new ArgumentException()
            };
        }

        #endregion

        #region Norm

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double L1Norm<T>(this T a) where T : IEquatable<T>
        {
            return a switch
            {
                int => a.ConvertTo<int>().Abs(),
                float => a.ConvertTo<float>().Abs(),
                double => a.ConvertTo<double>().Abs(),
                Vector2 => MathUtility.L1Norm(a.ConvertTo<Vector2>()),
                Vector3 => MathUtility.L1Norm(a.ConvertTo<Vector3>()),
                Vector4 => MathUtility.L1Norm(a.ConvertTo<Vector4>()),
                Vector2Int => MathUtility.L1Norm(a.ConvertTo<Vector2Int>()),
                Vector3Int => MathUtility.L1Norm(a.ConvertTo<Vector3Int>()),
                Color => MathUtility.L1Norm(a.ConvertTo<Color>()),
                _ => throw new ArgumentException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double L2Norm<T>(this T a) where T : IEquatable<T>
        {
            return a switch
            {
                int => a.ConvertTo<int>().Abs(),
                float => a.ConvertTo<float>().Abs(),
                double => a.ConvertTo<double>().Abs(),
                Vector2 => MathUtility.L2Norm(a.ConvertTo<Vector2>()),
                Vector3 => MathUtility.L2Norm(a.ConvertTo<Vector3>()),
                Vector4 => MathUtility.L2Norm(a.ConvertTo<Vector4>()),
                Vector2Int => MathUtility.L2Norm(a.ConvertTo<Vector2Int>()),
                Vector3Int => MathUtility.L2Norm(a.ConvertTo<Vector3Int>()),
                Color => MathUtility.L2Norm(a.ConvertTo<Color>()),
                _ => throw new ArgumentException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double InfinityNorm<T>(this T a) where T : IEquatable<T>
        {
            return a switch
            {
                int => a.ConvertTo<int>().Abs(),
                float => a.ConvertTo<float>().Abs(),
                double => a.ConvertTo<double>().Abs(),
                Vector2 => MathUtility.InfinityNorm(a.ConvertTo<Vector2>()),
                Vector3 => MathUtility.InfinityNorm(a.ConvertTo<Vector3>()),
                Vector4 => MathUtility.InfinityNorm(a.ConvertTo<Vector4>()),
                Vector2Int => MathUtility.InfinityNorm(a.ConvertTo<Vector2Int>()),
                Vector3Int => MathUtility.InfinityNorm(a.ConvertTo<Vector3Int>()),
                Color => MathUtility.InfinityNorm(a.ConvertTo<Color>()),
                _ => throw new ArgumentException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Norm<T>(this T a, double p) where T : IEquatable<T>
        {
            return a switch
            {
                int => a.ConvertTo<int>().Abs(),
                float => a.ConvertTo<float>().Abs(),
                double => a.ConvertTo<double>().Abs(),
                Vector2 => MathUtility.Norm(a.ConvertTo<Vector2>(), p),
                Vector3 => MathUtility.Norm(a.ConvertTo<Vector3>(), p),
                Vector4 => MathUtility.Norm(a.ConvertTo<Vector4>(), p),
                Vector2Int => MathUtility.Norm(a.ConvertTo<Vector2Int>(), p),
                Vector3Int => MathUtility.Norm(a.ConvertTo<Vector3Int>(), p),
                Color => MathUtility.Norm(a.ConvertTo<Color>(), p),
                _ => throw new ArgumentException()
            };
        }

        #endregion

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Distance<T>(this T a, T b) where T : IEquatable<T>
        {
            return a.Subtract(b).L2Norm();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Lerp<T>(this T from, T to, float t) where T : IEquatable<T>
        {
            return from switch
            {
                int => MathUtility
                    .Lerp(from.ConvertTo<int>(), to.ConvertTo<int>(), t)
                    .ConvertTo<T>(),
                float => MathUtility
                    .Lerp(from.ConvertTo<float>(), to.ConvertTo<float>(), t)
                    .ConvertTo<T>(),
                double => MathUtility
                    .Lerp(from.ConvertTo<double>(), to.ConvertTo<double>(), t)
                    .ConvertTo<T>(),
                Vector2 => MathUtility
                    .Lerp(from.ConvertTo<Vector2>(), to.ConvertTo<Vector2>(), t)
                    .ConvertTo<T>(),
                Vector3 => MathUtility
                    .Lerp(from.ConvertTo<Vector3>(), to.ConvertTo<Vector3>(), t)
                    .ConvertTo<T>(),
                Vector4 => MathUtility
                    .Lerp(from.ConvertTo<Vector4>(), to.ConvertTo<Vector4>(), t)
                    .ConvertTo<T>(),
                Vector2Int => MathUtility.Lerp(from.ConvertTo<Vector2Int>(),
                    to.ConvertTo<Vector2Int>(), t).ConvertTo<T>(),
                Vector3Int => MathUtility.Lerp(from.ConvertTo<Vector3Int>(),
                    to.ConvertTo<Vector3Int>(), t).ConvertTo<T>(),
                Color => MathUtility
                    .Lerp(from.ConvertTo<Color>(), to.ConvertTo<Color>(), t)
                    .ConvertTo<T>(),
                _ => throw new ArgumentException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Lerp<T>(this T from, T to, float t, float power)
            where T : IEquatable<T>
        {
            return from switch
            {
                int => MathUtility
                    .Lerp(from.ConvertTo<int>(), to.ConvertTo<int>(), t, power)
                    .ConvertTo<T>(),
                float => MathUtility
                    .Lerp(from.ConvertTo<float>(), to.ConvertTo<float>(), t, power)
                    .ConvertTo<T>(),
                double => MathUtility.Lerp(from.ConvertTo<double>(),
                    to.ConvertTo<double>(), t, power).ConvertTo<T>(),
                Vector2 => MathUtility.Lerp(from.ConvertTo<Vector2>(),
                    to.ConvertTo<Vector2>(), t, power).ConvertTo<T>(),
                Vector3 => MathUtility.Lerp(from.ConvertTo<Vector3>(),
                    to.ConvertTo<Vector3>(), t, power).ConvertTo<T>(),
                Vector4 => MathUtility.Lerp(from.ConvertTo<Vector4>(),
                    to.ConvertTo<Vector4>(), t, power).ConvertTo<T>(),
                Vector2Int => MathUtility.Lerp(from.ConvertTo<Vector2Int>(),
                    to.ConvertTo<Vector2Int>(), t, power).ConvertTo<T>(),
                Vector3Int => MathUtility.Lerp(from.ConvertTo<Vector3Int>(),
                    to.ConvertTo<Vector3Int>(), t, power).ConvertTo<T>(),
                Color => MathUtility
                    .Lerp(from.ConvertTo<Color>(), to.ConvertTo<Color>(), t, power)
                    .ConvertTo<T>(),
                _ => throw new ArgumentException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Normalize01<T>(this T a, T min, T max) where T : IEquatable<T>
        {
            return a switch
            {
                int => MathUtility.Normalize01(a.ConvertTo<int>(),
                    min.ConvertTo<int>(), max.ConvertTo<int>()).ConvertTo<T>(),
                float => MathUtility.Normalize01(a.ConvertTo<float>(),
                    min.ConvertTo<float>(), max.ConvertTo<float>()).ConvertTo<T>(),
                double => MathUtility.Normalize01(a.ConvertTo<double>(),
                    min.ConvertTo<double>(), max.ConvertTo<double>()).ConvertTo<T>(),
                Vector2 => MathUtility.Normalize01(a.ConvertTo<Vector2>(),
                        min.ConvertTo<Vector2>(), max.ConvertTo<Vector2>())
                    .ConvertTo<T>(),
                Vector3 => MathUtility.Normalize01(a.ConvertTo<Vector3>(),
                        min.ConvertTo<Vector3>(), max.ConvertTo<Vector3>())
                    .ConvertTo<T>(),
                Vector4 => MathUtility.Normalize01(a.ConvertTo<Vector4>(),
                        min.ConvertTo<Vector4>(), max.ConvertTo<Vector4>())
                    .ConvertTo<T>(),
                Vector2Int => MathUtility.Normalize01(a.ConvertTo<Vector2Int>(),
                        min.ConvertTo<Vector2Int>(), max.ConvertTo<Vector2Int>())
                    .ConvertTo<T>(),
                Vector3Int => MathUtility.Normalize01(a.ConvertTo<Vector3Int>(),
                        min.ConvertTo<Vector3Int>(), max.ConvertTo<Vector3Int>())
                    .ConvertTo<T>(),
                Color => throw new ArgumentException(),
                _ => throw new ArgumentException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T PointSymmetric<T>(this T a, T point) where T : IEquatable<T>
        {
            return a switch
            {
                int => MathUtility
                    .PointSymmetric(a.ConvertTo<int>(), point.ConvertTo<int>())
                    .ConvertTo<T>(),
                float => MathUtility
                    .PointSymmetric(a.ConvertTo<float>(), point.ConvertTo<float>())
                    .ConvertTo<T>(),
                double => MathUtility
                    .PointSymmetric(a.ConvertTo<double>(), point.ConvertTo<double>())
                    .ConvertTo<T>(),
                Vector2 => MathUtility
                    .PointSymmetric(a.ConvertTo<Vector2>(),
                        point.ConvertTo<Vector2>()).ConvertTo<T>(),
                Vector3 => MathUtility
                    .PointSymmetric(a.ConvertTo<Vector3>(),
                        point.ConvertTo<Vector3>()).ConvertTo<T>(),
                Vector4 => MathUtility
                    .PointSymmetric(a.ConvertTo<Vector4>(),
                        point.ConvertTo<Vector4>()).ConvertTo<T>(),
                Vector2Int => MathUtility.PointSymmetric(a.ConvertTo<Vector2Int>(),
                    point.ConvertTo<Vector2Int>()).ConvertTo<T>(),
                Vector3Int => MathUtility.PointSymmetric(a.ConvertTo<Vector3Int>(),
                    point.ConvertTo<Vector3Int>()).ConvertTo<T>(),
                Color => throw new ArgumentException(),
                _ => throw new ArgumentException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T RandomRange<T>(this T a, T min) where T : IEquatable<T>
        {
            return a switch
            {
                int => RandomUtility
                    .RandomRange(a.ConvertTo<int>(), min.ConvertTo<int>())
                    .ConvertTo<T>(),
                float => RandomUtility
                    .RandomRange(a.ConvertTo<float>(), min.ConvertTo<float>())
                    .ConvertTo<T>(),
                double => throw new ArgumentException(),
                Vector2 => RandomUtility
                    .RandomRange(a.ConvertTo<Vector2>(), min.ConvertTo<Vector2>())
                    .ConvertTo<T>(),
                Vector3 => RandomUtility
                    .RandomRange(a.ConvertTo<Vector3>(), min.ConvertTo<Vector3>())
                    .ConvertTo<T>(),
                Vector4 => RandomUtility
                    .RandomRange(a.ConvertTo<Vector4>(), min.ConvertTo<Vector4>())
                    .ConvertTo<T>(),
                Vector2Int => RandomUtility.RandomRange(a.ConvertTo<Vector2Int>(),
                    min.ConvertTo<Vector2Int>()).ConvertTo<T>(),
                Vector3Int => RandomUtility.RandomRange(a.ConvertTo<Vector3Int>(),
                    min.ConvertTo<Vector3Int>()).ConvertTo<T>(),
                Color => RandomUtility
                    .RandomRange(a.ConvertTo<Color>(), min.ConvertTo<Color>())
                    .ConvertTo<T>(),
                _ => throw new ArgumentException()
            };
        }
    }
}

