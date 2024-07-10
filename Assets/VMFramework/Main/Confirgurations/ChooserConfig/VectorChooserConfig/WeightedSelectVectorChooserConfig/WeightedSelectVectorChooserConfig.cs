using System;
using System.Collections.Generic;

namespace VMFramework.Configuration
{
    public partial class WeightedSelectVectorChooserConfig<TVector> : WeightedSelectChooserConfig<TVector>,
        IVectorChooserConfig<TVector>
        where TVector : struct, IEquatable<TVector>
    {
        public WeightedSelectVectorChooserConfig() : base()
        {
            
        }
        
        public WeightedSelectVectorChooserConfig(IEnumerable<TVector> items) : base(items)
        {
            
        }
    }
}