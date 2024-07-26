namespace VMFramework.Core
{
    public interface IMapping<in TPoint, out TResult>
    {
        public TResult Map(TPoint point);
    }
}