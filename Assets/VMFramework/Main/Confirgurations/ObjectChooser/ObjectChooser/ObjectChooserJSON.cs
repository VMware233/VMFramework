namespace VMFramework.Configuration
{
    public partial class ObjectChooser<T>
    {
        public bool ShouldSerializerandomType()
        {
            return isRandomValue == true;
        }

        public bool ShouldSerializefixedType()
        {
            return isRandomValue == false;
        }

        public bool ShouldSerializevalue()
        {
            return isRandomValue == false && fixedType == SINGLE_VALUE;
        }

        public bool ShouldSerializevalueProbabilities()
        {
            return isRandomValue == true && randomType == WEIGHTED_SELECT;
        }

        public bool ShouldSerializecircularItems()
        {
            return isRandomValue == true && randomType == CIRCULAR_SELECT;
        }

        public bool ShouldSerializestartCircularIndex()
        {
            return isRandomValue == true && randomType == CIRCULAR_SELECT;
        }

        public bool ShouldSerializepingPong()
        {
            return isRandomValue == true && randomType == CIRCULAR_SELECT;
        }
    }
}