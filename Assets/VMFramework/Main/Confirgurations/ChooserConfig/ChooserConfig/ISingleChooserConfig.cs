using VMFramework.Core;

namespace VMFramework.Configuration
{
    public interface ISingleChooserConfig<T> : IChooserConfig<T>
    {
        protected IChooser<T> objectChooser { get; set; }

        void IChooserConfig.Init()
        {
            objectChooser = GenerateNewObjectChooser();
        }

        T IChooserConfig<T>.GetValue()
        {
            return objectChooser.GetValue();
        }

        IChooser<T> IChooserConfig<T>.GetObjectChooser()
        {
            return objectChooser;
        }

        void IChooserConfig.RegenerateObjectChooser()
        {
            objectChooser = GenerateNewObjectChooser();
        }
    }
}