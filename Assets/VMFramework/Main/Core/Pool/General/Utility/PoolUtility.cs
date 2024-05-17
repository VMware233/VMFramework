using System;
using System.Runtime.CompilerServices;

namespace VMFramework.Core.Pool
{
    public static class PoolUtility
    {
        /// <summary>
        /// 从池中获取一个对象，如果池中没有对象则使用creator函数创建一个对象
        /// </summary>
        /// <param name="pool"></param>
        /// <param name="creator"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Get<T>(this IPool<T> pool, Func<T> creator)
        {
            return pool.Get(creator, out _);
        }
        
        /// <summary>
        /// 对池进行预热，创建count个对象并放入池中
        /// </summary>
        /// <param name="pool"></param>
        /// <param name="creator"></param>
        /// <param name="count"></param>
        /// <typeparam name="T"></typeparam>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Prewarm<T>(this IPool<T> pool, int count, Func<T> creator)
        {
            for (int i = 0; i < count; i++)
            {
                pool.Return(creator());
            }
        }
    }
}