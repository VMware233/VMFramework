using System;

namespace VMFramework.Core.Pool
{
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
}
