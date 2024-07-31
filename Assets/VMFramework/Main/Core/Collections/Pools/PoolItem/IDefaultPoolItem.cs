namespace VMFramework.Core.Pools
{
    public interface IDefaultPoolItem : IPoolItem
    {
        public void OnCreate();
    }
}