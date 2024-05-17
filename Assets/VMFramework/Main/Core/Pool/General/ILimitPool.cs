namespace VMFramework.Core.Pool
{
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
}