namespace VMFramework.Procedure
{
    public readonly struct InitializationAction
    {
        public readonly int order;
        public readonly IInitializer.InitAction action;
        public readonly IInitializer initializer;
        
        public InitializationAction(int order, IInitializer.InitAction action, IInitializer initializer)
        {
            this.order = order;
            this.action = action;
            this.initializer = initializer;
        }

        public InitializationAction(InitializationOrder order, IInitializer.InitAction action, IInitializer initializer)
        {
            this.order = (int)order;
            this.action = action;
            this.initializer = initializer;
        }
    }
}