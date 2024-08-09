namespace VMFramework.Core.Pools
{
    public interface IDictionaryPool<in TKey, TItem> : IPool<TItem>
    {
        public TItem Get(TKey key, out bool isFreshlyCreated);
    }
}