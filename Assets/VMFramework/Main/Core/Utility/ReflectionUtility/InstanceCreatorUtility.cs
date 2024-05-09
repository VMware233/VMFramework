using System;
using System.Runtime.CompilerServices;

namespace VMFramework.Core
{
    public static partial class ReflectionUtility
    {
        /// <summary>
        /// 创建一个实例
        /// </summary>
        /// <returns>值类型返回default，如果是class就返回一个new()</returns>
        /// <exception cref="InvalidOperationException"></exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TValue CreateInstance<TValue>()
        {
            return (TValue)CreateInstance(typeof(TValue));
        }

        /// <summary>
        /// 创建一个实例
        /// </summary>
        /// <param name="type"></param>
        /// <returns>值类型返回default，如果是class就返回一个new()</returns>
        /// <exception cref="InvalidOperationException"></exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static object CreateInstance(this Type type)
        {
            if (type.IsValueType)
            {
                return default;
            }

            if (type.IsGenericTypeDefinition)
            {
                throw new ArgumentException(
                    $"Type:{type.FullName} 是泛型定义，无法创建实例，请填补泛型参数");
            }

            if (type.IsAbstract)
            {
                throw new ArgumentException($"Type:{type.FullName} 是抽象类，无法创建实例");
            }

            if (type.GetConstructor(Type.EmptyTypes) != null)
            {
                return Activator.CreateInstance(type);
            }

            if (type == typeof(string))
            {
                return string.Empty;
            }

            throw new ArgumentException($"Type:{type.FullName} 没有无参构造函数，无法创建实例");
        }
        
        /// <summary>
        /// 创建一个实例
        /// </summary>
        /// <param name="type"></param>
        /// <returns>值类型返回default，如果是class就返回一个new()</returns>
        /// <exception cref="InvalidOperationException"></exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static object CreateInstance(this Type type, params object[] objs)
        {
            if (type.IsValueType)
            {
                return default;
            }

            if (type.IsGenericTypeDefinition)
            {
                throw new ArgumentException(
                    $"Type:{type.FullName} 是泛型定义，无法创建实例，请填补泛型参数");
            }

            if (type.IsAbstract)
            {
                throw new ArgumentException($"Type:{type.FullName} 是抽象类，无法创建实例");
            }

            return Activator.CreateInstance(type, objs);
        }
    }
}