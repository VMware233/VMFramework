namespace VMFramework.Configuration
{
    public interface IInitializableConfig
    {
        public bool initDone { get; }

        public void Init();
    }
}