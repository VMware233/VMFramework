namespace VMFramework.Core
{
    public interface IRandomPointProvider<out TPoint>
    {
        public TPoint GetRandomPoint();
    }
}