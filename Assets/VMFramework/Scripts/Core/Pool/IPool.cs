using System;
using System.Runtime.CompilerServices;

namespace VMFramework.Core
{
    #region Utility

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

    #endregion
    
    #region Interfaces

    public interface IPool<T>
    {
        /// <summary>
        /// 从池中获取一个对象，如果池中没有对象则使用creator函数创建一个对象,
        /// 并通过isFreshlyCreated变量返回是否是新创建的对象
        /// </summary>
        /// <param name="creator"></param>
        /// <param name="isFreshlyCreated"></param>
        /// <returns></returns>
        public T Get(Func<T> creator, out bool isFreshlyCreated);

        /// <summary>
        /// 将对象归还到池中
        /// </summary>
        /// <param name="item"></param>
        public void Return(T item);

        /// <summary>
        /// 池是否为空
        /// </summary>
        /// <returns></returns>
        public bool IsEmpty();

        /// <summary>
        /// 清空池
        /// </summary>
        public void Clear();
    }

    public interface ICheckablePool<T> : IPool<T>
    {
        /// <summary>
        /// 检查池中是否包含某个对象
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Contains(T item);
    }

    public interface ILimitPool<T> : IPool<T>
    {
        /// <summary>
        /// 池的最大容量
        /// </summary>
        public int maxCapacity { get; }

        /// <summary>
        /// 池是否已满
        /// </summary>
        /// <returns></returns>
        public bool IsFull();
    }

    public interface ICheckableLimitPool<T> : ICheckablePool<T>, ILimitPool<T>
    {

    }

    #endregion
}
