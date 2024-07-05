namespace VMFramework.Core
{
    public interface IDestructible : IReadOnlyDestructible
    {
        protected void SetDestructible(bool isDestructible);
    }
}