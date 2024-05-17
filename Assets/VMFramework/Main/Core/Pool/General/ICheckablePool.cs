namespace VMFramework.Core.Pool
{
    public interface ICheckablePool<T> : IPool<T>
    {
        /// <summary>
        /// 检查池中是否包含某个对象
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Contains(T item);
    }
}