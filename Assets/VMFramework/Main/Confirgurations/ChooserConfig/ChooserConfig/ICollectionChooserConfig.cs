namespace VMFramework.Configuration
{
    public interface ICollectionChooserConfig<TWrapper, TItem> : IChooserConfig<TWrapper, TItem>
    {
        public bool ContainsWrapper(TWrapper wrapper);
        
        public void AddWrapper(TWrapper wrapper);
        
        public void RemoveWrapper(TWrapper wrapper);
    }
}