namespace VMFramework.Configuration
{
    public abstract partial class NumberOrVectorChooser<T, TRange>
    {
        public bool ShouldSerializerangeValue()
        {
            return isRandomValue == true && randomType == RANGE_SELECT;
        }
    }
}