using System;

namespace VMFramework.Core.Pool
{
    public interface IPool<T>
    {
        public int count { get; }
        
        /// <summary>
        /// 从池中获取一个对象，如果池中没有对象则会自动创建一个对象,
        /// 并通过isFreshlyCreated变量返回是否是新创建的对象
        /// </summary>
        /// <param name="isFreshlyCreated"></param>
        /// <returns></returns>
        public T Get(out bool isFreshlyCreated);

        /// <summary>
        /// 将对象归还到池中
        /// </summary>
        /// <param name="item"></param>
        public void Return(T item);

        /// <summary>
        /// 清空池
        /// </summary>
        public void Clear();
    }
}
