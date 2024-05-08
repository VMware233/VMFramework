namespace VMFramework.Configuration
{
    public interface INumberOrVectorValueSetter<T> : IValueSetter<T>
    {
        public T GetMaxValue();

        public void SetMaxValue(T maxValue);

        public T GetMinValue();
        
        public void SetMinValue(T minValue);
    }
}