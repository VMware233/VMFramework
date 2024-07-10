namespace VMFramework.Configuration
{
    public interface ICircularSelectChooserConfig<TItem> : ICircularSelectChooserConfig<TItem, TItem>
    {
        
    }
    
    public interface ICircularSelectChooserConfig<TWrapper, TItem> : ICollectionChooserConfig<TWrapper, TItem>
    {
        
    }
}