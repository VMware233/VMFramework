namespace VMFramework.GameLogicArchitecture
{
    public interface IIDOwner : IIDOwner<string>
    {
        
    }

    public interface IIDOwner<out T>
    {
        public T id { get; }
    }
}
