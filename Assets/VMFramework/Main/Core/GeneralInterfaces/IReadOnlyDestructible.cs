namespace VMFramework.Core
{
    public interface IReadOnlyDestructible
    {
        public bool IsDestroyed { get; }
    }
}