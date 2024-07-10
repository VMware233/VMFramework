using VMFramework.Core;

namespace VMFramework.Configuration
{
    public interface IChooserGenerator
    {
        public IChooser GetObjectChooser();
    }

    public interface IChooserGenerator<T> : IChooserGenerator
    {
        public new IChooser<T> GetObjectChooser();

        IChooser IChooserGenerator.GetObjectChooser()
        {
            return GetObjectChooser();
        }
    }
}